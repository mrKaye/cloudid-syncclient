namespace SyncClientInstaller
{
    partial class LDAPPath
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLDAPSecurityGroup = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtLDAPSecurityGroup = new System.Windows.Forms.TextBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblLDAPPath = new System.Windows.Forms.Label();
            this.txtLDAPPath = new System.Windows.Forms.TextBox();
            this.txtLDAPProtocol1 = new System.Windows.Forms.TextBox();
            this.txtLDAPProtocol2 = new System.Windows.Forms.TextBox();
            this.txtLdapPathServerName = new System.Windows.Forms.TextBox();
            this.txtOuPathServerName = new System.Windows.Forms.TextBox();
            this.chkEnableADServerName = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtAdServerUsername = new System.Windows.Forms.TextBox();
            this.txtAdServerPassword = new System.Windows.Forms.TextBox();
            this.lblAdServerPassword = new System.Windows.Forms.Label();
            this.lblAdServerUserName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLDAPSecurityGroup
            // 
            this.lblLDAPSecurityGroup.AutoSize = true;
            this.lblLDAPSecurityGroup.Font = new System.Drawing.Font("Arial", 10F);
            this.lblLDAPSecurityGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblLDAPSecurityGroup.Location = new System.Drawing.Point(175, 192);
            this.lblLDAPSecurityGroup.Name = "lblLDAPSecurityGroup";
            this.lblLDAPSecurityGroup.Size = new System.Drawing.Size(61, 16);
            this.lblLDAPSecurityGroup.TabIndex = 14;
            this.lblLDAPSecurityGroup.Text = "OU Path";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            this.txtDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.txtDescription.Location = new System.Drawing.Point(175, 62);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(430, 80);
            this.txtDescription.TabIndex = 16;
            this.txtDescription.Text = "Please provide the LDAP path and the security group path in the textbox provided." +
    " This path will be used to fetch the user from the active directory group \'cloud" +
    "idsyncusers\'and update into the cloud.";
            // 
            // txtLDAPSecurityGroup
            // 
            this.txtLDAPSecurityGroup.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLDAPSecurityGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.txtLDAPSecurityGroup.Location = new System.Drawing.Point(401, 193);
            this.txtLDAPSecurityGroup.Name = "txtLDAPSecurityGroup";
            this.txtLDAPSecurityGroup.Size = new System.Drawing.Size(204, 20);
            this.txtLDAPSecurityGroup.TabIndex = 13;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.White;
            this.lblHeading.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblHeading.Location = new System.Drawing.Point(175, 18);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(162, 19);
            this.lblHeading.TabIndex = 15;
            this.lblHeading.Text = "LDAP Configuration";
            // 
            // lblLDAPPath
            // 
            this.lblLDAPPath.AutoSize = true;
            this.lblLDAPPath.Font = new System.Drawing.Font("Arial", 10F);
            this.lblLDAPPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblLDAPPath.Location = new System.Drawing.Point(174, 171);
            this.lblLDAPPath.Name = "lblLDAPPath";
            this.lblLDAPPath.Size = new System.Drawing.Size(77, 16);
            this.lblLDAPPath.TabIndex = 16;
            this.lblLDAPPath.Text = "LDAP Path";
            // 
            // txtLDAPPath
            // 
            this.txtLDAPPath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLDAPPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.txtLDAPPath.Location = new System.Drawing.Point(401, 172);
            this.txtLDAPPath.Name = "txtLDAPPath";
            this.txtLDAPPath.Size = new System.Drawing.Size(204, 20);
            this.txtLDAPPath.TabIndex = 10;
            // 
            // txtLDAPProtocol1
            // 
            this.txtLDAPProtocol1.Enabled = false;
            this.txtLDAPProtocol1.Location = new System.Drawing.Point(278, 172);
            this.txtLDAPProtocol1.Name = "txtLDAPProtocol1";
            this.txtLDAPProtocol1.Size = new System.Drawing.Size(50, 20);
            this.txtLDAPProtocol1.TabIndex = 8;
            this.txtLDAPProtocol1.Text = "LDAP://";
            // 
            // txtLDAPProtocol2
            // 
            this.txtLDAPProtocol2.Enabled = false;
            this.txtLDAPProtocol2.Location = new System.Drawing.Point(278, 193);
            this.txtLDAPProtocol2.Name = "txtLDAPProtocol2";
            this.txtLDAPProtocol2.Size = new System.Drawing.Size(50, 20);
            this.txtLDAPProtocol2.TabIndex = 11;
            this.txtLDAPProtocol2.Text = "LDAP://";
            // 
            // txtLdapPathServerName
            // 
            this.txtLdapPathServerName.Enabled = false;
            this.txtLdapPathServerName.Location = new System.Drawing.Point(325, 172);
            this.txtLdapPathServerName.Name = "txtLdapPathServerName";
            this.txtLdapPathServerName.Size = new System.Drawing.Size(68, 20);
            this.txtLdapPathServerName.TabIndex = 9;
            this.txtLdapPathServerName.Text = "Server name";
            this.txtLdapPathServerName.TextChanged += new System.EventHandler(this.txtLdapPathServerName_TextChanged);
            this.txtLdapPathServerName.Enter += new System.EventHandler(this.txtLdapPathServerName_Enter);
            // 
            // txtOuPathServerName
            // 
            this.txtOuPathServerName.Enabled = false;
            this.txtOuPathServerName.Location = new System.Drawing.Point(325, 193);
            this.txtOuPathServerName.Name = "txtOuPathServerName";
            this.txtOuPathServerName.Size = new System.Drawing.Size(68, 20);
            this.txtOuPathServerName.TabIndex = 12;
            this.txtOuPathServerName.Text = "Server name";
            this.txtOuPathServerName.TextChanged += new System.EventHandler(this.txtOuPathServerName_TextChanged);
            this.txtOuPathServerName.Enter += new System.EventHandler(this.txtOuPathServerName_Enter);
            // 
            // chkEnableADServerName
            // 
            this.chkEnableADServerName.AutoSize = true;
            this.chkEnableADServerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.chkEnableADServerName.Location = new System.Drawing.Point(175, 149);
            this.chkEnableADServerName.Name = "chkEnableADServerName";
            this.chkEnableADServerName.Size = new System.Drawing.Size(202, 17);
            this.chkEnableADServerName.TabIndex = 7;
            this.chkEnableADServerName.Text = "Enable Active Directory Server Name";
            this.chkEnableADServerName.UseVisualStyleBackColor = true;
            this.chkEnableADServerName.CheckedChanged += new System.EventHandler(this.chkEnableADServerName_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(392, 193);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(10, 20);
            this.textBox3.TabIndex = 26;
            this.textBox3.Text = "/";
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(392, 172);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(10, 20);
            this.textBox4.TabIndex = 27;
            this.textBox4.Text = "/";
            // 
            // txtAdServerUsername
            // 
            this.txtAdServerUsername.Location = new System.Drawing.Point(401, 214);
            this.txtAdServerUsername.Name = "txtAdServerUsername";
            this.txtAdServerUsername.Size = new System.Drawing.Size(204, 20);
            this.txtAdServerUsername.TabIndex = 14;
            this.txtAdServerUsername.Visible = false;
            // 
            // txtAdServerPassword
            // 
            this.txtAdServerPassword.Location = new System.Drawing.Point(401, 236);
            this.txtAdServerPassword.Name = "txtAdServerPassword";
            this.txtAdServerPassword.PasswordChar = '*';
            this.txtAdServerPassword.Size = new System.Drawing.Size(204, 20);
            this.txtAdServerPassword.TabIndex = 15;
            this.txtAdServerPassword.Visible = false;
            // 
            // lblAdServerPassword
            // 
            this.lblAdServerPassword.AutoSize = true;
            this.lblAdServerPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblAdServerPassword.Location = new System.Drawing.Point(281, 239);
            this.lblAdServerPassword.Name = "lblAdServerPassword";
            this.lblAdServerPassword.Size = new System.Drawing.Size(105, 13);
            this.lblAdServerPassword.TabIndex = 30;
            this.lblAdServerPassword.Text = "AD Server Password";
            this.lblAdServerPassword.Visible = false;
            // 
            // lblAdServerUserName
            // 
            this.lblAdServerUserName.AutoSize = true;
            this.lblAdServerUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblAdServerUserName.Location = new System.Drawing.Point(281, 221);
            this.lblAdServerUserName.Name = "lblAdServerUserName";
            this.lblAdServerUserName.Size = new System.Drawing.Size(107, 13);
            this.lblAdServerUserName.TabIndex = 29;
            this.lblAdServerUserName.Text = "AD Server Username";
            this.lblAdServerUserName.Visible = false;
            // 
            // LDAPPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(630, 320);
            this.Controls.Add(this.txtAdServerUsername);
            this.Controls.Add(this.txtAdServerPassword);
            this.Controls.Add(this.lblAdServerPassword);
            this.Controls.Add(this.lblAdServerUserName);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.chkEnableADServerName);
            this.Controls.Add(this.txtOuPathServerName);
            this.Controls.Add(this.txtLdapPathServerName);
            this.Controls.Add(this.txtLDAPProtocol2);
            this.Controls.Add(this.txtLDAPProtocol1);
            this.Controls.Add(this.lblLDAPPath);
            this.Controls.Add(this.txtLDAPPath);
            this.Controls.Add(this.lblLDAPSecurityGroup);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtLDAPSecurityGroup);
            this.Controls.Add(this.lblHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LDAPPath";
            this.ShowIcon = false;
            this.Text = "CustomerEmail";
            this.Load += new System.EventHandler(this.LDAPPath_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLDAPSecurityGroup;
        private System.Windows.Forms.TextBox txtDescription;
        public System.Windows.Forms.TextBox txtLDAPSecurityGroup;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblLDAPPath;
        public System.Windows.Forms.TextBox txtLDAPPath;
        private System.Windows.Forms.TextBox txtLDAPProtocol1;
        private System.Windows.Forms.TextBox txtLDAPProtocol2;
        public System.Windows.Forms.TextBox txtLdapPathServerName;
        public System.Windows.Forms.TextBox txtOuPathServerName;
        public System.Windows.Forms.CheckBox chkEnableADServerName;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.TextBox txtAdServerUsername;
        public System.Windows.Forms.TextBox txtAdServerPassword;
        private System.Windows.Forms.Label lblAdServerPassword;
        private System.Windows.Forms.Label lblAdServerUserName;
    }
}