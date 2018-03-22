using System.Windows.Forms;
using Download.Common.Resources;

namespace Download.AppMain.Extensions
{
    public class MessageBoxExtension
    {
        private MessageBoxExtension()
        {
            
        }
        public static DialogResult ConfirmCancel(string message)
        {
            return MessageBox.Show(ContentStatic.CancelProgramTitle, message,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        public static DialogResult MessageNotify(string message, MessageBoxIcon messageBoxIcon)
        {
            return MessageBox.Show(message, ContentStatic.TitleMessage, MessageBoxButtons.OK, messageBoxIcon);
        }
    }
}
