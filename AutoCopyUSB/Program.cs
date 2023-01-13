using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace AutoCopyUSB
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "AutoCopyUSB", out createdNew))
            {
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1(args));
                }
                else
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            IntPtr hwndMessageWasSentTo = IntPtr.Zero;
                            uint msg = 313;
                            IntPtr wParam = IntPtr.Zero;
                            IntPtr lParam = IntPtr.Zero;
                            IntPtr returnValue = process.SendMessage(out hwndMessageWasSentTo, msg, wParam, lParam);
                            if (hwndMessageWasSentTo != IntPtr.Zero)
                            {
                                //MessageBox.Show("Message successfully sent to hwnd: " + hwndMessageWasSentTo.ToString() + " and return value was: " + returnValue.ToString());
                            }
                            else
                            {
                                //MessageBox.Show("No windows found in process.  SendMessage was not called.");
                            }
                            SetForegroundWindow(process.MainWindowHandle);
                            break;
                        }
                    }
                }
            }
        }
    }
}
