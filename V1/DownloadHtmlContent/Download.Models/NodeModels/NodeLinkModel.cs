namespace Download.Models.NodeModels
{
    public class NodeLinkModel
    {
        public string BaseLink { get; set; }
        public FolderType FolderType { get; set; }
        public string UrlInFile { get; set; }
        public string LocalLink { get; set; }
        public string OnlineLink { get; set; }
    }
}
