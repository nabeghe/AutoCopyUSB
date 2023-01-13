namespace AutoCopyUSB
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.cbStartUp = new System.Windows.Forms.CheckBox();
            this.btnPassword = new System.Windows.Forms.Button();
            this.tbTargetPath = new System.Windows.Forms.TextBox();
            this.btnBrowserTargetPath = new System.Windows.Forms.Button();
            this.btnPassport = new System.Windows.Forms.Button();
            this.lblLinkDeveloper = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(107, 51);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cbStartUp
            // 
            this.cbStartUp.AutoSize = true;
            this.cbStartUp.Location = new System.Drawing.Point(351, 30);
            this.cbStartUp.Name = "cbStartUp";
            this.cbStartUp.Size = new System.Drawing.Size(62, 17);
            this.cbStartUp.TabIndex = 2;
            this.cbStartUp.Text = "StartUp";
            this.cbStartUp.UseVisualStyleBackColor = true;
            this.cbStartUp.CheckedChanged += new System.EventHandler(this.cbStartUp_CheckedChanged);
            // 
            // btnPassword
            // 
            this.btnPassword.Location = new System.Drawing.Point(125, 12);
            this.btnPassword.Name = "btnPassword";
            this.btnPassword.Size = new System.Drawing.Size(107, 51);
            this.btnPassword.TabIndex = 3;
            this.btnPassword.Text = "Password";
            this.btnPassword.UseVisualStyleBackColor = true;
            this.btnPassword.Click += new System.EventHandler(this.btnPassword_Click);
            // 
            // tbTargetPath
            // 
            this.tbTargetPath.Location = new System.Drawing.Point(12, 69);
            this.tbTargetPath.Name = "tbTargetPath";
            this.tbTargetPath.ReadOnly = true;
            this.tbTargetPath.Size = new System.Drawing.Size(333, 20);
            this.tbTargetPath.TabIndex = 4;
            // 
            // btnBrowserTargetPath
            // 
            this.btnBrowserTargetPath.Location = new System.Drawing.Point(351, 68);
            this.btnBrowserTargetPath.Name = "btnBrowserTargetPath";
            this.btnBrowserTargetPath.Size = new System.Drawing.Size(54, 20);
            this.btnBrowserTargetPath.TabIndex = 5;
            this.btnBrowserTargetPath.Text = "Browse";
            this.btnBrowserTargetPath.UseVisualStyleBackColor = true;
            this.btnBrowserTargetPath.Click += new System.EventHandler(this.btnBrowserTargetPath_Click);
            // 
            // btnPassport
            // 
            this.btnPassport.Location = new System.Drawing.Point(238, 12);
            this.btnPassport.Name = "btnPassport";
            this.btnPassport.Size = new System.Drawing.Size(107, 51);
            this.btnPassport.TabIndex = 6;
            this.btnPassport.Text = "Passport";
            this.btnPassport.UseVisualStyleBackColor = true;
            this.btnPassport.Click += new System.EventHandler(this.btnPassport_Click);
            // 
            // lblLinkDeveloper
            // 
            this.lblLinkDeveloper.AutoSize = true;
            this.lblLinkDeveloper.Location = new System.Drawing.Point(85, 104);
            this.lblLinkDeveloper.Name = "lblLinkDeveloper";
            this.lblLinkDeveloper.Size = new System.Drawing.Size(16, 13);
            this.lblLinkDeveloper.TabIndex = 7;
            this.lblLinkDeveloper.TabStop = true;
            this.lblLinkDeveloper.Text = "...";
            this.lblLinkDeveloper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLinkDeveloper_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Developed by:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 126);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLinkDeveloper);
            this.Controls.Add(this.btnPassport);
            this.Controls.Add(this.btnBrowserTargetPath);
            this.Controls.Add(this.tbTargetPath);
            this.Controls.Add(this.btnPassword);
            this.Controls.Add(this.cbStartUp);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Copy USB";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox cbStartUp;
        private System.Windows.Forms.Button btnPassword;
        private System.Windows.Forms.TextBox tbTargetPath;
        private System.Windows.Forms.Button btnBrowserTargetPath;
        private System.Windows.Forms.Button btnPassport;
        private System.Windows.Forms.LinkLabel lblLinkDeveloper;
        private System.Windows.Forms.Label label1;
    }
}

