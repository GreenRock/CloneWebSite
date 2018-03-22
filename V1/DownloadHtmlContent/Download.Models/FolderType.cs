using System.ComponentModel;

namespace Download.Models
{
    public enum FolderType
    {
        [Description("Css")]
        Css,
        [Description("Scripts")]
        Scripts,
        [Description("Images")]
        Images,
        [Description("Fonts")]
        Fonts
    }
}
