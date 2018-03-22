using System;
using System.Windows.Forms;
using Download.Common.Resources;
using Download.Models.PageModels;

namespace Download.AppMain
{
    public partial class AddHtmlForm : Form
    {
        public delegate void SetFormTopLevelDelegate(bool isTop);
        public SetFormTopLevelDelegate FormTopLevel;

        public delegate void AddDataHtmlToDataSourceDelegate(LinkModel model);
        public AddDataHtmlToDataSourceDelegate AddDataHtmlToDataSource;
        public AddHtmlForm()
        {
            InitializeComponent();
        }

        #region Button Event
        private void BtnClearOnClick(object sender, EventArgs eventArgs)
        {
            ClearInput();
        }

        private void BtnAddOnClick(object sender, EventArgs eventArgs)
        {
            var linkName = txtLinkName.Text;
            string link = txtLink.Text;
            if (string.IsNullOrEmpty(linkName) || string.IsNullOrEmpty(link))
            {
                MessageBox.Show(ContentStatic.LinkNameRequired, ContentStatic.RequiredTitle, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var linkHtml = txtLinkHtml.Text;
            if (string.IsNullOrEmpty(linkHtml))
            {
                MessageBox.Show(ContentStatic.HtmlContentRequired, ContentStatic.RequiredTitle, MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
                return;
            }

            AddDataHtmlToDataSource(new LinkModel
                                        {
                                            LinkName = linkName,
                                            Link = link,
                                            SourcePage = linkHtml,
                                            IsHtml = true
                                        });

            ClearInput();
        }

        #endregion

        #region Form Event

        protected override void OnLoad(EventArgs e)
        {
            FormTopLevel(false);
            txtLinkHtml.MaxLength = int.MaxValue;
            btnAdd.Click += BtnAddOnClick;
            btnClear.Click += BtnClearOnClick;

            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            FormTopLevel(true);

            btnAdd.Click -= BtnAddOnClick;
            btnClear.Click -= BtnClearOnClick;

            base.OnClosed(e);
        }

        #endregion

        #region Private Method
        private void ClearInput()
        {
            txtLinkHtml.Clear();
            txtLinkName.Clear();
        }
        #endregion
    }
}
