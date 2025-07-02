using AutoCopyUSB.Properties;
using System.Windows.Forms;

namespace AutoCopyUSB
{
    public partial class Form1
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 313)
            {
                if ((!this.Visible || Settings.Default.Enabled) && Utils.RequestPassword())
                {
                    Stop();
                }
            }

            base.WndProc(ref m);
        }
    }
}
