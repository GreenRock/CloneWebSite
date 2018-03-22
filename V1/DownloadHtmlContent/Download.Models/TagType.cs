using System.ComponentModel;

namespace Download.Models
{
    public enum TagType
    {
        [Description("script")]
        Javascript,
        [Description("link")]
        Css,
        [Description("img")]
        Image,
        [Description("a")]
        TagA,
        [Description("meta")]
        Meta,
        [Description("div")]
        Div,
        [Description("style")]
        Style
    }
}
