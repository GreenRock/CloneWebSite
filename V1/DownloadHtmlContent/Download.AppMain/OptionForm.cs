using System;
using System.Windows.Forms;

namespace Download.AppMain
{
    public partial class OptionForm : Form
    {
        private BindingSource _bindingInput;
        public OptionForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            
            _bindingInput = new BindingSource();

            btnSave.Click  += BtnSaveOnClick;

            txtCss.DataBindings.Add("Text", _bindingInput, "CssFolder");
            txtImage.DataBindings.Add("Text", _bindingInput, "ImageFolder");
            txtScript.DataBindings.Add("Text", _bindingInput, "ScriptFolder");
            txtFont.DataBindings.Add("Text", _bindingInput, "FontFolder");

            base.OnLoad(e);
        }

        private void BtnSaveOnClick(object sender, EventArgs eventArgs)
        {
            
        }
    }
}
