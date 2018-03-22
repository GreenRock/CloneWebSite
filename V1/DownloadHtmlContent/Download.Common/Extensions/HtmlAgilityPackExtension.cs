using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace Download.Common.Extensions
{
    public class HtmlAgilityPackExtension
    {
        public IEnumerable<HtmlNode> QueryNodes(string htmlString, string nodeName = "")
        {
            return InitHtmlDocument(htmlDocument => { htmlDocument.LoadHtml(htmlString); }, nodeName);
        }

        public IEnumerable<HtmlNode> QueryNodes(HtmlDocument htmlDocument, string nodeName = "")
        {
            return htmlDocument.DocumentNode.Descendants(nodeName);
        }
        public IEnumerable<HtmlNode> QueryNodes(HtmlDocument htmlDocument, Func<HtmlNode,bool> predicate)
        {
            return htmlDocument.DocumentNode.Descendants().Where(predicate);
        }
        public IEnumerable<HtmlNode> QueryNodes(Stream htmlStream, string nodeName = "")
        {
            return InitHtmlDocument(htmlDocument => { htmlDocument.Load(htmlStream); }, nodeName);
        }

        public string GetValueOfNode(HtmlNode node, string nodeName)
        {
            return node.GetAttributeValue(nodeName, null);
        }

        public IEnumerable<HtmlNode> InitHtmlDocument(Action<HtmlDocument> load, string nodeName)
        {
            var htmlDocument = new HtmlDocument();

            load.Invoke(htmlDocument);

            return !string.IsNullOrEmpty(nodeName) ?
                htmlDocument.DocumentNode.Descendants(nodeName) :
                htmlDocument.DocumentNode.Descendants();
        }

        public HtmlDocument InitHtmlDocument(Action<HtmlDocument> load)
        {
            var htmlDocument = new HtmlDocument();

            load.Invoke(htmlDocument);

            return htmlDocument;
        }

    }
}
