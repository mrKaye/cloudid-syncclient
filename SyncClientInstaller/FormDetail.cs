using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncClientInstaller
{
    public class FormDetail
    {
        public static void OpenWelcomeForm()
        {
            frmWelcome welcomeFrm = new frmWelcome();
            welcomeFrm.MdiParent = GlobalData.FrmName;
            welcomeFrm.Dock = DockStyle.Fill;
            welcomeFrm.Show();
        }

        public static void OpenApiKeyForm()
        {
            APIKeyValidator apiKeyFrm = new APIKeyValidator();
            apiKeyFrm.MdiParent = GlobalData.FrmName;
            apiKeyFrm.Dock = DockStyle.Fill;
            apiKeyFrm.txtApiKey.Text = string.IsNullOrEmpty(Common.GetAppSettingdataValue("ApiKey")) ? "" : Common.GetAppSettingdataValue("ApiKey");
            apiKeyFrm.Show();
        }

        public static void OpenCustomerShortCodeForm()
        {
            CustomerValidation custFrm = new CustomerValidation();
            custFrm.MdiParent = GlobalData.FrmName;
            custFrm.Dock = DockStyle.Fill;
            var preVersionAppSettings = Common.GetPreviousAppSettings();
            if (preVersionAppSettings == null)
            {
                custFrm.txtCustomerName.Text = string.IsNullOrEmpty(Common.GetAppSettingdataValue("CustomerShortCode")) ? "" : Common.GetAppSettingdataValue("CustomerShortCode");
            }
            else
            {
                custFrm.txtCustomerName.Text = preVersionAppSettings["CustomerShortCode"].Value;
                custFrm.txtCustomerName.Enabled = false;
            }
            custFrm.txtApiKey.Text = string.IsNullOrEmpty(Common.GetAppSettingdataValue("ApiKey")) ? "" : Common.GetAppSettingdataValue("ApiKey");
            custFrm.Show();
        }

        public static void OpenUserDetailsForm()
        {
            UserDetails userFrm = new UserDetails();
            userFrm.MdiParent = GlobalData.FrmName;
            userFrm.Dock = DockStyle.Fill;
            userFrm.txtUserName.Text = string.IsNullOrEmpty(Common.GetAppSettingdataValue("UserName")) ? "" : Common.GetAppSettingdataValue("UserName");
            userFrm.txtPassword.Text = string.IsNullOrEmpty(Common.GetAppSettingdataValue("Password")) ? "" : Common.GetAppSettingdataValue("Password");
            userFrm.Show();
        }

        public static void OpenLDAPPathForm()
        {
            LDAPPath ldapPathFrm = new LDAPPath();
            if(GlobalData.IsCheckBoxChecked)
            {
                ldapPathFrm.chkEnableADServerName.Checked = true;
                ldapPathFrm.txtLdapPathServerName.Text = Common.GetAppSettingdataValue("ADServerName");
                ldapPathFrm.txtOuPathServerName.Text = Common.GetAppSettingdataValue("ADServerName");
                ldapPathFrm.txtLdapPathServerName.Enabled = ldapPathFrm.txtOuPathServerName.Enabled = true;
                ldapPathFrm.txtAdServerUsername.Visible = ldapPathFrm.txtAdServerPassword.Visible = true;
                ldapPathFrm.txtAdServerUsername.Text = Common.GetAppSettingdataValue("ADServerUserName");
                ldapPathFrm.txtAdServerPassword.Text = Common.GetAppSettingdataValue("ADServerPassword");
            }
            ldapPathFrm.MdiParent = GlobalData.FrmName;
            ldapPathFrm.Dock = DockStyle.Fill;
            ldapPathFrm.txtLDAPPath.Text = string.IsNullOrEmpty(GlobalData.LdapString) ? "" : GlobalData.LdapString;
            ldapPathFrm.txtLDAPSecurityGroup.Text = string.IsNullOrEmpty(GlobalData.OUString) ? "" : GlobalData.OUString;
            ldapPathFrm.Show();
        }

        public static void OpenCustomerEmailForm()
        {
            CustomerEmail custEmailFrm = new CustomerEmail();
            custEmailFrm.MdiParent = GlobalData.FrmName;
            custEmailFrm.Dock = DockStyle.Fill;
            custEmailFrm.txtCustomerEmail.Text = string.IsNullOrEmpty(Common.GetAppSettingdataValue("NotificationEmail")) ? "" : Common.GetAppSettingdataValue("NotificationEmail");
            custEmailFrm.Show();
        }

        public static void OpenInstallationPathForm()
        {
            InstallationPath installPathFrm = new InstallationPath();
            installPathFrm.MdiParent = GlobalData.FrmName;
            installPathFrm.Dock = DockStyle.Fill;
            string previousTaskPath = Scheduler.GetScheduledTaskPath("SyncClientApp");
            if (string.IsNullOrEmpty(previousTaskPath))
            {
                installPathFrm.txtInstallationPath.Text = string.IsNullOrEmpty(GlobalData.InstallationPath) ? "" : GlobalData.InstallationPath;
            }
            else
            {
                installPathFrm.txtInstallationPath.Text = System.IO.Directory.GetParent(System.IO.Path.GetDirectoryName(previousTaskPath)).FullName;
                installPathFrm.btnBrowse.Visible = false;
                installPathFrm.txtInstallationPath.ReadOnly = true;
            }
            installPathFrm.Show();
        }

        public static void OpenInstallForm()
        {
            Install installFrm = new Install();
            installFrm.MdiParent = GlobalData.FrmName;
            installFrm.Dock = DockStyle.Fill;
            installFrm.Show();
        }
    }
}
