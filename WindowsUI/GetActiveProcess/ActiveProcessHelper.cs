using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GetActiveProcess
{
    public static class ActiveProcessHelper
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
                
        public delegate bool WindowEnumProc(IntPtr hwnd, IntPtr lparam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc callback, IntPtr lParam);

        /// <summary>
        /// 通过句柄获取进程
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        private static Process GetProcessByHandler(IntPtr hwnd)
        {
            try
            {
                GetWindowThreadProcessId(hwnd, out var processId);
                return Process.GetProcessById((int)processId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取当前系统中被激活的窗口.
        /// </summary>
        /// <returns></returns>
        public static Process GetActiveProcess()
        {
            var hwnd = GetForegroundWindow();
            return GetProcessByHandler(hwnd);
        }

        /// <summary>
        /// 获取进程的名称
        /// </summary>
        /// <returns></returns>
        public static string GetActiveProcessName(out Process process)
        {
            process = GetActiveProcess();
            return process?.ProcessName;
        }

        
        private static Process _realProcess;
        /// <summary>
        /// 解决无法获取XD软件句柄的问题。XD软件被进程ApplicationFrameHost托管.
        /// 因此GetForegroundWindow()方法返回带有标题的窗口,但不返回托管的实际进程，
        /// 解决方法是再次通过EnumChildWindows获取ApplicationFrameHost的子窗口句柄
        /// </summary>
        /// <param name="foregroundProcess"></param>
        /// <returns></returns>
        public static Process GetRealProcess(Process foregroundProcess)
        {
            EnumChildWindows(foregroundProcess.MainWindowHandle, ChildWindowCallback, IntPtr.Zero);
            return _realProcess;
        }

        private static bool ChildWindowCallback(IntPtr hwnd, IntPtr lparam)
        {
            Process process = GetProcessByHandler(hwnd);
            if (process.ProcessName != "ApplicationFrameHost")
            {
                _realProcess = process;
            }
            return true;
        }
    }
}
