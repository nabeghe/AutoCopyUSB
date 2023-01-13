using Microsoft.Win32;
using System.Windows.Forms;

namespace AutoCopyUSB
{
    internal class RegisteryHelper
    {
        private const string STARTUP_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static void SetStartupEnabled(bool enabled)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(STARTUP_KEY, true);
            if (enabled)
            {
                rk.SetValue(Utils.ApplicationName(), Application.ExecutablePath.ToString() + " -s");
            }
            else
            {
                rk.DeleteValue(Utils.ApplicationName());
            }
        }

        public static bool GetStartupEnabled()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(STARTUP_KEY, true);
            var value = rk.GetValue(Utils.ApplicationName());
            return value != null && value.ToString().Equals(Application.ExecutablePath.ToString() + " -s");
        }
    }
}
