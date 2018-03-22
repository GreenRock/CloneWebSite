using System.Collections.Generic;
using Download.Models.NodeModels;
using Download.Models.UrlModels;

namespace Download.AppMain.Models
{
    public class SourceAndAllLinkInPageModel
    {
        public string PageName { get; set; }
        public string HtmlPage { get; set; }
        public AnalysisUrlModel RootPage { get; set; }
        public IEnumerable<HtmlTagModel> HtmlTagModels { get; set; }
        public string PageLink { get; set; }
    }
}
