using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Download.Common.Resources;

namespace Download.AppMain
{
    public partial class FrmAboutMe : Form
    {
        public FrmAboutMe()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            lblName.Text = AuthorResource.Name;
            lblEmail.Text = AuthorResource.Email;
            lblPhone.Text = AuthorResource.Phone;
            lblSkype.Text = AuthorResource.Skype;
            label5.Visible = false;
            base.OnLoad(e);
        }
    }
}
