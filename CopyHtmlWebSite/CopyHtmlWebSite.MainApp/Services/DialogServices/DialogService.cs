namespace CopyHtmlWebSite.MainApp.Services.DialogServices
{
    using System.Windows.Forms;
    using Properties;

    public class DialogService : IDialogService
    {
        public string ChooseFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = Resources.DialogChooseFolderDescription;
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                   return dialog.SelectedPath;
                }
                return string.Empty;
            }
        }
    }
}