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
    public partial class APIKeyValidator : Form
    {
        public APIKeyValidator()
        {
            InitializeComponent();
        }

        private void LocalizeText()
        {
            lblHeading.Text = Common.GetResourceKeyValue("ApiKeyFrmHeading");
            txtDescription.Text = Common.GetResourceKeyValue("ApiKeyFrmDescription");
            lblApiKey.Text = Common.GetResourceKeyValue("ApiKey");
        }

        private void APIKeyValidator_Load(object sender, EventArgs e)
        {
            LocalizeText();
            GlobalData.BackFormName = "StartForm";
            GlobalData.NextFormName = "CustomerValidation";
        }
    }
}
