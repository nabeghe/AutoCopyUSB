using AutoCopyUSB.Extensions;
using AutoCopyUSB.Properties;
using System;
using System.IO;
using Usb.Events;

namespace AutoCopyUSB
{
    /// <summary>
    /// Detect USB Drives.
    /// 
    /// https://www.nuget.org/packages/Usb.Events
    /// https://github.com/Jinjinov/Usb.Events
    /// </summary>
    public partial class Form1
    {
        private void InitilizeDeviceDetector()
        {
            IUsbEventWatcher usbEventWatcher = new UsbEventWatcher();
            usbEventWatcher.UsbDriveMounted += UsbEventWatcher_UsbDriveMounted;
        }

        private void UsbEventWatcher_UsbDriveMounted(object sender, string path)
        {
            try
            {
                if (!Settings.Default.Enabled)
                {
                    return;
                }

                var driveLetter = path[0].ToString();

                var driveLabel = Utils.GetDriveLabelByLetter(driveLetter);
                if (driveLabel == null) driveLabel = "";

                var targetCopiesPath = Utils.MakeNewCopyTargetPath("", $" ({driveLetter}) {driveLabel}");

                if (File.Exists(path + "\\" + Config.PASSPORT_FILE_NAME))
                {
                    var passportContent = File.ReadAllText(path + "\\" + Config.PASSPORT_FILE_NAME);
                    if (Utils.VerifyPassword(passportContent, true))
                    {
                        return;
                    }
                }

                var source = new DirectoryInfo(path);

                source.CopyTo(targetCopiesPath, true, true, () =>
                {
                    return !Settings.Default.Enabled;
                });
            }
            catch (Exception ex)
            {
                Utils.Log("UsbEventWatcher_UsbDriveMounted - " + ex.Message);
            }
        }
    }
}