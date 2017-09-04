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
    /// <summary>
    /// This form is used to capture the user details
    /// </summary>
    public partial class UserDetails : Form
    {
        public UserDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This function will fetch the data from the resource file and update the form
        /// </summary>
        private void LocalizeText()
        {
            lblHeading.Text = Common.GetResourceKeyValue("UserDetailsFrmHeading");
            txtDescription.Text = Common.GetResourceKeyValue("UserDetailsFrmDescription");
            lblUsername.Text = Common.GetResourceKeyValue("UserDetailsFrmUserName");
            lblPassword.Text = Common.GetResourceKeyValue("UserDetailsFrmPassword");
        }

        private void UserDetails_Load(object sender, EventArgs e)
        {
            //Set the form data
            LocalizeText();
            GlobalData.BackFormName = "CustomerValidation";
            GlobalData.NextFormName = "SyncOption";
        }
    }
}
