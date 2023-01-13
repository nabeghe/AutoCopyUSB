using AutoCopyUSB.Properties;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace AutoCopyUSB
{
    public static partial class Utils
    {
        /// <summary>
        /// Gets the application name.
        /// </summary>
        /// <returns></returns>
        public static string ApplicationName()
        {
            return Process.GetCurrentProcess().ProcessName;
        }

        public static string CurrentDateTime()
        {
            var now = DateTime.Now;
            return $"{now.Year}-{now.Month}-{now.Day} {now.Hour}-{now.Minute}-{now.Second}";
        }

        /// <summary>
        /// Gets the default copies path.
        /// </summary>
        /// <returns></returns>
        public static string DefaultCopiesPath()
        {
            return Environment.CurrentDirectory + "\\Copies";
        }

        public static void ExportPassport(string directoryPath)
        {
            var passportPath = directoryPath + "\\" + Config.PASSPORT_FILE_NAME;
            if (File.Exists(passportPath)) File.Delete(passportPath);
            File.WriteAllText(passportPath, Settings.Default.Password);
        }

        /// <summary>
        /// Gets the root path of copies.
        /// </summary>
        /// <returns></returns>
        public static string GetCopiesPath()
        {
            // If the custom path not set, so return the default path.
            if (String.IsNullOrEmpty(Settings.Default.TargetPath))
            {
                return DefaultCopiesPath();
            }
            // Return the custom path.
            return Settings.Default.TargetPath;
        }

        /// <summary>
        /// Gets a drive volum label by it's device id.
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public static string GetDriveLabelByDeviceID(string deviceID)
        {
            return GetDriveLabelByLetter(GetDriveLetterByDeviceID(deviceID));
        }

        /// <summary>
        /// Gets a drive volume label by it's letter.
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public static string GetDriveLabelByLetter(string letter)
        {
            try
            {
                // sanitize letter.
                letter = letter.Replace(":", "").ToLower() + ":\\";

                // Get all drives.
                DriveInfo[] drives = DriveInfo.GetDrives();

                // For each drive.
                foreach (DriveInfo drive in drives)
                {
                    // If the drive name is same with the letter.
                    if (drive.Name.ToLower().Equals(letter))
                    {
                        // if the drive not ready, so break the loop.
                        if (!drive.IsReady)
                        {
                            break;
                        }
                        // return the volum name of the drive.
                        return drive.VolumeLabel;
                    }
                }
            }
            catch (Exception)
            {
            }
            // return null if nothing.
            return null;
        }

        /// <summary>
        /// Gets the Letter of an USB Device (Like External Hard Disks)
        /// </summary>
        /// <param name="deviceID">Device ID</param>
        /// <returns></returns>
        public static string GetDriveLetterByDeviceID(string deviceID)
        {
            string query = string.Format("SELECT * FROM Win32_LogicalDisk WHERE DeviceID='{0}'", deviceID.Replace(@"\", @"\\"));

            foreach (ManagementObject device in new ManagementObjectSearcher(@"SELECT * FROM Win32_DiskDrive WHERE InterfaceType LIKE 'USB%'").Get())
            {
                Console.WriteLine((string)device.GetPropertyValue("DeviceID"));
                Console.WriteLine((string)device.GetPropertyValue("PNPDeviceID"));

                foreach (ManagementObject partition in new ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + device.Properties["DeviceID"].Value
                    + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                {
                    foreach (ManagementObject disk in new ManagementObjectSearcher(
                                "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                                    + partition["DeviceID"]
                                    + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                    {
                        return disk["Name"].ToString().Replace(":", "");
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a drive path by device id.
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public static string GetDrivePathByDeviceID(string deviceID)
        {
            string letter = GetDriveLetterByDeviceID(deviceID);
            if (letter == null) return null;
            return letter + ":\\";
        }

        /// <summary>
        /// Hide a form.
        /// </summary>
        /// <param name="frm"></param>
        public static void HideForm(Form frm)
        {
            frm.ShowInTaskbar = false;
            frm.Visible = false;
            frm.Hide();
        }

        /// <summary>
        /// Checks that a form is hidded or not.
        /// </summary>
        /// <param name="frm"></param>
        /// <returns></returns>
        public static bool IsHiddenForm(Form frm)
        {
            return !frm.Visible && frm.WindowState == FormWindowState.Minimized && frm.ShowInTaskbar == false;
        }

        public static void Log(string msg)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(CurrentDateTime() + ": " + msg + "\n");
                File.AppendAllText(Environment.CurrentDirectory + "\\log.txt", sb.ToString());
                sb.Clear();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Makes a new folder in the copies path.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string MakeNewCopyTargetPath(string prefix = "", string suffix = "")
        {
            
            var path = (String.IsNullOrEmpty(Settings.Default.TargetPath) ? DefaultCopiesPath() : Settings.Default.TargetPath) + "\\" + $"{prefix}{CurrentDateTime()}{suffix}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// Converts (hash) a string to md5.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5(string input)
        {
            var provider = System.Security.Cryptography.MD5.Create();
            StringBuilder builder = new StringBuilder();

            foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(input)))
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }

        public static bool RequestPassword()
        {
            return VerifyPassword(Interaction.InputBox("Password?", "", ""));
        }

        public static void SetPassword(string password)
        {
            Settings.Default.Password = MD5(password);
            Settings.Default.Save();
        }

        /// <summary>
        /// Show a form that hidded (unhide).
        /// </summary>
        /// <param name="frm"></param>
        public static void ShowForm(Form frm)
        {
            frm.ShowInTaskbar = true;
            frm.Visible = true;
            frm.Show();
        }

        public static Form ProccessHandleToForm(IntPtr handle)
        {
            return handle == IntPtr.Zero ? null : Control.FromHandle(handle) as Form;
        }

        public static bool VerifyPassword(string password, bool isMD5 = false)
        {
            if (!isMD5)
            {
                password = Utils.MD5(password);
            }
            return password.Equals(Settings.Default.Password);
        }
    }
}
