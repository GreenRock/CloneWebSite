using System;
using System.Drawing;
using System.Windows.Forms;

namespace Download.AppMain.Extensions
{
    static class ControlExtension
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

        public static void SafeUpdate(this Control control, Action update)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate { update(); }));
            }
            else
            {
                update();
            }
        }

        public static void ChangeStatus(this Button button)
        {
            button.Enabled = !button.Enabled;
        }
    }
}
