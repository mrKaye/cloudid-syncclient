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
    /// This form is used to store the customer information
    /// </summary>
    public partial class CustomerValidation : Form
    {
        public CustomerValidation()
        {
            InitializeComponent();
        }
     
        /// <summary>
        /// This function will fetch the data from the resource file and update the form
        /// </summary>
        private void LocalizeText()
        {
            lblHeading.Text = Common.GetResourceKeyValue("CustomerFrmHeading");
            txtDescription.Text = Common.GetResourceKeyValue("CustomerFrmDescription");
            lblCustomerShortCode.Text = Common.GetResourceKeyValue("CustomerCode");
            lblApiKey.Text = Common.GetResourceKeyValue("ApiKey");
        }

        private void CustomerValidation_Load(object sender, EventArgs e)
        {
            //Loading the form with localized data
            LocalizeText();
            GlobalData.BackFormName = "StartForm";
            GlobalData.NextFormName = "UserDetails";
        }
    }
}
