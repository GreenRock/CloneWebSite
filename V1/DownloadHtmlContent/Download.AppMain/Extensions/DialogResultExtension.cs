using System.Windows.Forms;

namespace Download.AppMain.Extensions
{
   public static class DialogResultExtension
    {
        public static bool ResultOk(this DialogResult dialogResult)
        {
            return dialogResult == DialogResult.OK;
        }

        public static bool ResultCancel(this DialogResult dialogResult)
        {
            return dialogResult != DialogResult.OK;
        }
    }
}
