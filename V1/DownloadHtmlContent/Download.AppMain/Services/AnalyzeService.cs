using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Download.AppMain.Helpers;
using Download.AppMain.Models;
using Download.Common.Extensions;
using Download.Models.NodeModels;
using Download.Models.PageModels;
using Download.Models.UrlModels;
using Download.Services.WebClientServices;
using HtmlAgilityPack;

namespace Download.AppMain.Services
{
    public class AnalyzeService
    {
        private readonly IWebClientService _webClientService;
        private readonly HandleTagService _handleTagService;
        private readonly UrlExtenstion _urlExtenstion;
        private readonly HandleFileService _handleFileService;
        private readonly DirectoryExtension _directoryExtension;
        public AnalyzeService(
                            IWebClientService webClientService,
                            HandleTagService handleTagService,
                            UrlExtenstion urlExtenstion,
                            HandleFileService handleFileService,
                            DirectoryExtension directoryExtension)
        {
            _webClientService = webClientService;
            _handleTagService = handleTagService;
            _urlExtenstion = urlExtenstion;
            _handleFileService = handleFileService;
            _directoryExtension = directoryExtension;
        }

        public string InitFolder(string pageName, string projectPath)
        {
            string projectCurrentPath = Path.Combine(projectPath, pageName);

            Trace.WriteLine($"Project path: {projectCurrentPath}");

            _directoryExtension.Create(projectCurrentPath);

            foreach (var folderType in FolderTypeHelper.FolderTypeList)
            {
                string folderPath = Path.Combine(projectCurrentPath, folderType);

                Trace.WriteLine($"Folder path: {folderPath}");

                _directoryExtension.Create(folderPath);
            }
            return projectCurrentPath;
        }

        public async Task<SourceAndAllLinkInPageModel> DownloadPageAndGetAllLinkInPage(LinkModel pageLink)
        {
            if (pageLink == null)
                return null;

            if (!pageLink.IsHtml)
            {
                var htmlPage = await _webClientService.DownloadStringAsync(pageLink.Link, pageLink.Link);

                if (string.IsNullOrEmpty(htmlPage))
                    return null;

                pageLink.SourcePage = htmlPage;
            }

            IEnumerable<HtmlNode> htmlNodes = _handleTagService.GetTagFromHtmlSource(pageLink.SourcePage);
            if (htmlNodes == null || !htmlNodes.Any())
                return null;

            IEnumerable<TagListModel<HtmlNode>> htmlNodeList = htmlNodes.Where(c => c.HasAttributes)
                                                                        .GroupBy(c => c.Name)
                                                                        .Select(s => new TagListModel<HtmlNode>
                                                                        {
                                                                            TagName = s.Key,
                                                                            DataList = s.ToList()
                                                                        });

            IEnumerable<HtmlTagModel> htmlNodeModels = _handleTagService.HandleTag(htmlNodeList);

            var rootPage = _urlExtenstion.DetectRoot(pageLink.Link);

            return new SourceAndAllLinkInPageModel
            {
                PageName = pageLink.LinkName,
                PageLink = pageLink.Link,
                HtmlPage = pageLink.SourcePage,
                RootPage = rootPage,
                HtmlTagModels = htmlNodeModels
            };
        }

        public List<NodeLinkModel> DownloadLinkFromNode(AnalysisUrlModel rootPage, string projectPath, IEnumerable<HtmlTagModel> htmlNodeModels, string referer)
        {
            List<NodeLinkModel> nodeLinks = new List<NodeLinkModel>();

            var tasks = new List<Task<NodeLinkModel>>();

            foreach (var htmlNode in htmlNodeModels)
            {
                var page = rootPage;
                var handleLink = _handleFileService.HandleLink(page, htmlNode, projectPath, referer);
                tasks.Add(handleLink);
            }

            var results = Task.WhenAll(tasks.ToArray());

            results.Wait();

            var linkResults = results.Result;

            foreach (var nodeLinkModel in linkResults)
            {
                if (nodeLinkModel != null && !string.IsNullOrEmpty(nodeLinkModel.OnlineLink))
                {
                    nodeLinkModel.PageLink = referer;
                    nodeLinks.Add(nodeLinkModel);
                }
            }
            
            return nodeLinks;
        }
    }
}
