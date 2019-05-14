using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SurveyApp.Code;

namespace SurveyApp.Forms
{
    public partial class FrmLoading : Form
    {
        public FrmLoading()
        {
            InitializeComponent();

            radWaitingBar1.StartWaiting();
        }

        private void FrmLoading_Shown(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }

        #region Background thread

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000); // Default value for showing progress
            Invoke((MethodInvoker)Utility.RunScriptFile);
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Hide current form
            radWaitingBar1.StopWaiting();
            Close();
        }

        #endregion
    }
}
