using System;
using System.Collections.Generic;
using System.Linq;

namespace Download.Models
{
    public class BlackList
    {
        public static readonly List<string> IgnoreAttributes = new List<string>
        {
            "id", "class", "alt", "title", "type", "language", "style", "rel", "class", "name"
        };

        public List<string> IgnoreContent = new List<string>
        {
            "javascript:;"
        };

        public const string IconFile = ".icon";

        public static bool IgnoreList(string url)
        {
            var list = new List<string>
            {
                "fonts.googleapis.com",
                "addthis.com"
            };
            return list.Select(item => url.IndexOf(item, StringComparison.Ordinal)).Any(index => index != -1);
        }
    }
}
