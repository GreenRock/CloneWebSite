using System.Collections.Generic;
using System.ComponentModel;

namespace Download.Models.PageModels
{
    public class PageModel
    {
        public string PageName { get; set; }
        public List<LinkModel> Links { get; set; }
        public PageStatus PageStatus { get; set; }
        public int TotalLink
        {
            get { return Links != null ? Links.Count : 0; }
        }
    }

    public enum PageStatus
    {
        Pending,
        Processing,
        Success
    }
}
