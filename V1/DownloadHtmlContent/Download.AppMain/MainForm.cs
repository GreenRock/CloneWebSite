namespace Download.AppMain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Common.Extensions;
    using Common.Resources;
    using Customizes;
    using Download.Models;
    using Download.Models.NodeModels;
    using Download.Models.PageModels;
    using Download.Models.UrlModels;
    using Extensions;
    using Models;
    using Services;
    using Unity;
    public partial class FrmMain : Form
    {

        private readonly IUnityContainer _unityContainer;
        private readonly AnalyzeService _analyzeService;
        private readonly DirectoryExtension _directoryExtension;
        private readonly RegexExtension _regexExtension;
        private readonly FileExtension _fileExtension;
        private readonly UrlExtenstion _urlExtenstion;
        private readonly HandleFileService _handleFileService;
        private readonly ProjectModel _projectModel;

        private readonly PageBinding _bindingPage;
        private readonly BackgroundWorkerProgram _backgroundWorkerProgram;

        public FrmMain(IUnityContainer unityContainer,
                       AnalyzeService analyzeService,
                       DirectoryExtension directoryExtension,
                       RegexExtension regexExtension,
                       FileExtension fileExtension,
                       UrlExtenstion urlExtenstion,
                       HandleFileService handleFileService)
        {
            _unityContainer = unityContainer;
            _analyzeService = analyzeService;
            _directoryExtension = directoryExtension;
            _regexExtension = regexExtension;
            _fileExtension = fileExtension;
            _urlExtenstion = urlExtenstion;
            _handleFileService = handleFileService;
            _projectModel = new ProjectModel();
            _bindingPage = new PageBinding();
            _backgroundWorkerProgram = BackgroundWorkerInit();
            InitializeComponent();
        }

        #region Button Event
        private void BtnStartOnClick(object sender, EventArgs eventArgs)
        {
            if (!_backgroundWorkerProgram.IsBusy)
            {
                if (_bindingPage == null || !_bindingPage.Any())
                    return;

                _directoryExtension.Create(_projectModel.Location);

                List<PageModel> dataSource = _bindingPage.Where(c => c.TotalLink > 0).ToList();

                _backgroundWorkerProgram.RunWorkerAsync(dataSource);
            }
            else
            {
                DialogResult dialogResult = MessageBoxExtension.ConfirmCancel(ContentStatic.CancelProgramMessage);

                if (dialogResult.ResultOk())
                {
                    _backgroundWorkerProgram.CancelAsync();
                }

                btnStart.Enabled = false;
            }
        }

        private void BtnMoveLocationOnClick(object sender, EventArgs eventArgs)
        {
            if (_backgroundWorkerProgram.IsBusy)
                return;

            var drFindFolder = fbdMoveLocation.ShowDialog();

            if (drFindFolder.ResultCancel()) return;

            var selectedPath = fbdMoveLocation.SelectedPath;

            _projectModel.Location = selectedPath;
        }

        private void BtnAboutMeOnClick(object sender, EventArgs eventArgs)
        {
            var aboutMeForm = _unityContainer.Resolve<FrmAboutMe>();
            aboutMeForm.Show();
        }

        private void BtnCreatePageOnClick(object sender, EventArgs eventArgs)
        {
            if (_backgroundWorkerProgram != null && _backgroundWorkerProgram.IsBusy)
                return;

            var createPageForm = _unityContainer.Resolve<FrmCreatePage>();

            createPageForm.CreatePage = DelegateCreatePage;
            createPageForm.FormTopLevel = DelegateSetFormTopLevel;

            createPageForm.Show();
        }

        private void BtnClearDataOnClick(object sender, EventArgs eventArgs)
        {
            if (_backgroundWorkerProgram != null && _backgroundWorkerProgram.IsBusy)
                return;

            InitLocation();
            _bindingPage.Clear();
        }

        private void BtnExitOnClick(object sender, EventArgs eventArgs)
        {
            Close();
        }

        #endregion

        #region backgroundWorker Main

        private BackgroundWorkerProgram BackgroundWorkerInit()
        {
            var backgroundWorkerProgram = new BackgroundWorkerProgram();

            backgroundWorkerProgram.DoWork += BackgroundWorkerOnDoWork;
            backgroundWorkerProgram.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;
            backgroundWorkerProgram.ProgressChanged += BackgroundWorkerOnProgressChanged;

            return backgroundWorkerProgram;
        }

        private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            UpdateProcessBar(progressChangedEventArgs.ProgressPercentage, value => $"{value}%");
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            string message;
            MessageBoxIcon messageBoxIcon;
            if (runWorkerCompletedEventArgs.Cancelled)
            {
                message = ContentStatic.MessageCancel;
                messageBoxIcon = MessageBoxIcon.Stop;
            }
            else if (runWorkerCompletedEventArgs.Error != null)
            {
                message = ContentStatic.MessageError;
                messageBoxIcon = MessageBoxIcon.Error;
            }
            else
            {
                message = ContentStatic.MessageSuccess;
                messageBoxIcon = MessageBoxIcon.Information;
            }

            MessageBoxExtension.MessageNotify(message, messageBoxIcon);

            btnStart.Enabled = true;

            _backgroundWorkerProgram.Dispose();
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;

            if (backgroundWorker == null)
                return;

            List<PageModel> pageModel = doWorkEventArgs.Argument as List<PageModel>;
            if (pageModel == null)
            {
                backgroundWorker.CancelAsync();
                return;
            }

            UpdateTextButtonStart(ContentStatic.ButtonStop);

            var pageCount = pageModel.Count;

            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                PageModel page = pageModel[pageIndex];
                if (page == null)
                    continue;

                _bindingPage.ChangeStatus(pageIndex, PageStatus.Processing);

                RefreshdgvPageList();

                string pageName = page.PageName;

                Trace.Listeners.Add(new TextWriterTraceListener($"{pageName}.log"));
                Trace.AutoFlush = true;

                string projectCurrentPath = _analyzeService.InitFolder(pageName, _projectModel.Location);

                Trace.WriteLine(projectCurrentPath);

                List<Task<SourceAndAllLinkInPageModel>> sourceAndAllLinkInPageTask = new List<Task<SourceAndAllLinkInPageModel>>();

                for (int linkIndex = 0; linkIndex < page.TotalLink; linkIndex++)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        doWorkEventArgs.Cancel = true;
                        break;
                    }

                    LinkModel linkModel = page.Links[linkIndex];

                    var startNew = _analyzeService.DownloadPageAndGetAllLinkInPage(linkModel);

                    sourceAndAllLinkInPageTask.Add(startNew);
                }

                var whenAll = Task.WhenAll(sourceAndAllLinkInPageTask.ToArray());

                whenAll.Wait();

                SourceAndAllLinkInPageModel[] sourceAndAllLinkInPages = whenAll.Result;
                if (sourceAndAllLinkInPages == null)
                    continue;

                IEnumerable<HtmlTagModel> htmlTags = sourceAndAllLinkInPages.SelectMany(s => s.HtmlTagModels).Distinct();

                var sourceAndAllLinkInPageModel = sourceAndAllLinkInPages.FirstOrDefault();

                if (sourceAndAllLinkInPageModel == null)
                    continue;

                var result = _analyzeService.DownloadLinkFromNode(sourceAndAllLinkInPageModel.RootPage, projectCurrentPath, htmlTags, sourceAndAllLinkInPageModel.PageLink);

                IEnumerable<NodeLinkModel> nodeLinks = result.Where(c => c != null);

                List<Task> savePageTasks = new List<Task>();

                foreach (var currentPage in sourceAndAllLinkInPages)
                {
                    foreach (var nodeLinkModel in nodeLinks)
                    {
                        currentPage.HtmlPage = currentPage.HtmlPage.Replace(nodeLinkModel.BaseLink,
                            nodeLinkModel.UrlInFile);
                    }

                    string pagecurrentName = Path.Combine(projectCurrentPath, currentPage.PageName + ".html");

                    currentPage.HtmlPage = _regexExtension.Replace(RegexResource.BaseTag, currentPage.HtmlPage);

                    var savePageTask = _fileExtension.WriteAsync(pagecurrentName, currentPage.HtmlPage);

                    savePageTasks.Add(savePageTask);
                }

                Task.WaitAll(savePageTasks.ToArray());

                List<NodeLinkModel> nodeCssLinks = nodeLinks.Where(c => c.FolderType == FolderType.Css).ToList();

                List<Task<IEnumerable<NodeLinkModel>>> tasklist = new List<Task<IEnumerable<NodeLinkModel>>>();

                foreach (var nodeLink in nodeCssLinks)
                {
                    AnalysisUrlModel rootPageCurrent = _urlExtenstion.DetectRoot(nodeLink.OnlineLink);

                    var task = _handleFileService.HandleCss(nodeLink.LocalLink, rootPageCurrent, projectCurrentPath, nodeLink.PageLink);

                    tasklist.Add(task);
                }

                Task.WhenAll(tasklist.ToArray()).Wait();

                var cssFiles = tasklist.SelectMany(s => s.Result).Where(w => w != null && w.FolderType == FolderType.Css).ToList();

                if (cssFiles.Any())
                {
                    List<Task<IEnumerable<NodeLinkModel>>> tasklistcssFile = new List<Task<IEnumerable<NodeLinkModel>>>();

                    foreach (var cssFile in cssFiles.Where(w => w != null && !string.IsNullOrEmpty(w.OnlineLink)))
                    {
                        AnalysisUrlModel rootPageCurrent = _urlExtenstion.DetectRoot(cssFile.OnlineLink);

                        var task = _handleFileService.HandleCss(cssFile.LocalLink, rootPageCurrent, projectCurrentPath, cssFile.PageLink);

                        tasklistcssFile.Add(task);
                    }

                    Task.WhenAll(tasklist.ToArray()).Wait();
                }

                double j = pageIndex + 1;

                double percentProgress = Math.Round(j / pageCount * 100, MidpointRounding.AwayFromZero);

                _bindingPage.ChangeStatus(pageIndex, PageStatus.Success);

                RefreshdgvPageList();

                backgroundWorker.ReportProgress((int)percentProgress);
            }
            UpdateTextButtonStart(ContentStatic.ButtonStart);

            doWorkEventArgs.Result = true;
        }

        #endregion

        #region ProcessBar

        private void UpdateProcessBar(int value, Func<int, string> message)
        {
            pbProcess.Value = value;
            pbProcess.ForeColor = Color.Crimson;
            pbProcess.Text = message.Invoke(value);
        }

        #endregion

        #region Dgv: ListPage
        private void DgvPageListOnCellValidating(object sender, DataGridViewCellValidatingEventArgs eventArgs)
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

            if ((string)newValue == (string)oldValue)
            {
                return;
            }

            var data = oldValue.ToString();
            var linkModelCount = _bindingPage.Count(c => c.PageName == data);
            if (linkModelCount > 0)
            {
                eventArgs.Cancel = true;
                MessageBox.Show(ContentStatic.LinkReallyExist, ContentStatic.DuplicateData,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgvListPageOnCellClick(object sender, DataGridViewCellEventArgs eventArgs)
        {
            if (_backgroundWorkerProgram != null && _backgroundWorkerProgram.IsBusy)
                return;

            DataGridView dataGridView = sender as DataGridView;

            if (dataGridView == null) return;

            var dataGridViewColumn = dataGridView.Columns["btnDelete"];
            if (dataGridViewColumn == null)
                return;

            if (eventArgs.ColumnIndex == dataGridViewColumn.Index && eventArgs.RowIndex >= 0)
            {
                var rowIndex = eventArgs.RowIndex;
                _bindingPage.RemoveAt(rowIndex);
            }
        }

        private void DgvPageListOnCellFormatting(object sender, DataGridViewCellFormattingEventArgs dataGridViewCellFormattingEventArgs)
        {
            DataGridView dataGridView = sender as DataGridView;

            if (dataGridView == null) return;

            var colIndex = dataGridViewCellFormattingEventArgs.ColumnIndex;
            var rowIndex = dataGridViewCellFormattingEventArgs.RowIndex;

            if (rowIndex >= 0 && colIndex >= 0)
            {
                var theRow = dataGridView.Rows[rowIndex];

                var statusCellValue = theRow.Cells[2].Value;
                if (statusCellValue != null)
                {
                    var statusCell = statusCellValue.ToString().ConvertValueToEnum<PageStatus>();
                    switch (statusCell)
                    {
                        case PageStatus.Processing:
                            theRow.DefaultCellStyle.BackColor = Color.Red;
                            break;
                        case PageStatus.Success:
                            theRow.DefaultCellStyle.BackColor = Color.Lime;
                            break;
                    }
                }

            }
        }
        #endregion

        #region Form Event
        private void DelegateSetFormTopLevel(bool isTop)
        {
            TopLevel = isTop;
        }

        protected override void OnLoad(EventArgs e)
        {
            InitLocation();

            btnAboutMe.Click += BtnAboutMeOnClick;
            btnMoveLocation.Click += BtnMoveLocationOnClick;
            btnStart.Click += BtnStartOnClick;
            btnCreatePage.Click += BtnCreatePageOnClick;
            btnClearData.Click += BtnClearDataOnClick;
            btnExit.Click += BtnExitOnClick;

            dgvPageList.AutoGenerateColumns = false;
            dgvPageList.DataSource = _bindingPage;
            dgvPageList.CellClick += DgvListPageOnCellClick;
            dgvPageList.CellValidating += DgvPageListOnCellValidating;
            dgvPageList.CellFormatting += DgvPageListOnCellFormatting;

            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            btnAboutMe.Click -= BtnAboutMeOnClick;
            btnMoveLocation.Click -= BtnMoveLocationOnClick;
            btnStart.Click -= BtnStartOnClick;
            btnCreatePage.Click -= BtnCreatePageOnClick;

            _bindingPage.Clear();

            dgvPageList.DataSource = null;
            dgvPageList.CellClick -= DgvListPageOnCellClick;
            dgvPageList.CellValidating -= DgvPageListOnCellValidating;
            dgvPageList.CellFormatting -= DgvPageListOnCellFormatting;
            txtLocation.DataBindings.Clear();

            base.OnClosed(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var dialogResult = MessageBox.Show(ContentStatic.ExitFormMessage, ContentStatic.ExitFormTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            e.Cancel = dialogResult != DialogResult.OK;

            base.OnFormClosing(e);
        }
        #endregion

        #region Private Method
        private void InitLocation()
        {
            txtLocation.Font = new Font(FontFamily.GenericSansSerif, 10);

            txtLocation.DataBindings.Add("Text", _projectModel, nameof(ProjectModel.Location), false,
                DataSourceUpdateMode.OnPropertyChanged);

        }

        private void UpdateTextButtonStart(string text)
        {
            btnStart.SafeUpdate(() =>
            {
                btnStart.Text = text;
            });
        }

        private void RefreshdgvPageList()
        {
            btnStart.SafeUpdate(() =>
            {
                dgvPageList.Refresh();
            });
        }

        #endregion

        #region Page
        private void DelegateCreatePage(PageModel pageModel)
        {
            _bindingPage.Add(pageModel);
        }
        #endregion
    }
}
