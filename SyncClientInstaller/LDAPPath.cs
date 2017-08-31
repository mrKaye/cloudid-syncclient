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
    public partial class LDAPPath : Form
    {
        public LDAPPath()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This function will fetch the data from the resource file and update the form
        /// </summary>
        private void LocalizeText()
        {
            lblHeading.Text = Common.GetResourceKeyValue("LDAPConfiguration");
            txtDescription.Text = Common.GetResourceKeyValue("LDAPPathDescription");
            lblLDAPPath.Text = Common.GetResourceKeyValue("LDAPPath");
            lblLDAPSecurityGroup.Text = Common.GetResourceKeyValue("LDAPSecurityGroup");
            chkEnableADServerName.Text = Common.GetResourceKeyValue("EnableADServerText");
            txtLdapPathServerName.Text = string.IsNullOrEmpty(Common.GetAppSettingdataValue("ADServerName")) ? Common.GetResourceKeyValue("ServerName") : Common.GetAppSettingdataValue("ADServerName");
            txtOuPathServerName.Text = string.IsNullOrEmpty(Common.GetAppSettingdataValue("ADServerName")) ? Common.GetResourceKeyValue("ServerName") : Common.GetAppSettingdataValue("ADServerName");
            lblAdServerUserName.Text = Common.GetResourceKeyValue("ADServerUsername");
            lblAdServerPassword.Text = Common.GetResourceKeyValue("ADServerPassword");
        }

        private void LDAPPath_Load(object sender, EventArgs e)
        {
            //Loading the form with localized data
            LocalizeText();
            GlobalData.BackFormName = "UserDetails";
            GlobalData.NextFormName = "CustomerEmail";
        }

        private void chkEnableADServerName_CheckedChanged(object sender, EventArgs e)
        {
            txtLdapPathServerName.Enabled = chkEnableADServerName.Checked;
            txtOuPathServerName.Enabled = chkEnableADServerName.Checked;
            lblAdServerUserName.Visible = chkEnableADServerName.Checked;
            lblAdServerPassword.Visible = chkEnableADServerName.Checked;
            txtAdServerUsername.Visible = chkEnableADServerName.Checked;
            txtAdServerPassword.Visible = chkEnableADServerName.Checked;
            if(!chkEnableADServerName.Checked)
            {
                txtLdapPathServerName.Text = Common.GetResourceKeyValue("ServerName");
                txtOuPathServerName.Text = Common.GetResourceKeyValue("ServerName");
            }
        }

        private void txtLdapPathServerName_Enter(object sender, EventArgs e)
        {
            if (txtLdapPathServerName.Text == Common.GetResourceKeyValue("ServerName"))
            {
                txtLdapPathServerName.Text = "";
                txtOuPathServerName.Text = "";
            }
        }

        private void txtOuPathServerName_Enter(object sender, EventArgs e)
        {
            if (txtOuPathServerName.Text == Common.GetResourceKeyValue("ServerName"))
            {
                txtLdapPathServerName.Text = "";
                txtOuPathServerName.Text = "";
            }
        }

        private void txtLdapPathServerName_TextChanged(object sender, EventArgs e)
        {
            txtOuPathServerName.Text = txtLdapPathServerName.Text;
        }

        private void txtOuPathServerName_TextChanged(object sender, EventArgs e)
        {
            txtLdapPathServerName.Text = txtOuPathServerName.Text;
        }
    }
}
