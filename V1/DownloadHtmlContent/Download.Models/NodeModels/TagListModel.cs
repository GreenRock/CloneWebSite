using System.Collections.Generic;

namespace Download.Models.NodeModels
{
    public class TagListModel<T>
    {
        public string TagName { get; set; }
        public List<T> DataList { get; set; }
    }
}
