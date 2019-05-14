using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SurveyApp.Code;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SurveyApp.Forms
{
    public partial class FrmMain : RadForm
    {
        #region Global variables

        private Thread _thread;
        private bool _queryResult;
        private int _currentPageIndex;

        // General and Legal Assistance
        private string _contactMe, _q1A1, _q2A2, _q7A7;
        private string _q3C1, _q3C2, _q3C3, _q3C4, _q3C5, _q3C6, _q3C7, _q3C8;
        private string _q4C1, _q4C2, _q4C3, _q4C4;
        private string _q5C1, _q5C2, _q5C3, _q5C4, _q5C5, _q5C6, _q5C7, _q5C8, _q5C9;
        private string _q6C1, _q6C2, _q6C3, _q6C4, _q6C5, _q6C6, _q6C7, _q6C8;

        // Housing 
        private string _q8C1, _q8C2, _q8C3, _q8C4, _q8C5, _q8C6, _q8C7;
        private DateTime _q9Date;
        private string _q10A10, _q11A11, _q12A12, _q13A13, _q14A14;

        // Cost of Living
        private string _q15C1, _q15C2, _q15C3, _q15C4;

        // Health
        private string _q16A16, _q17A17, _q18A18, _q19A19, _q20A20, _q21A21, _q22A22;

        #endregion

        #region Form related & PageView

        static FrmMain()
        {
            RadTypeResolver.Instance.ResolveTypesInCurrentAssembly = true;
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            #region Crash handler event binding

            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CrashHandler;
            Application.ThreadException += CrashHandler_thread;

            #endregion
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            // Set controls settings
            radPageView1.SelectedPage = radPageViewPageGeneral;
            txtName.Focus();
            datePickerQ9.Value = DateTime.Today;
            ddlNumberOfPeople.SelectedIndex = 0;
            _currentPageIndex = radPageViewPageGeneral.TabIndex;

            // Show connection prompt
            if (File.Exists(@"C:\SurveyApp\Config.xml"))
            {
                _thread = new Thread(ServerThread);
                if (_thread != null)
                    _thread.Start();
            }
            else
            {
                ShowConnectionPrompt();
            }
        }

        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            _currentPageIndex = radPageView1.SelectedPage.TabIndex;
        }

        #endregion

        #region 1ST PAGE VIEW : GENERAL

        private void rbContactMe_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option == null) return;
            _contactMe = option.Text;
        }

        private void ddlNumberOfPeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberValidator(e, ddlNumberOfPeople, "Only digits are allowed.");
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberValidator(e, txtPhoneNumber, "Only digits are allowed as Phone Number.");
        }

        #endregion

        #region 2ND PAGE VIEW : LEGAL ASSISTANCE

        private void rbQ1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q1A1 = option.Text;
        }

        private void rbQ2_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q2A2 = option.Text;
        }

        private void chkQ3_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadCheckBox;
            if (option == null) return;

            switch (option.Name)
            {
                case "chkQ3C1":
                    OptionSaver(option, ref _q3C1);
                    break;

                case "chkQ3C2":
                    OptionSaver(option, ref _q3C2);
                    break;

                case "chkQ3C3":
                    OptionSaver(option, ref _q3C3);
                    break;

                case "chkQ3C4":
                    OptionSaver(option, ref _q3C4);
                    break;

                case "chkQ3C5":
                    OptionSaver(option, ref _q3C5);
                    break;

                case "chkQ3C6":
                    OptionSaver(option, ref _q3C6);
                    break;

                case "chkQ3C7":
                    OptionSaver(option, ref _q3C7);
                    break;

                case "chkQ3C8":
                    OptionSaver(option, ref _q3C8, txtQ3C8);
                    break;
            }
        }

        private void chkQ4_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadCheckBox;
            if (option == null) return;

            switch (option.Name)
            {
                case "chkQ4C1":
                    OptionSaver(option, ref _q4C1);
                    break;

                case "chkQ4C2":
                    OptionSaver(option, ref _q4C2);
                    break;

                case "chkQ4C3":
                    OptionSaver(option, ref _q4C3);
                    break;

                case "chkQ4C4":
                    OptionSaver(option, ref _q4C4, txtQ4C4);
                    break;
            }
        }

        private void chkQ5_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadCheckBox;
            if (option == null) return;

            switch (option.Name)
            {
                case "chkQ5C1":
                    OptionSaver(option, ref _q5C1, txtQ5C1);
                    break;

                case "chkQ5C2":
                    OptionSaver(option, ref _q5C2, txtQ5C2);
                    break;

                case "chkQ5C3":
                    OptionSaver(option, ref _q5C3, txtQ5C3);
                    break;

                case "chkQ5C4":
                    OptionSaver(option, ref _q5C4, txtQ5C4);
                    break;

                case "chkQ5C5":
                    OptionSaver(option, ref _q5C5, txtQ5C5);
                    break;

                case "chkQ5C6":
                    OptionSaver(option, ref _q5C6, txtQ5C6);
                    break;

                case "chkQ5C7":
                    OptionSaver(option, ref _q5C7, txtQ5C7);
                    break;

                case "chkQ5C8":
                    OptionSaver(option, ref _q5C8, txtQ5C8);
                    break;

                case "chkQ5C9":
                    OptionSaver(option, ref _q5C9);
                    break;
            }
        }

        private void chkQ6_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadCheckBox;
            if (option == null) return;

            switch (option.Name)
            {
                case "chkQ6C1":
                    OptionSaver(option, ref _q6C1);
                    chkQ6C8.Checked = false;
                    break;

                case "chkQ6C2":
                    OptionSaver(option, ref _q6C2);
                    chkQ6C8.Checked = false;
                    break;

                case "chkQ6C3":
                    OptionSaver(option, ref _q6C3);
                    chkQ6C8.Checked = false;
                    break;

                case "chkQ6C4":
                    OptionSaver(option, ref _q6C4);
                    chkQ6C8.Checked = false;
                    break;

                case "chkQ6C5":
                    OptionSaver(option, ref _q6C5);
                    chkQ6C8.Checked = false;
                    break;

                case "chkQ6C6":
                    OptionSaver(option, ref _q6C6);
                    chkQ6C8.Checked = false;
                    break;

                case "chkQ6C7":
                    OptionSaver(option, ref _q6C7);
                    chkQ6C8.Checked = false;
                    break;

                case "chkQ6C8":
                    if (option.IsChecked)
                    {
                        _q6C8 = option.Text;

                        // Disable other
                        chkQ6C1.Checked = chkQ6C2.Checked = chkQ6C3.Checked = false;
                        chkQ6C4.Checked = chkQ6C5.Checked = chkQ6C6.Checked = false;
                        chkQ6C7.Checked = false;

                        // Clean respective values
                        _q6C1 = _q6C2 = _q6C3 = _q6C4 = _q6C5 = _q6C6 = _q6C7 = null;
                    }

                    break;
            }
        }

        private void rbQ7_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q7A7 = option.Text;
        }

        private void txtQ5_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txtControl = sender as RadTextBox;
            NumberValidator(e, txtControl, "Only digits are allowed as amount.");
        }

        #endregion

        #region 3RD PAGE VIEW : HOUSING

        private void chkQ8_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadCheckBox;
            if (option == null) return;

            switch (option.Name)
            {
                case "chkQ8C1":
                    OptionSaver(option, ref _q8C1, txtQ8C1);
                    break;

                case "chkQ8C2":
                    OptionSaver(option, ref _q8C2, txtQ8C2);
                    break;

                case "chkQ8C3":
                    OptionSaver(option, ref _q8C3, txtQ8C3);
                    break;

                case "chkQ8C4":
                    OptionSaver(option, ref _q8C4, txtQ8C4);
                    break;

                case "chkQ8C5":
                    OptionSaver(option, ref _q8C5, txtQ8C5);
                    break;

                case "chkQ8C6":
                    OptionSaver(option, ref _q8C6, txtQ8C6);
                    break;

                case "chkQ8C7":
                    OptionSaver(option, ref _q8C7, txtQ8C7);
                    break;
            }
        }

        private void datePickerQ9_ValueChanged(object sender, EventArgs e)
        {
            _q9Date = datePickerQ9.Value;
        }

        private void rbQ10_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option == null) return;

            switch (option.Name)
            {
                case "rbQ10C1":
                    OptionSaver(option, ref _q10A10, txtQ10C1);
                    break;

                case "rbQ10C2":
                case "rbQ10C3":
                    OptionSaver(option, ref _q10A10);
                    break;
            }
        }

        private void rbQ11_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q11A11 = option.Text;
        }

        private void rbQ12_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option == null) return;

            switch (option.Name)
            {
                case "rbQ12C1":
                    OptionSaver(option, ref _q12A12, txtQ12C1);
                    break;

                case "rbQ12C2":
                case "rbQ12C3":
                    OptionSaver(option, ref _q12A12);
                    break;
            }
        }

        private void rbQ13_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q13A13 = option.Text;
        }

        private void rbQ14_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q14A14 = option.Text;
        }

        private void txtQ8_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txtControl = sender as RadTextBox;
            NumberValidator(e, txtControl, "Only digits are allowed as number of weeks.");
        }

        private void txtQ10C1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txtControl = sender as RadTextBox;
            NumberValidator(e, txtControl, "Only digits are allowed as amount.");
        }

        private void txtQ12C1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txtControl = sender as RadTextBox;
            NumberValidator(e, txtControl, "Only digits are allowed as money.");
        }

        #endregion

        #region 4TH PAGE VIEW : COST OF LIVING

        private void chkQ15_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadCheckBox;
            if (option == null) return;

            switch (option.Name)
            {
                case "chkQ15C1":
                    OptionSaver(option, ref _q15C1, txtQ15C1);
                    break;

                case "chkQ15C2":
                    OptionSaver(option, ref _q15C2, txtQ15C2);
                    break;

                case "chkQ15C3":
                    OptionSaver(option, ref _q15C3, txtQ15C3);
                    break;

                case "chkQ15C4":
                    OptionSaver(option, ref _q15C4, txtQ15C4);
                    break;
            }
        }

        private void txtQ15_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txtControl = sender as RadTextBox;
            NumberValidator(e, txtControl, "Only digits are allowed as amount.");
        }

        #endregion

        #region 5TH PAGE VIEW : HEALTH

        private void rbQ16_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option == null) return;

            switch (option.Name)
            {
                case "rbQ16C1":
                case "rbQ16C2":
                case "rbQ16C3":
                    OptionSaver(option, ref _q10A10);
                    break;

                case "rbQ16C4":
                    OptionSaver(option, ref _q16A16, txtQ16C4);
                    break;
            }
        }

        private void rbQ17_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q17A17 = option.Text;
        }

        private void rbQ18_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q18A18 = option.Text;
        }

        private void rbQ19_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q19A19 = option.Text;
        }

        private void rbQ20_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q20A20 = option.Text;
        }

        private void rbQ21_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q21A21 = option.Text;
        }

        private void rbQ22_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            var option = sender as RadRadioButton;
            if (option != null) _q22A22 = option.Text;
        }

        #endregion

        #region Button clicks

        private void btnNext_Click(object sender, EventArgs e)
        {
            var pageIndex = _currentPageIndex + 1;

            if (pageIndex < 5)
                radPageView1.SelectedPage = radPageView1.Pages[pageIndex];
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;
            lblNotice.Text = "Please Wait! It might take few minutes in saving data to database...";
            lblNotice.Image = Properties.Resources.BlackLoader;

            bgWorker.RunWorkerAsync();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            AppExitPrompt();
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            var dialogResult = RadMessageBox.Show(this, "Are you sure you want to clear the form?", "Survey App", MessageBoxButtons.YesNo, RadMessageIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                ResetForm(this, null);
            }
        }


        #endregion

        #region Office menu clicks

        void NewSurvey(object sender, EventArgs e)
        {
            var dialogResult = RadMessageBox.Show(this, "This will clear the current form, do you want to proceed?", "Survey App", MessageBoxButtons.YesNo, RadMessageIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                ResetForm(this, null);
            }
        }

        void ResetForm(object sender, EventArgs e)
        {
            // GENERAL PAGE VIEW
            txtName.Text = txtPhoneNumber.Text = txtPriorAddress.Text = txtCurrentAddress.Text = null;
            rbContactMeNo.IsChecked = rbContactMeYes.IsChecked = false;
            ddlNumberOfPeople.SelectedIndex = 0;

            // LEGAL PAGE VIEW
            // Q1
            rbQ1C1.IsChecked = rbQ1C2.IsChecked = rbQ1C3.IsChecked = rbQ1C4.IsChecked =
                rbQ1C5.IsChecked = rbQ1C6.IsChecked = false;

            // Q2
            rbQ2C1.IsChecked = rbQ2C2.IsChecked = rbQ2C3.IsChecked = rbQ2C4.IsChecked =
                rbQ2C5.IsChecked = rbQ2C6.IsChecked = rbQ2C7.IsChecked = false;

            // Q3
            chkQ3C1.Checked = chkQ3C2.Checked = chkQ3C3.Checked = chkQ3C4.Checked = chkQ3C5.Checked =
                chkQ3C6.Checked = chkQ3C7.Checked = chkQ3C8.Checked = false;
            txtQ3C8.Text = string.Empty;

            // Q4
            chkQ4C1.Checked = chkQ4C2.Checked = chkQ4C3.Checked = chkQ4C4.Checked = false;
            txtQ4C4.Text = string.Empty;

            // Q5
            chkQ5C1.Checked = chkQ5C2.Checked = chkQ5C3.Checked = chkQ5C4.Checked = chkQ5C5.Checked = chkQ5C6.Checked =
                chkQ5C7.Checked = chkQ5C8.Checked = chkQ5C9.Checked = false;
            txtQ5C1.Text = txtQ5C2.Text = txtQ5C3.Text = txtQ5C4.Text = txtQ5C5.Text = txtQ5C6.Text = txtQ5C7.Text =
                txtQ5C8.Text = string.Empty;

            // Q6
            chkQ6C1.Checked = chkQ6C2.Checked = chkQ6C3.Checked = chkQ6C4.Checked = chkQ6C5.Checked = chkQ6C6.Checked =
                chkQ6C7.Checked = chkQ6C8.Checked = false;

            // Q7
            rbQ7C1.IsChecked = rbQ7C2.IsChecked = rbQ7C3.IsChecked = rbQ7C4.IsChecked = rbQ7C5.IsChecked = false;

            // HOUSING PAGE VIEW
            // Q8
            chkQ8C1.Checked = chkQ8C2.Checked = chkQ8C3.Checked = chkQ8C4.Checked = chkQ8C5.Checked =
                chkQ8C6.Checked = chkQ8C7.Checked = false;
            txtQ8C1.Text = txtQ8C2.Text = txtQ8C3.Text = txtQ8C4.Text = txtQ8C5.Text = txtQ8C6.Text =
                txtQ8C7.Text = string.Empty;

            // Q9
            _q9Date = DateTime.Today;

            // Q10
            rbQ10C1.IsChecked = rbQ10C2.IsChecked = rbQ10C3.IsChecked = false;
            txtQ10C1.Text = string.Empty;

            // Q11
            rbQ11C1.IsChecked = rbQ11C2.IsChecked = rbQ11C3.IsChecked = false;

            // Q12
            rbQ12C1.IsChecked = rbQ12C2.IsChecked = rbQ12C3.IsChecked = false;
            txtQ12C1.Text = string.Empty;

            // Q13
            rbQ13C1.IsChecked = rbQ13C2.IsChecked = rbQ13C3.IsChecked = false;

            // Q14
            rbQ14C1.IsChecked = rbQ14C2.IsChecked = rbQ14C3.IsChecked = rbQ14C4.IsChecked = rbQ14C5.IsChecked = false;

            //COST OF LIVING PAGE VIEW
            // Q15
            chkQ15C1.Checked = chkQ15C2.Checked = chkQ15C3.Checked = chkQ15C4.Checked = false;
            txtQ15C1.Text = txtQ15C2.Text = txtQ15C3.Text = txtQ15C4.Text = string.Empty;

            // HEALTH PAGE VIEW
            // Q16
            rbQ16C1.IsChecked = rbQ16C2.IsChecked = rbQ16C3.IsChecked = rbQ16C4.IsChecked = false;
            txtQ16C4.Text = string.Empty;

            // Q17
            rbQ17C1.IsChecked = rbQ17C2.IsChecked = rbQ17C3.IsChecked = rbQ17C4.IsChecked = rbQ17C5.IsChecked = false;

            // Q18
            rbQ18C1.IsChecked = rbQ18C2.IsChecked = rbQ18C3.IsChecked = rbQ18C4.IsChecked = rbQ18C5.IsChecked = false;

            // Q19
            rbQ19C1.IsChecked = rbQ19C2.IsChecked = rbQ19C3.IsChecked = false;

            // Q20
            rbQ20C1.IsChecked = rbQ20C2.IsChecked = rbQ20C3.IsChecked = rbQ20C4.IsChecked = rbQ20C5.IsChecked = false;

            // Q21
            rbQ21C1.IsChecked = rbQ21C2.IsChecked = rbQ21C3.IsChecked = rbQ21C4.IsChecked = rbQ21C5.IsChecked = false;

            // Q22
            rbQ22C1.IsChecked = rbQ22C2.IsChecked = rbQ22C3.IsChecked = rbQ22C4.IsChecked = rbQ22C5.IsChecked = false;


            // RESET ALL VARIABLES
            _contactMe = _q1A1 = _q2A2 = _q7A7 = _q3C1 = _q3C2 = _q3C3 = _q3C4 = _q3C5 = _q3C6 = _q3C7 = _q3C8 = _q4C1 = _q4C2 = _q4C3 = _q4C4 = _q5C1 = _q5C2 = _q5C3 = _q5C4 = _q5C5 = _q5C6 = _q5C7 = _q5C8 = _q5C9 = _q6C1 = _q6C2 = _q6C3 = _q6C4 = _q6C5 = _q6C6 = _q6C7 = _q6C8 = _q8C1 = _q8C2 = _q8C3 = _q8C4 = _q8C5 = _q8C6 = _q8C7 = _q10A10 = _q11A11 = _q12A12 = _q13A13 = _q14A14 = _q15C1 = _q15C2 = _q15C3 = _q15C4 = _q16A16 = _q17A17 = _q18A18 = _q19A19 = _q20A20 = _q21A21 = _q22A22 = null;

            // Reset page view
            radPageView1.SelectedPage = radPageViewPageGeneral;
        }

        void RunScript(object sender, EventArgs e)
        {
            if (!ShowPasswordPrompt()) return;
            if (!Utility.FormExists("FrmLoading")) return;
            var frmLoading = new FrmLoading();
            frmLoading.Show();
            frmLoading.Activate();
        }

        void ShowAbout(object sender, EventArgs e)
        {
            if (!Utility.FormExists("RadAboutBox")) return;
            var about = new FrmAbout();
            about.Show();
            about.Activate();
        }

        void Exit(object sender, EventArgs e)
        {
            AppExitPrompt();
        }

        #endregion

        #region Helping Methods

        private static void OptionSaver(RadToggleButton toggleButton, ref string strName, Control radText)
        {
            if (toggleButton.IsChecked)
            {
                strName = toggleButton.Text;
                radText.Enabled = true;
            }
            else
            {
                strName = string.Empty;
                radText.Enabled = false;
                radText.Text = string.Empty;
            }
        }

        private static void OptionSaver(RadToggleButton toggleButton, ref string strName)
        {
            if (toggleButton.IsChecked)
                strName = toggleButton.Text;
        }

        private void NumberValidator(KeyPressEventArgs e, IWin32Window control, string message)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8) return;
            e.Handled = true;
            toolTip1.OwnerDraw = false;
            toolTip1.Show(message, control, 4000);
        }

        /// <summary>
        /// Convert string type value to long type. If conversion fails return null.
        /// </summary>
        /// <param name="strVal">Accepts string value to be converted</param>
        /// <returns>Return converted long value.</returns>
        private static long? TryParse(string strVal)
        {
            long val;
            return long.TryParse(strVal, out val) ? (int?)val : null;
        }

        private void AppExitPrompt()
        {
            var dialogResult = RadMessageBox.Show(this, "Are you sure you want to quit?", "Survey App", MessageBoxButtons.YesNo, RadMessageIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private bool ShowPasswordPrompt()
        {
            if (!Utility.FormExists("FrmPasser")) return false;
            using (var promptDialog = new FrmPassPrompt())
            {
                promptDialog.BringToFront();
                var dr = promptDialog.ShowDialog(this);
                return dr == DialogResult.OK;
            }
        }

        private bool ShowConnectionPrompt()
        {
            using (var promptDialog = new FrmConnectionPrompt())
            {
                promptDialog.BringToFront();
                var dr = promptDialog.ShowDialog(this);
                return dr == DialogResult.OK;
            }
        }

        private void SaveToDatabase()
        {
            #region Variables with values : To be used in parameters

            var name = String.IsNullOrEmpty(txtName.Text.Trim()) ? null : txtName.Text.Trim();
            var phone = TryParse(txtPhoneNumber.Text.Trim());
            var priorAddress = String.IsNullOrEmpty(txtPriorAddress.Text.Trim()) ? null : txtPriorAddress.Text.Trim();
            var currAddress = String.IsNullOrEmpty(txtCurrentAddress.Text.Trim()) ? null : txtCurrentAddress.Text.Trim();
            var numberOfPeople = TryParse(ddlNumberOfPeople.Text.Trim());

            var q3C8T = String.IsNullOrEmpty(txtQ3C8.Text.Trim()) ? null : txtQ3C8.Text.Trim();
            var q4C4T = String.IsNullOrEmpty(txtQ4C4.Text.Trim()) ? null : txtQ4C4.Text.Trim();

            var q5C1T = TryParse(txtQ5C1.Text.Trim());
            var q5C2T = TryParse(txtQ5C2.Text.Trim());
            var q5C3T = TryParse(txtQ5C3.Text.Trim());
            var q5C4T = TryParse(txtQ5C4.Text.Trim());
            var q5C5T = TryParse(txtQ5C5.Text.Trim());
            var q5C6T = TryParse(txtQ5C6.Text.Trim());
            var q5C7T = TryParse(txtQ5C7.Text.Trim());
            var q5C8T = TryParse(txtQ5C8.Text.Trim());

            var q8C1T = TryParse(txtQ8C1.Text.Trim());
            var q8C2T = TryParse(txtQ8C2.Text.Trim());
            var q8C3T = TryParse(txtQ8C3.Text.Trim());
            var q8C4T = TryParse(txtQ8C4.Text.Trim());
            var q8C5T = TryParse(txtQ8C5.Text.Trim());
            var q8C6T = TryParse(txtQ8C6.Text.Trim());
            var q8C7T = String.IsNullOrEmpty(txtQ8C7.Text.Trim()) ? null : txtQ8C7.Text.Trim();

            var q10C10T = TryParse(txtQ10C1.Text.Trim());
            var q12C12T = TryParse(txtQ12C1.Text.Trim());

            var q15C1T = TryParse(txtQ15C1.Text.Trim());
            var q15C2T = TryParse(txtQ15C2.Text.Trim());
            var q15C3T = TryParse(txtQ15C3.Text.Trim());
            var q15C4T = TryParse(txtQ15C4.Text.Trim());

            var q16C4T = String.IsNullOrEmpty(txtQ16C4.Text.Trim()) ? null : txtQ16C4.Text.Trim();

            #endregion

            _queryResult = DataAccess.ExecuteQuery(name, phone, priorAddress, currAddress, numberOfPeople, _contactMe, _q1A1, _q2A2, _q3C1, _q3C2, _q3C3, _q3C4, _q3C5, _q3C6, _q3C7, _q3C8, q3C8T, _q4C1, _q4C2, _q4C3, _q4C4, q4C4T, _q5C1, q5C1T, _q5C2, q5C2T, _q5C3, q5C3T, _q5C4, q5C4T, _q5C5, q5C5T, _q5C6, q5C6T, _q5C7, q5C7T, _q5C8, q5C8T, _q5C9, _q6C1, _q6C2, _q6C3, _q6C4, _q6C5, _q6C6, _q6C7, _q6C8, _q7A7, _q8C1, q8C1T, _q8C2, q8C2T, _q8C3, q8C3T, _q8C4, q8C4T, _q8C5, q8C5T, _q8C6, q8C6T, _q8C7, q8C7T, _q9Date, _q10A10, q10C10T, _q11A11, _q12A12, q12C12T, _q13A13, _q14A14, _q15C1, q15C1T, _q15C2, q15C2T, _q15C3, q15C3T, _q15C4, q15C4T, _q16A16, q16C4T, _q17A17, _q18A18, _q19A19, _q20A20, _q21A21, _q22A22);
        }

        #endregion

        #region Crash handler implementation

        private void CrashHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Utility.WriteError(e);
            RadMessageBox.Show(this, "An unhandled error of 'Program' category occured.\n\n *An entry of error had been made in log file. Consult your vendor for further assistance.", "Whoops!", MessageBoxButtons.OK, RadMessageIcon.Error);
            Application.Exit();
        }

        private void CrashHandler_thread(object sender, ThreadExceptionEventArgs e)
        {
            Utility.WriteError(e);
            RadMessageBox.Show(this, "An unhandled error of 'Thread' category occured.\n\n *An entry of error had been made in log file. Consult your vendor for further assistance.", "Oops!", MessageBoxButtons.OK, RadMessageIcon.Error);
            Application.Exit();
        }

        #endregion

        #region Threading & Background worker

        public void ServerThread()
        {
            if (!IsHandleCreated && !IsDisposed) return;
            Invoke((MethodInvoker)Utility.CreateConfigFile);
            Invoke((MethodInvoker)Utility.ReadXmlFile);
            Invoke((MethodInvoker)Utility.CreateScriptFile);
        }

        private void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SaveToDatabase();
        }

        private void bgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (_queryResult)
            {
                btnSubmit.Enabled = true;
                lblNotice.Image = null;
                lblNotice.Text = String.Empty;
                RadMessageBox.Show(this, "Data saved to database successfully!", "Data saved!", MessageBoxButtons.OK, RadMessageIcon.Info);
            }
            else
            {
                btnSubmit.Enabled = true;
                lblNotice.Image = null;
                lblNotice.Text = String.Empty;
                RadMessageBox.Show(this, "Saving data to database failed. \n\n * Don't worry for your data! We had saved your data in Script.sql file.\n You can run script from option provided in menu.", "Data saving failed", MessageBoxButtons.OK, RadMessageIcon.Error);
            }

            // Reset form to initial state
            ResetForm(this, null);
        }

        #endregion

    }
}
