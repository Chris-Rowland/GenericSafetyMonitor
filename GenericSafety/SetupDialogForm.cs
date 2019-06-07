using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.GenericSafety
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm()
        {
            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            if (!checkBoxUsePowerStatus.Checked && string.IsNullOrWhiteSpace(textBoxDataFileName.Text))
            {
                if (MessageBox.Show("No checks will be done, the monitor will always be safe.\r\nDo you want to continue?", "No Safety Tests selected", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }
            // Update the state variables with results from the dialogue
            SafetyMonitor.DataFile = textBoxDataFileName.Text;
            SafetyMonitor.SafeString = textBoxSafeString.Text;
            SafetyMonitor.UnsafeString = textBoxUnsafeString.Text;
            SafetyMonitor.usePowerStatus = checkBoxUsePowerStatus.Checked;
            SafetyMonitor.tl.Enabled = chkTrace.Checked;
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            chkTrace.Checked = SafetyMonitor.tl.Enabled;
            textBoxDataFileName.Text = SafetyMonitor.DataFile;
            textBoxSafeString.Text = SafetyMonitor.SafeString;
            textBoxUnsafeString.Text = SafetyMonitor.UnsafeString;
            checkBoxUsePowerStatus.Checked = SafetyMonitor.usePowerStatus;
            labelVersion.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); 
        }

        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Safety File";
                var dirName = textBoxDataFileName.Text;
                if (string.IsNullOrWhiteSpace(dirName))
                    dirName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\safety.txt";
                var fileName = Path.GetFileName(dirName);
                if (String.IsNullOrEmpty(Path.GetExtension(dirName)))
                    fileName = "";
                else
                    dirName = Path.GetDirectoryName(dirName);
                ofd.InitialDirectory = dirName;
                ofd.FileName = fileName;
                if ((ofd.ShowDialog()) == System.Windows.Forms.DialogResult.OK)
                {
                    textBoxDataFileName.Text = ofd.FileName;
                }
            }
        }
    }
}