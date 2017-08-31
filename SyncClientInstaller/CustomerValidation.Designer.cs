namespace SyncClientInstaller
{
    partial class CustomerValidation
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblCustomerShortCode = new System.Windows.Forms.Label();
            this.lblApiKey = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblHeading.Location = new System.Drawing.Point(175, 18);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(177, 19);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "Enter Customer Name";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.txtCustomerName.Location = new System.Drawing.Point(310, 195);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(293, 20);
            this.txtCustomerName.TabIndex = 4;
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
            this.txtDescription.TabIndex = 6;
            // 
            // lblCustomerShortCode
            // 
            this.lblCustomerShortCode.AutoSize = true;
            this.lblCustomerShortCode.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerShortCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblCustomerShortCode.Location = new System.Drawing.Point(172, 194);
            this.lblCustomerShortCode.Name = "lblCustomerShortCode";
            this.lblCustomerShortCode.Size = new System.Drawing.Size(109, 16);
            this.lblCustomerShortCode.TabIndex = 6;
            this.lblCustomerShortCode.Text = "Customer Name";
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApiKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblApiKey.Location = new System.Drawing.Point(172, 220);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(56, 16);
            this.lblApiKey.TabIndex = 8;
            this.lblApiKey.Text = "Api Key";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApiKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.txtApiKey.Location = new System.Drawing.Point(310, 221);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(292, 20);
            this.txtApiKey.TabIndex = 5;
            // 
            // CustomerValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(630, 320);
            this.ControlBox = false;
            this.Controls.Add(this.lblApiKey);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.lblCustomerShortCode);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.lblHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerValidation";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.CustomerValidation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        public System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblCustomerShortCode;
        private System.Windows.Forms.Label lblApiKey;
        public System.Windows.Forms.TextBox txtApiKey;
    }
}