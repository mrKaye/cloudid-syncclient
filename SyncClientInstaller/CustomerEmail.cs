﻿using System;
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
    public partial class CustomerEmail : Form
    {
        public CustomerEmail()
        {
            InitializeComponent();
        }

        private void LocalizeText()
        {
            lblHeading.Text = Common.GetResourceKeyValue("CustomerEmail");
            txtDescription.Text = Common.GetResourceKeyValue("CustomerEmailDescription");
            lblCustomerEmail.Text = Common.GetResourceKeyValue("CustomerEmail");
        }

        private void CustomerEmail_Load(object sender, EventArgs e)
        {
            //Loading the form with localized data
            LocalizeText();
            GlobalData.BackFormName = "LDAPPath";
            GlobalData.NextFormName = "InstallationPath";
        }
    }
}
