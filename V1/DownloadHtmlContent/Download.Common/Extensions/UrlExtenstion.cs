using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Download.Common.Resources;
using Download.Models.UrlModels;

namespace Download.Common.Extensions
{
    public class UrlExtenstion
    {
        private readonly RegexExtension _regexExtension;

        public UrlExtenstion(RegexExtension regexExtension)
        {
            _regexExtension = regexExtension;
        }

        public  string BestUrl(string url)
        {
            var regex = new Regex(RegexResource.FlashAfterAndBeforeUrl);

            url = regex.Replace(url, "");

            var indexChar = url.IndexOf('?');
            if (indexChar != -1)
            {
                url = url.Substring(0, indexChar);
            }

            int indexQuestion = url.IndexOf("#", StringComparison.Ordinal);

            if (indexQuestion != -1)
            {
                url = url.Substring(0, indexQuestion);
            }
            return url;
        }
        public bool IsUrl(string url)
        {
            var regx = new Regex(RegexResource.Url);
            var isRegex = regx.IsMatch(url);
            return isRegex;
        }
        public  string GetFileNameFromUrl(string url, bool client = false, bool isSuffix = true)
        {
            var splitChar = !client ? '/' : '\\';
            var strings = url.Split(splitChar).Where(c => !string.IsNullOrEmpty(c)).ToArray();
            if (strings.Length == 0)
                return string.Empty;

            var fileNameFromUrl = strings[strings.Length - 1].ToString(CultureInfo.InvariantCulture);
            if (isSuffix)
            {
                fileNameFromUrl = BestUrl(fileNameFromUrl);
            }
           
            return fileNameFromUrl;
        }

        public string GetNameFromLink(string url)
        {
            var fileNameFromUrl = GetFileNameFromUrl(url, isSuffix: false);

            if(string.IsNullOrEmpty(fileNameFromUrl))
                return string.Empty;

            return _regexExtension.Replace(RegexResource.SpecialChar, fileNameFromUrl);
        }
        public AnalysisUrlModel DetectRoot(string url)
        {
            var urlPage = new Uri(url);
            var segments = urlPage.Segments;
            var rootPage = $"{urlPage.Scheme}://{urlPage.Authority}{segments[0]}";
            return new AnalysisUrlModel
            {
                RootPage = rootPage,
                Segments = segments
            };
        }
    }
}
