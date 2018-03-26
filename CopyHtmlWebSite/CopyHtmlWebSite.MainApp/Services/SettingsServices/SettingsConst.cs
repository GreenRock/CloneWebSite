namespace CopyHtmlWebSite.MainApp.Services.SettingsServices
{
    using Properties;

    public struct SettingsConst
    {
        public const string SaveTo = nameof(Settings.SaveTo);
        public struct Folder
        {
            public const string CssFolder = "Css";
            public const string JsFolder = "Js";
            public const string ImageFolder = "Image";
            public const string FontFolder = "Font";
        }
    }
}