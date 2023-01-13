using AutoCopyUSB.Properties;
using AutoUpdaterDotNET;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AutoCopyUSB
{
    public partial class Form1 : Form
    {
        private string[] Args;

        public Form1(string[] args)
        {
            Args = args;
            InitializeComponent();
            InitilizeCopiesPath();
            InitilizeDeviceDetector();
            InitilizeUI();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Args.Length > 0)
            {
                Settings.Default.Enabled = true;
                Settings.Default.Save();
                Start();
            }
            else  {
                Thread worker = new Thread(() =>
                {
                    AutoUpdater.Synchronous = true;
                    AutoUpdater.Start(Config.UPDATE_URL);
                });
                worker.IsBackground = true;
                worker.SetApartmentState(System.Threading.ApartmentState.STA);
                worker.Start();
            }
        }

        protected void InitilizeCopiesPath()
        {
            try
            {
                if (!Directory.Exists(Utils.DefaultCopiesPath()))
                {
                    Directory.CreateDirectory(Utils.DefaultCopiesPath());
                }
                if (!Directory.Exists(Settings.Default.TargetPath) && String.IsNullOrEmpty(Settings.Default.TargetPath))
                {
                    Directory.CreateDirectory(Settings.Default.TargetPath);
                }
            }
            catch { }
        }

        /// <summary>
        /// Starts the auto copy USB.
        /// </summary>
        public void Start()
        {
            Settings.Default.Enabled = true;
            Settings.Default.Save();
            Utils.HideForm(this);
        }

        /// <summary>
        /// Stops the auto copy USB.
        /// </summary>
        public void Stop() {
            Settings.Default.Enabled = false;
            Settings.Default.Save();
            Utils.ShowForm(this);
        }
    }
}