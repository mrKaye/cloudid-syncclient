using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncClientInstaller
{
    /// <summary>
    /// This form is used to call the methods for installing the application
    /// </summary>
    public partial class Install : Form
    {
        public Install()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This function is used to copy the job exe to the installation path
        /// </summary>
        //private static void CopyDirectoryFiles()
        //{
        //    DirectoryInfo dirInfo = new DirectoryInfo("Data");
        //    string srcDirectory = GlobalData.InstallationPath;
        //    string destDirectory = Path.Combine(srcDirectory, "Data");
        //    if (!Directory.Exists(destDirectory))
        //    {
        //        Directory.CreateDirectory(destDirectory);
        //    }
        //    FileInfo[] files = dirInfo.GetFiles();
        //    foreach (var info in files)
        //    {
        //        string tempPath = Path.Combine(destDirectory, info.Name);
        //        info.CopyTo(tempPath, true);
        //    }
        //}

        private void Install_Load(object sender, EventArgs e)
        {
            //installProgress.st
            txtMessage.Visible = true;
            //btnFinish.Visible = false;
            picLoad.Visible = false;
            lblWait.Visible = false;
            lblWait.Visible = false;
            if(Common.GetPreviousAppSettings() != null)
            {
                GlobalData.BackFormName = "StartForm";
            }
            else
            {
                GlobalData.BackFormName = "InstallationPath";
            }
            
            GlobalData.NextFormName = "";
            LocalizeText();
        }

        private void LocalizeText()
        {
        //    btnFinish.Text = Common.GetResourceKeyValue("CommonFinish");
            txtMessage.Text = Common.GetResourceKeyValue("InstallDescription");
            lblHeading.Text = Common.GetResourceKeyValue("InstallHeading");
        }
    }
}
