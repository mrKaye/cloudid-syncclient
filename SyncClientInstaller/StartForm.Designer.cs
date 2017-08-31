namespace SyncClientInstaller
{
    partial class frmWelcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWelcome));
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblChangeLanguage = new System.Windows.Forms.Label();
            this.rbtnEnglish = new System.Windows.Forms.RadioButton();
            this.rbtnNorsk = new System.Windows.Forms.RadioButton();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
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
            this.txtDescription.Size = new System.Drawing.Size(434, 120);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.TabStop = false;
            // 
            // lblChangeLanguage
            // 
            this.lblChangeLanguage.AutoSize = true;
            this.lblChangeLanguage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblChangeLanguage.Location = new System.Drawing.Point(175, 211);
            this.lblChangeLanguage.Name = "lblChangeLanguage";
            this.lblChangeLanguage.Size = new System.Drawing.Size(126, 16);
            this.lblChangeLanguage.TabIndex = 4;
            this.lblChangeLanguage.Text = "Change Language";
            // 
            // rbtnEnglish
            // 
            this.rbtnEnglish.AutoSize = true;
            this.rbtnEnglish.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnEnglish.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.rbtnEnglish.Image = ((System.Drawing.Image)(resources.GetObject("rbtnEnglish.Image")));
            this.rbtnEnglish.Location = new System.Drawing.Point(323, 206);
            this.rbtnEnglish.Name = "rbtnEnglish";
            this.rbtnEnglish.Size = new System.Drawing.Size(122, 30);
            this.rbtnEnglish.TabIndex = 2;
            this.rbtnEnglish.Text = "English";
            this.rbtnEnglish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rbtnEnglish.UseVisualStyleBackColor = true;
            this.rbtnEnglish.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rbtnEnglish_MouseUp);
            // 
            // rbtnNorsk
            // 
            this.rbtnNorsk.AutoSize = true;
            this.rbtnNorsk.Checked = true;
            this.rbtnNorsk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnNorsk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.rbtnNorsk.Image = ((System.Drawing.Image)(resources.GetObject("rbtnNorsk.Image")));
            this.rbtnNorsk.Location = new System.Drawing.Point(494, 206);
            this.rbtnNorsk.Name = "rbtnNorsk";
            this.rbtnNorsk.Size = new System.Drawing.Size(111, 30);
            this.rbtnNorsk.TabIndex = 1;
            this.rbtnNorsk.TabStop = true;
            this.rbtnNorsk.Text = "Norsk";
            this.rbtnNorsk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rbtnNorsk.UseVisualStyleBackColor = true;
            this.rbtnNorsk.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rbtnNorsk_MouseUp);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.lblWelcome.Location = new System.Drawing.Point(175, 18);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(282, 19);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Welcome To Sync Client Application";
            // 
            // frmWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(630, 320);
            this.ControlBox = false;
            this.Controls.Add(this.rbtnNorsk);
            this.Controls.Add(this.rbtnEnglish);
            this.Controls.Add(this.lblChangeLanguage);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWelcome";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.frmWelcome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblChangeLanguage;
        private System.Windows.Forms.RadioButton rbtnEnglish;
        private System.Windows.Forms.RadioButton rbtnNorsk;
        private System.Windows.Forms.Label lblWelcome;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

