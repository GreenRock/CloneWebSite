using System;
using System.ComponentModel;

namespace Download.AppMain.Customizes
{
    public class BackgroundWorkerProgram : BackgroundWorker
    {
        public BackgroundWorkerProgram()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            base.OnDoWork(e);
        }

        protected override void OnProgressChanged(ProgressChangedEventArgs e)
        {
            base.OnProgressChanged(e);
        }

        protected override void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            base.OnRunWorkerCompleted(e);
        }

        public bool IsRunning()
        {
            return this.IsBusy;
        }
    }
}
