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
    public partial class ScheduleTime : Form
    {
        public ScheduleTime()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit installation?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
            InstallationPath installPath = new InstallationPath();
            installPath.MdiParent = GlobalData.FrmName;
            Control txtpath = installPath.Controls.Find("txtInstallationPath", true).FirstOrDefault();
            if (txtpath != null && txtpath.GetType() == typeof(TextBox))
            {
                ((TextBox)txtpath).Text = GlobalData.InstallationPath;
            }
            installPath.Show();
            installPath.WindowState = FormWindowState.Maximized;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtScheduleTime.Text.Trim()))
            {
                GlobalData.ScheduleTime = Convert.ToInt32(txtScheduleTime.Text.Trim());
            }
            else
            {
                MessageBox.Show("Please enter scheduling interval.");
                return;
            }
            this.Close();
            Install installFrm = new Install();
            installFrm.MdiParent = GlobalData.FrmName;
            installFrm.WindowState = FormWindowState.Maximized;
            installFrm.Show();
        }

        private void txtScheduleTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
