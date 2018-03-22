using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Download.AppMain.Helpers;
using Download.Common.Extensions;
using Download.Common.Resources;
using Download.Models;
using Download.Models.NodeModels;
using HtmlAgilityPack;

namespace Download.AppMain.Services
{
    public class HandleTagService
    {
        private readonly HtmlAgilityPackExtension _htmlAgilityPackExtension;
        private readonly FileExtension _fileExtension;
        private readonly RegexExtension _regexExtension;
        public HandleTagService(HtmlAgilityPackExtension htmlAgilityPackExtension,
            FileExtension fileExtension, RegexExtension regexExtension)
        {
            _htmlAgilityPackExtension = htmlAgilityPackExtension;
            _fileExtension = fileExtension;
            _regexExtension = regexExtension;
        }
        public IEnumerable<HtmlNode> GetTagFromHtmlSource(string htmlSource)
        {
            var htmlDocument = _htmlAgilityPackExtension.InitHtmlDocument(htmlAgilityPack =>
            {
                htmlAgilityPack.LoadHtml(htmlSource);
            });

            IEnumerable<HtmlNode> nodes = _htmlAgilityPackExtension.QueryNodes(htmlDocument,
                node => TagTypeHelper.TagList.Any(any => any.ToLower() == node.Name.ToLower()));

            return nodes;
        }

        public HtmlTagModel HandleAttributeInTag(HtmlNode htmlNode, string attribute, string tagName)
        {
            var link = htmlNode.GetAttributeValue(attribute, null);
            if (string.IsNullOrEmpty(link))
                return null;

            var tagType = TagTypeHelper.TagTypeList.FirstOrDefault(c => c.GetCustomAttributeDescription().ToLower() == tagName);

            if (tagName.Trim() == "div")
            {
                tagType = TagType.Image;
            }

            if ((tagType == TagType.Image && !_fileExtension.IsImage(link)))
                return null;

            var htmlNodeModel = new HtmlTagModel
            {
                TagType = tagType
            };

            if (BlackList.IgnoreList(link))
                return null;

            var icoFile = link.IndexOf(BlackList.IconFile, StringComparison.Ordinal);
            if (icoFile != -1)
            {
                htmlNodeModel.TagType = TagType.Image;
            }

            htmlNodeModel.Link = link;
            return htmlNodeModel;
        }

        public IEnumerable<HtmlTagModel> HandleTag(IEnumerable<TagListModel<HtmlNode>> htmlNodeList)
        {
            List<Task<List<HtmlTagModel>>> taskList = new List<Task<List<HtmlTagModel>>>();

            foreach (var htmlNodeItem in htmlNodeList)
            {
                Task<List<HtmlTagModel>> currentNodeTask = Task.Factory.StartNew(() =>
                {
                    return HandlerNode(htmlNodeItem);
                });

                taskList.Add(currentNodeTask);
            }

            Task<List<HtmlTagModel>[]> htmlTagModelTask = Task.WhenAll(taskList.ToArray());

            htmlTagModelTask.Wait();

            return htmlTagModelTask.Result.SelectMany(s => s);
        }

        private List<HtmlTagModel> HandlerNode(TagListModel<HtmlNode> htmlNodeItem)
        {
            var tagName = htmlNodeItem.TagName;
            var nodeList = htmlNodeItem.DataList;

            List<HtmlTagModel> htmlNodeModels = new List<HtmlTagModel>();

            var styleTag = TagType.Style.GetCustomAttributeDescription();
            if (tagName == styleTag.ToLower())
            {
                foreach (var htmlNode in nodeList)
                {
                    var content = htmlNode.OuterHtml;
                    var regex = RegexResource.ImageInCss;

                    var matchesCollection = _regexExtension.Matchs(regex, content);

                    IEnumerable<string> imageUrls = (from object item in matchesCollection
                                                     select _regexExtension
                                                         .Replace(RegexResource.ContentImageInCss, item.ToString()))
                        .ToList();

                    if (!imageUrls.Any())
                        continue;

                    var htmlTagModels = imageUrls.Select(cssLink => new HtmlTagModel
                    {
                        Link = cssLink,
                        TagType = TagType.Image
                    });

                    htmlNodeModels.AddRange(htmlTagModels);
                }
            }
            else
            {
                foreach (var htmlNode in nodeList)
                {
                    var attributes =
                        htmlNode.Attributes.Where(
                            c => !BlackList.IgnoreAttributes.Contains(c.Name.Trim().ToLower()));

                    foreach (var attribute in attributes)
                    {
                        var htmlNodeModel = HandleAttributeInTag(htmlNode, attribute.Name, tagName);
                        if (htmlNodeModel == null)
                            continue;

                        htmlNodeModels.Add(htmlNodeModel);
                    }
                }
            }

            return htmlNodeModels;
        }
    }
}
