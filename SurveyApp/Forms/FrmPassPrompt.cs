using System;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SurveyApp.Forms
{
    public partial class FrmPassPrompt : RadForm
    {
        public FrmPassPrompt()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidatePassword(txtPassword.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
                Dispose();
            }
            else
            {
                RadMessageBox.Show(this, "Password not matched. Please try again!", "Verification failed", MessageBoxButtons.OK, RadMessageIcon.Error);
                DialogResult = DialogResult.None;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            Close();
            Dispose();
        }

        private static bool ValidatePassword(string uDate)
        {
            // 8/28/2013 should be format for password
            var todayDate = DateTime.Today.ToShortDateString();
            return uDate.Equals(todayDate);
        }
    }
}
