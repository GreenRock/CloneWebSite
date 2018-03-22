using System.Drawing;
using System.Windows.Forms;

namespace Download.AppMain.Extensions
{
    static class ShowMessageExtension
    {
        public static string ShowDialog(string caption, string buttonText, bool required = true)
        {
            var prompt = new Form
            {
                Width = 287,
                Height = 120,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            var textBox = new TextBox
            {
                Left = 10,
                Top = 10,
                Width = 250,
                Height = 30,
                Multiline = true,
                Font = new Font(FontFamily.GenericSansSerif, 13)
            };
            var confirmation = new Button { Text = buttonText, Left = 180, Width = 80, Top = 45, Height = 30 };

            confirmation.Click += (sender, e) => prompt.Close();

            prompt.Controls.Add(confirmation);

            prompt.Controls.Add(textBox);

            prompt.ShowDialog();

            if (string.IsNullOrEmpty(textBox.Text) && required)
            {
                ShowDialog(caption, buttonText);
            }

            return textBox.Text;
        }
    }
}
