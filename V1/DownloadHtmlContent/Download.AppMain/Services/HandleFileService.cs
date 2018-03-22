using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Download.Common.Extensions;
using Download.Common.Resources;
using Download.Models;
using Download.Models.NodeModels;
using Download.Models.UrlModels;
using Download.Services.WebClientServices;

namespace Download.AppMain.Services
{
    public class HandleFileService
    {
        private readonly IWebClientService _webClientService;

        private readonly UrlExtenstion _urlExtenstion;

        private readonly RegexExtension _regexExtension;
        private readonly FileExtension _fileExtension;

        public HandleFileService(
            IWebClientService webClientService,
            UrlExtenstion urlExtenstion,
            RegexExtension regexExtension,
            FileExtension fileExtension)
        {
            _webClientService = webClientService;
            _urlExtenstion = urlExtenstion;
            _regexExtension = regexExtension;
            _fileExtension = fileExtension;
        }

        public async Task<NodeLinkModel> HandleLink(AnalysisUrlModel analysisUrlModel, HtmlTagModel htmlTagModel, string pagePath)
        {
            try
            {
                string baseLink = htmlTagModel.Link;

                if (htmlTagModel.Link.IndexOf("../", StringComparison.Ordinal) == 0)
                {
                    htmlTagModel.Link = htmlTagModel.Link.Replace("../", "");
                }

                if (_fileExtension.BlackLink(htmlTagModel.Link))
                {
                    return null;
                }

                if (_fileExtension.IsImageBase64(htmlTagModel.Link))
                {
                    FolderType folderFile = FolderType.Images;

                    return HandleImageBase64(htmlTagModel.Link, pagePath, folderFile);
                }

                string onlineLink = await FixLink(analysisUrlModel, htmlTagModel.Link);

                string nameFile = _urlExtenstion.GetFileNameFromUrl(onlineLink);

                FolderType folderType = FolderType.Images;

                switch (htmlTagModel.TagType)
                {
                    case TagType.Css:
                        {
                            folderType = _fileExtension.IsImage(htmlTagModel.Link) ?
                                                        FolderType.Images :
                                                        FolderType.Css;
                        }
                        break;
                    case TagType.Javascript:
                        folderType = FolderType.Scripts;
                        break;
                }

                if (folderType == FolderType.Images && _fileExtension.IsFont(htmlTagModel.Link))
                {
                    folderType = FolderType.Fonts;
                }

                if (folderType == FolderType.Images)
                {
                    if (!_fileExtension.IsImage(htmlTagModel.Link))
                    {
                        return null;
                    }
                }

                var folderTypeString = folderType.GetCustomAttributeDescription();

                string filePath = Path.Combine(pagePath, folderTypeString, nameFile);

                if (!_fileExtension.Exists(filePath))
                    _webClientService.DownloadFileAsync(onlineLink, filePath).Wait();

                return new NodeLinkModel
                {
                    BaseLink = baseLink,
                    UrlInFile = $"{folderTypeString}/{nameFile}",
                    FolderType = folderType,
                    LocalLink = filePath,
                    OnlineLink = onlineLink
                };
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<NodeLinkModel>> HandleCss(string cssFile, AnalysisUrlModel analysisUrlModel, string pagePath)
        {
            try
            {
                string fileContent = await _fileExtension.ReadTextAsync(cssFile);

                StringBuilder dataCss = new StringBuilder(fileContent);

                MatchCollection matchs = _regexExtension.Matchs(RegexResource.ImageInCss, fileContent);

                List<NodeLinkModel> nodeLinkModels = new List<NodeLinkModel>();

                FolderType folderFile = FolderType.Images;

                foreach (Match match in matchs)
                {
                    string linkImage = match.ToString();

                    linkImage = _regexExtension.Replace(RegexResource.UrlImageInCss, linkImage);

                    bool preFix = false;

                    if (linkImage.IndexOf(RegexResource.PrefixLink, StringComparison.Ordinal) == 0)
                    {
                        linkImage = linkImage.Replace(RegexResource.PrefixLink, "");
                        preFix = true;
                    }

                    NodeLinkModel nodeLink;

                    if (_fileExtension.IsImageBase64(linkImage))
                    {
                        nodeLink = HandleImageBase64(linkImage, pagePath, folderFile);
                    }
                    else
                    {
                        if (_fileExtension.IsFont(linkImage))
                        {
                            folderFile = FolderType.Fonts;
                        }
                        else if (!_fileExtension.IsImage(linkImage))
                        {
                            folderFile = FolderType.Css;
                        }
                        else
                        {
                            folderFile = FolderType.Images;
                        }

                        string onlineLink = await FixLink(analysisUrlModel, linkImage);

                        string nameFile = _urlExtenstion.GetFileNameFromUrl(linkImage);

                        var folderTypeString = folderFile.GetCustomAttributeDescription();

                        if (folderFile == FolderType.Css)
                        {
                            //if css file read file add download file
                            nameFile = _fileExtension.GetUrlSeo(linkImage.Replace(nameFile, "")) + nameFile;
                        }

                        string filePath = Path.Combine(pagePath, folderTypeString, nameFile);

                        if (!_fileExtension.Exists(filePath))
                        {
                            await _webClientService.DownloadFileAsync(onlineLink, filePath);
                        }

                        string urlInFile;

                        if (folderFile == FolderType.Css)
                        {
                            urlInFile = nameFile;
                            if (preFix)
                            {
                                linkImage = RegexResource.PrefixLink + linkImage;
                            }
                        }
                        else
                        {
                            urlInFile = $"{(preFix ? RegexResource.PrefixLink : "")}{folderTypeString}/{nameFile}";
                        }
                        nodeLink = new NodeLinkModel
                        {
                            UrlInFile = urlInFile,
                            FolderType = folderFile,
                            LocalLink = filePath,
                            OnlineLink = onlineLink
                        };
                    }

                    nodeLinkModels.Add(nodeLink);

                    dataCss = dataCss.Replace(linkImage, nodeLink.UrlInFile);
                }

                _fileExtension.WriteAllText(cssFile, dataCss.ToString());

                return nodeLinkModels;
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                return new List<NodeLinkModel>();
            }
        }

        #region Private Method
        private async Task<string> FixLink(AnalysisUrlModel analysisUrlModel, string link)
        {
            string linkFix;

            if (link.StartsWith("//"))
                linkFix = "http:" + link;
            else if (_urlExtenstion.IsUrl(link))
                linkFix = link;
            else
                linkFix = await FindLinkOnline(analysisUrlModel, link);

            linkFix = _urlExtenstion.BestUrl(linkFix);

            if (!await _webClientService.IsUrl(linkFix))
                linkFix = string.Empty;

            return linkFix;
        }

        private async Task<string> FindLinkOnline(AnalysisUrlModel analysisUrlModel, string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return string.Empty;

            string rootPage = analysisUrlModel.RootPage;

            string onlineLink = $"{rootPage}/{link}";

            if (await _webClientService.IsUrl(onlineLink))
                return onlineLink;

            foreach (var segment in analysisUrlModel.Segments)
            {
                var indexOf = analysisUrlModel.Segments.IndexOf(segment);
                if (indexOf <= 0)
                    continue;

                rootPage = $"{rootPage}{segment}";

                onlineLink = $"{rootPage}/{link}";

                if (! await _webClientService.IsUrl(onlineLink))
                    continue;
                break;
            }
            return onlineLink;
        }

        private NodeLinkModel HandleImageBase64(string link, string pagePath, FolderType folderFile)
        {
            Random random = new Random();

            var fileName = random.Next(1, 99999).ToString();

            string folderTypeString = folderFile.GetCustomAttributeDescription();

            var fileDirectory = Path.Combine(pagePath, folderTypeString, fileName);

            string imageExtension;
            string onlineLink = _fileExtension.SaveImageBase64(link, fileDirectory, out imageExtension);

            return new NodeLinkModel
            {
                UrlInFile = $"{folderTypeString}/{fileName}{imageExtension}",
                FolderType = folderFile,
                LocalLink = link,
                OnlineLink = onlineLink
            };
        }

        #endregion
    }
}
