using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncClientInstaller
{
    public partial class InstallationPath : Form
    {
        public InstallationPath()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Select the installation path";
            folderDialog.ShowNewFolderButton = true;
            DialogResult folderDialogResult = folderDialog.ShowDialog();
            if(folderDialogResult == DialogResult.OK)
            {
                string path = folderDialog.SelectedPath;
                if (System.IO.Directory.Exists(path))
                {
                    GlobalData.InstallationPath = path;
                    txtInstallationPath.Text = path;
                }
                else
                {
                    MessageBox.Show("Invalid Path. Please enter a valid path.");
                    return;
                }
            }            
        }

        private void LocalizeText()
        {
            lblHeading.Text = Common.GetResourceKeyValue("InstallationPathFrmHeading");
            txtDescription.Text = Common.GetResourceKeyValue("InstallationPathFrmDescription");
            btnBrowse.Text = Common.GetResourceKeyValue("InstallationPathFrmBrowse");
            lblInstallPath.Text = Common.GetResourceKeyValue("SelectPath");
        }

        private void InstallationPath_Load(object sender, EventArgs e)
        {
            LocalizeText();
            GlobalData.BackFormName = "CustomerEmail";
            GlobalData.NextFormName = "Install";
        }
    }
}
