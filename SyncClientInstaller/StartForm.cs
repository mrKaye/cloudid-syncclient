using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;

namespace SyncClientInstaller
{
    public partial class frmWelcome : Form
    {
        public frmWelcome()
        {
            InitializeComponent();
        }
      
        private void rbtnEnglish_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalData.Language = "en-US";
            LocalizeText();
        }

        private void rbtnNorsk_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalData.Language = "nb-NO";
            LocalizeText();
        }

        private void LocalizeText()
        {
            lblWelcome.Text = Common.GetResourceKeyValue("StartFormHeading");
            lblChangeLanguage.Text = Common.GetResourceKeyValue("CommonChangeLanguage");
            txtDescription.Text = Common.GetResourceKeyValue("StartFormDescription");
            rbtnEnglish.Text = Common.GetResourceKeyValue("CommonEnglish");
            rbtnNorsk.Text = Common.GetResourceKeyValue("CommonNorsk");
            ((SyncClientApp)this.MdiParent).btnNext.Text = Common.GetResourceKeyValue("CommonNext");
            ((SyncClientApp)this.MdiParent).btnBack.Text = Common.GetResourceKeyValue("CommonPrevious");
            ((SyncClientApp)this.MdiParent).btnCancel.Text = Common.GetResourceKeyValue("CommonCancel");
            ((SyncClientApp)this.MdiParent).lnkLblWelcome.Text = Common.GetResourceKeyValue("Welcome");
            //((SyncClientApp)this.MdiParent).lnkLblApiKey.Text = Common.GetResourceKeyValue("ApiKey");
            ((SyncClientApp)this.MdiParent).lnkLblCustomerCode.Text = Common.GetResourceKeyValue("CustomerCode");
            ((SyncClientApp)this.MdiParent).lnkLblUserDetails.Text = Common.GetResourceKeyValue("UserDetail");
            ((SyncClientApp)this.MdiParent).lnkLblCustomerEmail.Text = Common.GetResourceKeyValue("CustomerEmail");
            ((SyncClientApp)this.MdiParent).lnkLblLDAPConfiguration.Text = Common.GetResourceKeyValue("LDAPConfiguration");
            ((SyncClientApp)this.MdiParent).lnkLblInstallationPath.Text = Common.GetResourceKeyValue("InstallationPath");
            ((SyncClientApp)this.MdiParent).lnkLblInstall.Text = Common.GetResourceKeyValue("Install");
        }

        private void frmWelcome_Load(object sender, EventArgs e)
        {
            LocalizeText();
            GlobalData.BackFormName = "StartForm";
            if (Common.GetPreviousAppSettings() != null)
            {
                GlobalData.NextFormName = "Install";
            }
            else
            {
                GlobalData.NextFormName = "CustomerValidation";
            }            

            if(GlobalData.Language == "en-US")
            {
                rbtnEnglish.Checked = true;
                rbtnNorsk.Checked = false;
            }
            else
            {
                rbtnEnglish.Checked = false;
                rbtnNorsk.Checked = true;
            }
            //Common.GetAllUsers();
        }
    }
}
