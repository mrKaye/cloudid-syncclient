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
    public partial class SyncOption : Form
    {
        public SyncOption()
        {
            InitializeComponent();
        }

        private void LocalizeText()
        {
            lblHeading.Text = Common.GetResourceKeyValue("SyncOptionHeading");
            txtDescription.Text = Common.GetResourceKeyValue("SyncOptionDescription");
            lblSyncingOption.Text = Common.GetResourceKeyValue("SyncOptionAvailability");
            rbtnEmail.Text = Common.GetResourceKeyValue("Email");
        }
        private void SyncOption_Load(object sender, EventArgs e)
        {
            LocalizeText();
            GlobalData.BackFormName = "UserDetails";
            GlobalData.NextFormName = "LDAPPath";
            string syncOption = Common.GetAppSettingdataValue("SyncOption");
            if (!string.IsNullOrEmpty(syncOption))
            {
                switch(syncOption.ToLower())
                {
                    case "mail":
                        rbtnEmail.Checked = true;
                        break;
                    case "userprincipalname":
                        rbtnUPN.Checked = true;
                        break;
                }
            }
        }
    }
}
