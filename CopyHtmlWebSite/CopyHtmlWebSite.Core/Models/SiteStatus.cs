namespace CopyHtmlWebSite.Core.Models
{
    public enum SiteStatus
    {
        Pending,
        BeginLoadResource,
        EndLoadResource,
        RemoveBlackList,
        DownloadCss,
        DownloadJs,
        DownloadImage,
        ReplaceCssInHtml,
        ReplaceJsInHtml,
        ReplaceImageInHtml,
        GetImageInCssFile,
        Done,
        Error,
        ReplaceImageInCssFile,
        SaveCss,
        SaveJs,
        SaveImage,
        PrepareToSave,
        DownloadImageInCssFile
    }
}