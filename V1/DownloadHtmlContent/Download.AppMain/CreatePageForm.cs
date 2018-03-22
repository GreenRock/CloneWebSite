namespace Download.AppMain
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Common.Extensions;
    using Common.Resources;
    using Download.Models.PageModels;
    using Unity;

    public partial class FrmCreatePage : Form
    {
        public delegate void CreatePageDelegate(PageModel pageModel);
        public CreatePageDelegate CreatePage;

        public delegate void SetFormTopLevelDelegate(bool isTop);
        public SetFormTopLevelDelegate FormTopLevel;

        private readonly UrlExtenstion _urlextenstion;
        private readonly RegexExtension _regexExtension;

        private BindingList<LinkModel> _bindingLink;
        private readonly IUnityContainer _unityContainer;
        public FrmCreatePage(IUnityContainer unityContainer, UrlExtenstion extenstion, RegexExtension regexExtension)
        {
            _urlextenstion = extenstion;
            _regexExtension = regexExtension;
            _unityContainer = unityContainer;

            InitializeComponent();
        }

        #region Dgv: PageList
        private void DgvListPageOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyCode != Keys.V || !keyEventArgs.Control) return;

            var dataString = Clipboard.GetData(DataFormats.Text).ToString();

            var links = dataString.Split(Environment.NewLine);
            if (!links.Any()) return;

            var datalinks = links.Where(c => !string.IsNullOrEmpty(c) && _regexExtension.IsMatch(RegexResource.Url, c))
                                 .ToList();

            foreach (var link in datalinks)
            {
                DelegateAddDataToDataSource(new LinkModel
                {
                    Link = link,
                    IsHtml = false
                });
            }
        }

        private void DgvListPageOnCellClick(object sender, DataGridViewCellEventArgs eventArgs)
        {
            DataGridView dataGridView = sender as DataGridView;

            if (dataGridView == null) return;

            var dataGridViewColumn = dataGridView.Columns["btnDelete"];
            if (dataGridViewColumn == null)
                return;

            if (eventArgs.ColumnIndex == dataGridViewColumn.Index && eventArgs.RowIndex >= 0)
            {
                var rowIndex = eventArgs.RowIndex;
                _bindingLink.RemoveAt(rowIndex);
            }
        }

        private void DgvListPageOnCellValidating(object sender, DataGridViewCellValidatingEventArgs eventArgs)
        {

            DataGridView dataGridView = sender as DataGridView;

            if (dataGridView == null) return;

            var newValue = dataGridView[eventArgs.ColumnIndex, eventArgs.RowIndex].EditedFormattedValue;

            var oldValue = eventArgs.FormattedValue;
            if (newValue == null)
            {
                eventArgs.Cancel = true;
                return;
            }

            if((string)newValue == (string)oldValue)
            {
                return;
            }

            var data = oldValue.ToString();
            var linkModelCount = _bindingLink.Count(c => c.Link == data || c.LinkName == data);
            if (linkModelCount > 0)
            {
                eventArgs.Cancel = true;
                MessageBox.Show(ContentStatic.PageNameReallyExist, ContentStatic.DuplicateData,
                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region DataSource
        private void BindingLinkOnListChanged(object sender, ListChangedEventArgs eventArgs)
        {
            if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
            {
                var listObject = sender as BindingList<LinkModel>;
                if (listObject != null)
                {
                    var newIndex = eventArgs.NewIndex;
                    var dataByIndex = listObject[newIndex];

                    var linkModelCount = listObject.Count(c => c.Link == dataByIndex.Link.Trim() || c.LinkName == dataByIndex.LinkName.Trim());
                    if (linkModelCount > 1)
                    {
                        MessageBox.Show(ContentStatic.LinkReallyExist, ContentStatic.LinkRequired,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                            listObject.RemoveAt(newIndex);
                    }
                }
            }
        }
        #endregion

        #region Button Event
        private void BtnCreatePageOnClick(object sender, EventArgs eventArgs)
        {
            var pageName = txtPageName.Text.Trim();
            if (string.IsNullOrEmpty(pageName))
            {
                MessageBox.Show(ContentStatic.PageNameRequired, ContentStatic.RequiredTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_bindingLink == null || !_bindingLink.Any())
            {
                MessageBox.Show(ContentStatic.LinkRequired, ContentStatic.RequiredTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveDataPage(pageName);

            this.Close();
        }

        private void BtnAddLinkOnClick(object sender, EventArgs eventArgs)
        {
            var link = txtLink.Text.Trim();
            var linkName = txtLinkName.Text.Trim();

            DelegateAddDataToDataSource(new LinkModel
            {
                Link = link,
                LinkName = linkName,
                IsHtml = false
            });

            ClearInput();
        }

        private void BtnAddHtmlOnClick(object sender, EventArgs eventArgs)
        {
            var formAddHtml = _unityContainer.Resolve<AddHtmlForm>();
            formAddHtml.FormTopLevel = DelegateSetFormTopLevel;
            formAddHtml.AddDataHtmlToDataSource = DelegateAddDataToDataSource;
            formAddHtml.Show();
        }
        #endregion

        #region Form Event
        private void DelegateSetFormTopLevel(bool isTop)
        {
            TopLevel = isTop;
        }

        protected override void OnLoad(EventArgs e)
        {
            btnCreatePage.Click += BtnCreatePageOnClick;
            btnAddHtml.Click += BtnAddHtmlOnClick;
            btnAddLink.Click += BtnAddLinkOnClick;

            _bindingLink = new BindingList<LinkModel>();
            _bindingLink.ListChanged += BindingLinkOnListChanged;

            dgvListPage.AutoGenerateColumns = false;
            dgvListPage.KeyDown += DgvListPageOnKeyDown;
            dgvListPage.CellClick += DgvListPageOnCellClick;
            dgvListPage.CellValidating += DgvListPageOnCellValidating;
            dgvListPage.DataSource = _bindingLink;

            FormTopLevel(false);
            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            btnCreatePage.Click -= BtnCreatePageOnClick;
            btnAddHtml.Click -= BtnAddHtmlOnClick;
            btnAddLink.Click -= BtnAddLinkOnClick;

            _bindingLink.ListChanged -= BindingLinkOnListChanged;
            _bindingLink.Clear();

            dgvListPage.KeyDown -= DgvListPageOnKeyDown;
            dgvListPage.CellClick -= DgvListPageOnCellClick;
            dgvListPage.DataSource = null;

            FormTopLevel(true);
            base.OnClosed(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_bindingLink.Count > 0)
            {
                var dialogResult = MessageBox.Show(ContentStatic.ExistFormHaveDataNotSave, ContentStatic.ConfirmExitFormTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.OK)
                {
                    SaveDataPage(DateTime.Now.ToString("hh:mm:ss"));
                }
            }
            base.OnFormClosed(e);
        }

        #endregion

        #region Private Method

        private void DelegateAddDataToDataSource(LinkModel model)
        {
            model.Link = model.Link.Trim();

            if (string.IsNullOrEmpty(model.Link))
                return;

            model.LinkName = string.IsNullOrEmpty(model.LinkName) ?
                            _urlextenstion.GetNameFromLink(model.Link) :
                            _regexExtension.Replace(RegexResource.SpecialChar, model.LinkName.Trim());

            _bindingLink.Add(model);
        }

        private void ClearInput()
        {
            txtLink.Clear();
            txtLinkName.Clear();
        }

        private void SaveDataPage(string pageName)
        {
            pageName = _regexExtension.Replace(RegexResource.SpecialChar, pageName);

            var pageModel = new PageModel
            {
                PageName = pageName,
                Links = _bindingLink.ToList()
            };

            CreatePage(pageModel);
        }
        #endregion
    }
}
