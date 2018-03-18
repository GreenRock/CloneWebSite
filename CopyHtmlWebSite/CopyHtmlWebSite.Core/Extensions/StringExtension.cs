namespace CopyHtmlWebSite.Core.Extensions
{
    public static class StringExtension
    {
        public static bool HasText(this string val)
        {
            return !string.IsNullOrWhiteSpace(val);
        }
    }
}