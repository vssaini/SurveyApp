using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SurveyApp.Code;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SurveyApp.Forms
{
    public partial class FrmConnectionPrompt : RadForm
    {
        private string _conString;
        private bool _result;

        public FrmConnectionPrompt()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            _conString = txtConString.Text;
            lblNotice.Text = "Validating and saving connection...";
            lblNotice.Image = Properties.Resources.BlackLoader;

            bgWorker.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            Close();
            Dispose();
        }

        private void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                using (var con = new SqlConnection(_conString))
                {
                    con.Open();
                    _result = con.State.Equals(ConnectionState.Open);
                    Utility.UserConnectionString = _conString;
                    Utility.CreateConfigFile();
                    Utility.CreateScriptFile();
                }
            }
            catch (Exception)
            {
                _result = false;
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            lblNotice.Text = string.Empty;
            lblNotice.Image = null;

            if (_result)
            {
                DialogResult = DialogResult.OK;
                Close();
                Dispose();
            }
            else
            {
                RadMessageBox.Show(this, "Connection cannot be established!\n\n * Please ensure SQL Server is not down and connection string is right.", "Connection failed", MessageBoxButtons.OK, RadMessageIcon.Error);
                DialogResult = DialogResult.None;
            }
        }
    }
}
