namespace CopyHtmlWebSite.Core.Models
{
    public interface ISelectedItem
    {
        string Id { get; set; }
        string Text { get; set; }
        string Value { get; set; }
        bool Selected { get; set; }
    }
}