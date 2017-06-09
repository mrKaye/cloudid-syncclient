using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;

namespace SyncClientInstaller
{
    public partial class SyncClientApp : Form
    {
        public SyncClientApp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is called when application is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyncClientApp_Load(object sender, EventArgs e)
        {
            GlobalData.Language = "nb-NO";
            GlobalData.FrmName = this;
            GlobalData.LinkNumber = 1;
            frmWelcome frmStart = new frmWelcome();
            frmStart.MdiParent = GlobalData.FrmName;
            frmStart.Dock = DockStyle.Fill;
            frmStart.Show();
            LocalizeText();
            btnBack.Enabled = false;
            InitialConfiguration();
            if (Common.GetPreviousAppSettings() != null)
            {
                EnableDsableLinks(false);
                lnkLblWelcome.Enabled = true;
                lnkLblInstall.Enabled = true;
                GlobalData.InstallationPath = System.IO.Directory.GetParent(System.IO.Path.GetDirectoryName(Scheduler.GetScheduledTaskPath("SyncClientApp"))).FullName;
            }
        }

        private void InitialConfiguration()
        {
            Common.AddAppSettingData("SyncOption", "userPrincipalName");
            Common.AddAppSettingData("Version", System.Reflection.Assembly.GetAssembly(typeof(SyncClientApp)).GetName().Version.ToString());
            Common.AddAppSettingData("sAMAccountName", "true");
            Common.AddAppSettingData("displayName", "true");
            Common.AddAppSettingData("sn", "true");
            Common.AddAppSettingData("givenName", "true");

            Common.AddAppSettingData("description", "true");
            Common.AddAppSettingData("l", "true");
            Common.AddAppSettingData("streetAddress", "true");
            Common.AddAppSettingData("postalCode", "true");
            Common.AddAppSettingData("mail", "true");
            Common.AddAppSettingData("telephoneNumber", "true");
            Common.AddAppSettingData("mobile", "true");
        }
        /// <summary>
        /// This method is used for localizing the common texts
        /// </summary>
        private void LocalizeText()
        {
            btnNext.Text = Common.GetResourceKeyValue("CommonNext");
            btnBack.Text = Common.GetResourceKeyValue("CommonPrevious");
            btnCancel.Text = Common.GetResourceKeyValue("CommonCancel");
            lnkLblWelcome.Text = Common.GetResourceKeyValue("Welcome");
            lnkLblCustomerCode.Text = Common.GetResourceKeyValue("CustomerCode");
            lnkLblUserDetails.Text = Common.GetResourceKeyValue("UserDetail");
            lnkLblLDAPConfiguration.Text = Common.GetResourceKeyValue("LDAPConfiguration");
            lnkLblCustomerEmail.Text = Common.GetResourceKeyValue("CustomerEmail");
            lnkLblInstallationPath.Text = Common.GetResourceKeyValue("InstallationPath");
            lnkLblInstall.Text = Common.GetResourceKeyValue("Install");
        }

        /// <summary>
        /// This event is called when the form is being closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyncClientApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!GlobalData.IsBtnClose)
            {
                DialogResult result = MessageBox.Show(Common.GetResourceKeyValue("ClosingText"), "Close", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Cancel button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Common.GetResourceKeyValue("ClosingText"), Common.GetResourceKeyValue("CommonConfirmation"), MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                GlobalData.IsBtnClose = true;
                Application.Exit();
            }
        }

        /// <summary>
        /// Back button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            //Disabling the back button if it is a start form
            if (GlobalData.BackFormName == "StartForm")
                btnBack.Enabled = false;
            else
                btnBack.Enabled = true;

            btnNext.Text = Common.GetResourceKeyValue("CommonNext");

            if (!string.IsNullOrEmpty(GlobalData.BackFormName))
            {
                //loading the appropriate form based on the form name
                switch (GlobalData.BackFormName)
                {
                    case "StartForm":
                        if (Common.GetPreviousAppSettings() != null)
                        {
                            ((Install)this.MdiChildren[0]).Close();
                        }
                        else
                        {
                            ((CustomerValidation)this.MdiChildren[0]).Close();
                        }
                        FormDetail.OpenWelcomeForm();
                        break;
                    case "CustomerValidation":
                        ((UserDetails)this.MdiChildren[0]).Close();
                        FormDetail.OpenCustomerShortCodeForm();
                        break;
                    case "UserDetails":
                        ((LDAPPath)this.MdiChildren[0]).Close();
                        FormDetail.OpenUserDetailsForm();
                        break;
                    case "LDAPPath":
                        ((CustomerEmail)this.MdiChildren[0]).Close();
                        FormDetail.OpenLDAPPathForm();
                        break;
                    case "CustomerEmail":
                        ((InstallationPath)this.MdiChildren[0]).Close();
                        FormDetail.OpenCustomerEmailForm();
                        break;
                    case "InstallationPath":
                        ((Install)this.MdiChildren[0]).Close();
                        FormDetail.OpenInstallationPathForm();
                        break;
                    case "Install":
                        FormDetail.OpenInstallForm();
                        break;
                }
            }

        }

        /// <summary>
        /// Next button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Disabling the back button if the form is start form
            if (GlobalData.NextFormName == "StartForm")
                btnBack.Enabled = false;
            else
                btnBack.Enabled = true;

            if (btnNext.Text == Common.GetResourceKeyValue("CommonInstall"))
            {
                EnableDsableLinks(false);
                BackgroundWorker bgWorker = new BackgroundWorker();
                bgWorker.DoWork += bgWorker_Install;
                bgWorker.RunWorkerAsync();
                bgWorker.RunWorkerCompleted += bgWorker_InstallationCompleted;
                ((Install)this.MdiChildren[0]).lblWait.Text = Common.GetResourceKeyValue("InstallWaitMessage");
                ((Install)this.MdiChildren[0]).picLoad.Visible = true;
                ((Install)this.MdiChildren[0]).lblWait.Visible = true;
                ((Install)this.MdiChildren[0]).txtMessage.Visible = false;
                btnNext.Enabled = false;
                btnCancel.Enabled = false;
                btnBack.Enabled = false;
            }
            else if (btnNext.Text == Common.GetResourceKeyValue("CommonFinish"))
            {
                GlobalData.IsBtnClose = true;
                Application.Exit();
            }
            else
            {
                if (GlobalData.NextFormName == "Install")
                {
                    btnNext.Text = Common.GetResourceKeyValue("CommonInstall");
                }
                else
                {
                    btnNext.Text = Common.GetResourceKeyValue("CommonNext");
                }

                switch (GlobalData.NextFormName)
                {
                    case "CustomerValidation":
                        Common.AddAppSettingData("ApiBaseUrl", ConfigurationManager.AppSettings["ApiBaseUrl"]);
                        ((frmWelcome)this.MdiChildren[0]).Close();
                        lnkLblCustomerCode.Enabled = true;
                        FormDetail.OpenCustomerShortCodeForm();
                        break;
                    case "UserDetails":
                        //Customer name validation
                        if (string.IsNullOrEmpty(((CustomerValidation)this.MdiChildren[0]).txtCustomerName.Text.Trim()))
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("CustomerShortCodeValidationMsg"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        //Api key validation
                        if (string.IsNullOrEmpty(((CustomerValidation)this.MdiChildren[0]).txtApiKey.Text.Trim()))
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("ApiKeyValidationMsg"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }

                        HttpWebResponse apiResponse = null;
                        try
                        {
                            apiResponse = (HttpWebResponse)Common.GetApiResponse(((CustomerValidation)this.MdiChildren[0]).txtCustomerName.Text.Trim(), ((CustomerValidation)this.MdiChildren[0]).txtApiKey.Text.Trim(), "/api/customer/validatekey", "GET", "application/json", "");
                        }
                        catch
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("CustomerApiKeyValidation"), Common.GetResourceKeyValue("Error"), MessageBoxButtons.OK);
                            return;
                        }
                        if (apiResponse == null || apiResponse.StatusCode != HttpStatusCode.OK)
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("CustomerApiKeyValidation"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        //Storing the customer name into global variable
                        Common.AddAppSettingData("CustomerShortCode", ((CustomerValidation)this.MdiChildren[0]).txtCustomerName.Text.Trim());

                        //Storing api key
                        Common.AddAppSettingData("ApiKey", ((CustomerValidation)this.MdiChildren[0]).txtApiKey.Text.Trim());
                        //Closing the current form and opening the new one
                        ((CustomerValidation)this.MdiChildren[0]).Close();
                        lnkLblUserDetails.Enabled = true;
                        FormDetail.OpenUserDetailsForm();
                        break;
                    case "LDAPPath":
                        //Textbox validations
                        if (string.IsNullOrEmpty(((UserDetails)this.MdiChildren[0]).txtUserName.Text.Trim()))
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("UsernameValidationMsg"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        if (string.IsNullOrEmpty(((UserDetails)this.MdiChildren[0]).txtPassword.Text.Trim()))
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("PasswordValidationMsg"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        var streamProperties = new
                        {
                            CustomerShortCode = Common.GetAppSettingdataValue("CustomerShortCode"),
                            ApiUserName = ((UserDetails)this.MdiChildren[0]).txtUserName.Text.Trim(),
                            ApiPassWord = ((UserDetails)this.MdiChildren[0]).txtPassword.Text.Trim()
                        };

                        var content = Json.Encode(streamProperties);
                        HttpWebResponse response = null;
                        try
                        {
                            response = (HttpWebResponse)Common.GetApiResponse(Common.GetAppSettingdataValue("CustomerShortCode"), Common.GetAppSettingdataValue("ApiKey"), "/api/customer/validate", "POST", "application/json", content);
                        }
                        catch
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("AuthenticationValidationMsg"), Common.GetResourceKeyValue("Error"), MessageBoxButtons.OK);
                            return;
                        }
                        if (response == null || response.StatusCode != HttpStatusCode.OK)
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("AuthenticationValidationMsg"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        //Temporary store username and password to be updated in config file later on
                        Common.AddAppSettingData("UserName", ((UserDetails)this.MdiChildren[0]).txtUserName.Text.Trim());
                        Common.AddAppSettingData("Password", ((UserDetails)this.MdiChildren[0]).txtPassword.Text.Trim());
                        //Open the new form
                        ((UserDetails)this.MdiChildren[0]).Close();
                        lnkLblLDAPConfiguration.Enabled = true;
                        FormDetail.OpenLDAPPathForm();
                        break;
                    case "CustomerEmail":
                        if (((LDAPPath)this.MdiChildren[0]).chkEnableADServerName.Checked)
                        {
                            if (string.IsNullOrEmpty(((LDAPPath)this.MdiChildren[0]).txtLdapPathServerName.Text))
                            {
                                MessageBox.Show(Common.GetResourceKeyValue("ServerNameRequired"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                                return;
                            }
                            if (string.IsNullOrEmpty(((LDAPPath)this.MdiChildren[0]).txtOuPathServerName.Text))
                            {
                                MessageBox.Show(Common.GetResourceKeyValue("ServerNameRequired"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                                return;
                            }
                            if (string.IsNullOrEmpty(((LDAPPath)this.MdiChildren[0]).txtAdServerUsername.Text))
                            {
                                MessageBox.Show(Common.GetResourceKeyValue("ServerUserNameRequired"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                                return;
                            }
                            if (string.IsNullOrEmpty(((LDAPPath)this.MdiChildren[0]).txtAdServerPassword.Text))
                            {
                                MessageBox.Show(Common.GetResourceKeyValue("ServerPassweordRequired"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                                return;
                            }
                        }
                        if (string.IsNullOrEmpty(((LDAPPath)this.MdiChildren[0]).txtLDAPPath.Text.Trim()))
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("LDAPPathRequired"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        if (string.IsNullOrEmpty(((LDAPPath)this.MdiChildren[0]).txtLDAPSecurityGroup.Text.Trim()))
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("LDAPSecurityGroupRequired"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }

                        if (((LDAPPath)this.MdiChildren[0]).chkEnableADServerName.Checked)
                        {
                            try
                            {
                                using (var ldapConnection = new LdapConnection(((LDAPPath)this.MdiChildren[0]).txtLdapPathServerName.Text))
                                {
                                    var networkCredential = new NetworkCredential(((LDAPPath)this.MdiChildren[0]).txtAdServerUsername.Text, ((LDAPPath)this.MdiChildren[0]).txtAdServerPassword.Text, ((LDAPPath)this.MdiChildren[0]).txtLdapPathServerName.Text.Trim());
                                    ldapConnection.SessionOptions.SecureSocketLayer = true;
                                    ldapConnection.AuthType = AuthType.Negotiate;
                                    ldapConnection.Bind(networkCredential);
                                }
                            }
                            catch (LdapException ldapException)
                            {
                                MessageBox.Show("Error - " + ldapException.Message);
                                return;
                            }
                        }
                        else
                        {
                            bool isLDAPPath = false;
                            try
                            {
                                isLDAPPath = DirectoryEntry.Exists("LDAP://" + ((LDAPPath)this.MdiChildren[0]).txtLDAPPath.Text.Trim());
                            }
                            catch
                            {
                                MessageBox.Show(Common.GetResourceKeyValue("LDAPPathValidation"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                                return;
                            }
                            if (!isLDAPPath)
                            {
                                MessageBox.Show(Common.GetResourceKeyValue("LDAPPathValidation"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                                return;
                            }
                        }

                        bool isValidOUGroup = false;
                        try
                        {
                            isValidOUGroup = DirectoryEntry.Exists("LDAP://" + ((LDAPPath)this.MdiChildren[0]).txtLDAPSecurityGroup.Text.Trim());
                        }
                        catch
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("LDAPSecurityGroupValidation"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        if (!isValidOUGroup)
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("LDAPSecurityGroupValidation"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        if (((LDAPPath)this.MdiChildren[0]).chkEnableADServerName.Checked)
                        {
                            Common.AddAppSettingData("ADServerName", ((LDAPPath)this.MdiChildren[0]).txtLdapPathServerName.Text.Trim());
                            Common.AddAppSettingData("ADServerUserName", ((LDAPPath)this.MdiChildren[0]).txtAdServerUsername.Text.Trim());
                            Common.AddAppSettingData("ADServerPassword", ((LDAPPath)this.MdiChildren[0]).txtAdServerPassword.Text.Trim());

                            Common.AddAppSettingData("LDAPPath", "LDAP://" + ((LDAPPath)this.MdiChildren[0]).txtLdapPathServerName.Text.Trim() + "/" + ((LDAPPath)this.MdiChildren[0]).txtLDAPPath.Text.Trim());
                            GlobalData.OUPath = "LDAP://" + ((LDAPPath)this.MdiChildren[0]).txtLdapPathServerName.Text.Trim() + "/" + ((LDAPPath)this.MdiChildren[0]).txtLDAPSecurityGroup.Text.Trim();
                        }
                        else
                        {
                            Common.AddAppSettingData("LDAPPath", "LDAP://" + ((LDAPPath)this.MdiChildren[0]).txtLDAPPath.Text.Trim());
                            GlobalData.OUPath = "LDAP://" + ((LDAPPath)this.MdiChildren[0]).txtLDAPSecurityGroup.Text.Trim();
                        }

                        //Open the new form
                        ((LDAPPath)this.MdiChildren[0]).Close();
                        lnkLblCustomerEmail.Enabled = true;
                        FormDetail.OpenCustomerEmailForm();
                        break;
                    case "InstallationPath":
                        //TextBox validation
                        if (string.IsNullOrEmpty(((CustomerEmail)this.MdiChildren[0]).txtCustomerEmail.Text.Trim()))
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("CustomerEmailRequired"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        if (!Regex.IsMatch(((CustomerEmail)this.MdiChildren[0]).txtCustomerEmail.Text.Trim(), @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase))
                        {
                            MessageBox.Show(Common.GetResourceKeyValue("CustomerEmailValidation"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                            return;
                        }
                        Common.AddAppSettingData("NotificationEmail", ((CustomerEmail)this.MdiChildren[0]).txtCustomerEmail.Text.Trim());
                        ((CustomerEmail)this.MdiChildren[0]).Close();
                        lnkLblInstallationPath.Enabled = true;
                        FormDetail.OpenInstallationPathForm();
                        break;
                    case "Install":
                        if (Common.GetPreviousAppSettings() == null)
                        {
                            string path = ((InstallationPath)this.MdiChildren[0]).txtInstallationPath.Text;
                            if (System.IO.Directory.Exists(path))
                            {
                                GlobalData.InstallationPath = path;
                            }
                            else
                            {
                                MessageBox.Show(Common.GetResourceKeyValue("PathValidationMsg"), Common.GetResourceKeyValue("Message"), MessageBoxButtons.OK);
                                return;
                            }
                            ((InstallationPath)this.MdiChildren[0]).Close();
                            lnkLblInstall.Enabled = true;
                        }
                        else
                        {
                            ((frmWelcome)this.MdiChildren[0]).Close();
                        }
                        FormDetail.OpenInstallForm();
                        break;
                }
            }
        }

        /// <summary>
        /// This method is called when the installation is complete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgWorker_InstallationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Install)this.MdiChildren[0]).txtMessage.Visible = true;
            ((Install)this.MdiChildren[0]).picLoad.Visible = false;
            ((Install)this.MdiChildren[0]).lblWait.Visible = false;
            if (GlobalData.IsInstallationSuccess)
            {
                ((Install)this.MdiChildren[0]).txtMessage.Text = Common.GetResourceKeyValue("InstallSuccessMsg");
                btnNext.Text = Common.GetResourceKeyValue("CommonFinish");
                btnNext.Enabled = true;
                btnCancel.Visible = false;
                btnBack.Visible = false;
                EnableDsableLinks(false);
            }
            else
            {
                ((Install)this.MdiChildren[0]).txtMessage.Text = Common.GetResourceKeyValue("InstallFailureMsg");
                btnBack.Enabled = true;
                btnNext.Enabled = true;
                btnCancel.Enabled = true;
                btnCancel.Visible = true;
                btnNext.Visible = true;
                btnBack.Visible = true;
                EnableDsableLinks(true);
            }
        }

        /// <summary>
        /// This private method will enable and disable the linksavailable on the application based on the parameter passed
        /// </summary>
        /// <param name="isEnable"></param>
        private void EnableDsableLinks(bool isEnable)
        {
            lnkLblWelcome.Enabled = isEnable;
            lnkLblCustomerCode.Enabled = isEnable;
            lnkLblUserDetails.Enabled = isEnable;
            lnkLblLDAPConfiguration.Enabled = isEnable;
            lnkLblCustomerEmail.Enabled = isEnable;
            lnkLblInstallationPath.Enabled = isEnable;
            lnkLblInstall.Enabled = isEnable;
        }
        /// <summary>
        /// This method is used for installation of the job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bgWorker_Install(object sender, DoWorkEventArgs e)
        {
            try
            {
                GlobalData.IsInstallationSuccess = false;
                if (Common.GetPreviousAppSettings() == null)
                {
                    Common.CheckSecurityGroupExistance();
                }
                Common.UpdateConfigFile();
                //Scheduler.DeleteTask();
                CopyDirectoryFiles();

                string jobPath = Path.Combine(GlobalData.InstallationPath, "Data\\UserSyncClient.exe");
                Scheduler.ScheduleJob(jobPath);
                System.Threading.Thread.Sleep(2000);
                GlobalData.IsInstallationSuccess = true;
            }
            catch (Exception ex)
            {
                GlobalData.IsInstallationSuccess = false;
                MessageBox.Show("Message : " + ex.Message, Common.GetResourceKeyValue("Error"), MessageBoxButtons.OK);
                MessageBox.Show("Stack Trace : " + ex.StackTrace, Common.GetResourceKeyValue("Error"), MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// This function is used to copy the job exe to the installation path
        /// </summary>
        private static void CopyDirectoryFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo("Data");
            string srcDirectory = GlobalData.InstallationPath;
            string destDirectory = Path.Combine(srcDirectory, "Data");
            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            else
            {
                foreach (var existingFile in Directory.GetFiles(destDirectory))
                {
                    FileInfo fileDetails = new FileInfo(existingFile);
                    if (fileDetails.Name.ToLower() == "securitygroupuser.xml")
                    {
                        fileDetails.CopyTo(Path.Combine(dirInfo.FullName, fileDetails.Name), true);
                    }
                    fileDetails.Delete();
                }
            }
            FileInfo[] files = dirInfo.GetFiles();
            foreach (var info in files)
            {
                string tempPath = Path.Combine(destDirectory, info.Name);
                info.CopyTo(tempPath, true);
            }
        }

        /// <summary>
        /// This method is used for closing all the child form
        /// </summary>
        /// <param name="formNumber"></param>
        private void CloseAllChildForm(int formNumber)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Dispose();
                frm.Close();
            }
            switch (formNumber)
            {
                case 1:
                    FormDetail.OpenWelcomeForm();
                    break;
                case 2:
                    FormDetail.OpenApiKeyForm();
                    break;
                case 3:
                    FormDetail.OpenCustomerShortCodeForm();
                    break;
                case 4:
                    FormDetail.OpenUserDetailsForm();
                    break;
                case 5:
                    FormDetail.OpenLDAPPathForm();
                    break;
                case 6:
                    FormDetail.OpenCustomerEmailForm();
                    break;
                case 7:
                    FormDetail.OpenInstallationPathForm();
                    break;
                case 8:
                    FormDetail.OpenInstallForm();
                    break;
            }
            if (formNumber == 1)
            {
                btnBack.Enabled = false;
            }
            else
            {
                btnBack.Enabled = true;
            }

            if (formNumber == 8)
            {
                btnNext.Text = Common.GetResourceKeyValue("CommonInstall");
            }
            else
            {
                btnNext.Text = Common.GetResourceKeyValue("CommonNext");
            }
        }

        /// <summary>
        /// Welcome link button click functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkLblWelcome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseAllChildForm(1);
        }

        /// <summary>
        /// Click event for customer code link button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkLblCustomerCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseAllChildForm(3);
        }

        /// <summary>
        /// click event for user details link button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkLblUserDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseAllChildForm(4);
        }

        /// <summary>
        /// click event for LDAP configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkLblLDAPConfiguration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseAllChildForm(5);
        }
        /// <summary>
        /// click event for customer email link button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkLblCustomerEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseAllChildForm(6);
        }
        /// <summary>
        /// Click event for Installation path link button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkLblInstallationPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseAllChildForm(7);
        }

        /// <summary>
        /// Click event for install button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkLblInstall_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseAllChildForm(8);
        }
    }
}
