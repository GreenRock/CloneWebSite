using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CopyHtmlWebSite.MainApp.ViewModels;

namespace CopyHtmlWebSite.MainApp.Views
{
    /// <summary>
    /// Interaction logic for CreateNewSiteUserControl
    /// </summary>
    public partial class CreateNewSiteUserControl : UserControl
    {
        public CreateNewSiteUserControl()
        {
            InitializeComponent();
        }

        private void TxtPage_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var pageViewModel = new PageViewModel
                {
                    PageName = TxtPageName.Text,
                    PageSource = TxtPageSource.Text
                };

                if (BtnPage.Command.CanExecute(pageViewModel))
                {
                    BtnPage.Command.Execute(pageViewModel);
                }
            }
        }
    }
}
