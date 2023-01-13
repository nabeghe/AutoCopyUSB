using AutoCopyUSB.Properties;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AutoCopyUSB
{
    public partial class Form1
    {
        public void InitilizeUI()
        {
            cbStartUp.Checked = RegisteryHelper.GetStartupEnabled();
            tbTargetPath.Text = String.IsNullOrEmpty(Settings.Default.TargetPath) ? Utils.DefaultCopiesPath() : Settings.Default.TargetPath;

            this.Text += $" ({Application.ProductVersion})";

            lblLinkDeveloper.Text = Config.DEVELOPER_URL_TITLE;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void cbStartUp_CheckedChanged(object sender, EventArgs e)
        {
            RegisteryHelper.SetStartupEnabled(cbStartUp.Checked);
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            var newPassword = Interaction.InputBox("Enter new password:", "", "");
            if (!String.IsNullOrEmpty(newPassword))
            {
                Utils.SetPassword(newPassword);
            }
        }

        private void btnPassport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Utils.ExportPassport(fbd.SelectedPath);
            }
        }

        private void btnBrowserTargetPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.TargetPath = fbd.SelectedPath;
                Settings.Default.Save();
                tbTargetPath.Text = fbd.SelectedPath;
            }
        }

        private void lblLinkDeveloper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Config.SOFTWARE_URL);
        }
    }
}
