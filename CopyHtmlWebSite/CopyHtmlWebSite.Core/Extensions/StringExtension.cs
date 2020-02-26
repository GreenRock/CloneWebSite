namespace CopyHtmlWebSite.Core.Extensions
{
    public static class StringExtension
    {
        public static bool HasText(this string val)
        {
            return !string.IsNullOrWhiteSpace(val);
        }

        public static bool HasNoText(this string val)
        {
            return string.IsNullOrWhiteSpace(val);
        }
    }
}