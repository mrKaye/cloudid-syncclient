namespace SyncClientInstaller
{
    partial class SyncOption
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
            this.lblSyncingOption = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.rbtnUPN = new System.Windows.Forms.RadioButton();
            this.rbtnEmail = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblSyncingOption
            // 
            this.lblSyncingOption.AutoSize = true;
            this.lblSyncingOption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSyncingOption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblSyncingOption.Location = new System.Drawing.Point(175, 209);
            this.lblSyncingOption.Name = "lblSyncingOption";
            this.lblSyncingOption.Size = new System.Drawing.Size(78, 16);
            this.lblSyncingOption.TabIndex = 16;
            this.lblSyncingOption.Text = "Sync Using";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.txtDescription.Location = new System.Drawing.Point(175, 62);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(430, 120);
            this.txtDescription.TabIndex = 20;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblHeading.Location = new System.Drawing.Point(175, 18);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(127, 19);
            this.lblHeading.TabIndex = 15;
            this.lblHeading.Text = "Syncing Option";
            // 
            // rbtnUPN
            // 
            this.rbtnUPN.AutoSize = true;
            this.rbtnUPN.Checked = true;
            this.rbtnUPN.Location = new System.Drawing.Point(348, 208);
            this.rbtnUPN.Name = "rbtnUPN";
            this.rbtnUPN.Size = new System.Drawing.Size(48, 17);
            this.rbtnUPN.TabIndex = 14;
            this.rbtnUPN.TabStop = true;
            this.rbtnUPN.Text = "UPN";
            this.rbtnUPN.UseVisualStyleBackColor = true;
            // 
            // rbtnEmail
            // 
            this.rbtnEmail.AutoSize = true;
            this.rbtnEmail.Location = new System.Drawing.Point(499, 208);
            this.rbtnEmail.Name = "rbtnEmail";
            this.rbtnEmail.Size = new System.Drawing.Size(50, 17);
            this.rbtnEmail.TabIndex = 19;
            this.rbtnEmail.Text = "Email";
            this.rbtnEmail.UseVisualStyleBackColor = true;
            // 
            // SyncOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(614, 282);
            this.Controls.Add(this.rbtnEmail);
            this.Controls.Add(this.rbtnUPN);
            this.Controls.Add(this.lblSyncingOption);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SyncOption";
            this.ShowIcon = false;
            this.Text = "SyncOption";
            this.Load += new System.EventHandler(this.SyncOption_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSyncingOption;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblHeading;
        internal System.Windows.Forms.RadioButton rbtnUPN;
        internal System.Windows.Forms.RadioButton rbtnEmail;
    }
}