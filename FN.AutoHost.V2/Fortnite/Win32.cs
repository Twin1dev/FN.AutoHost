using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FN.AutoHost.V2.Fortnite
{
    internal class Win32
    {
        [DllImport("kernel32.dll")]
        public static extern int SuspendThread(IntPtr hThread);


        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(IntPtr hThread);


        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, int dwThreadId);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);


        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);


        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);


        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);


        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(SetConsoleCtrlEventHandler HandlerRoutine, bool Add);

        /// <summary>
        /// An application-defined function used with the SetConsoleCtrlHandler function.
        /// </summary>
        /// <param name="dwCtrlType">The type of control signal received by the handler.</param>
        /// <returns>If the function handles the control signal, it should return TRUE. If it returns FALSE, the next handler function in the list of handlers for this process is used.</returns>
        public delegate bool SetConsoleCtrlEventHandler(CtrlType dwCtrlType);

        /// <summary>
        /// The type of control signal received by the handler.
        /// </summary>
        public enum CtrlType
        {
            /// <summary>
            /// A CTRL+C signal was received.
            /// </summary>
            CTRL_C_EVENT = 0,

            /// <summary>
            /// A CTRL+BREAK signal was received.
            /// </summary>
            CTRL_BREAK_EVENT = 1,

            /// <summary>
            /// A signal that the system sends to all processes attached to a console when the user closes the console.
            /// </summary>
            CTRL_CLOSE_EVENT = 2,

            /// <summary>
            /// A signal that the system sends to all console processes when a user is logging off.
            /// </summary>
            CTRL_LOGOFF_EVENT = 5,

            /// <summary>
            /// A signal that the system sends when the system is shutting down. 
            /// </summary>
            CTRL_SHUTDOWN_EVENT = 6
        }



        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, InternetOptions dwOption, IntPtr lpBuffer, uint dwBufferLength);

        /// <summary>
        /// The following option flags are used with the InternetQueryOption and InternetSetOption functions.
        /// </summary>
        internal enum InternetOptions : int
        {
            /// <summary>
            /// Causes the proxy data to be reread from the registry for a handle.
            /// </summary>
            INTERNET_OPTION_REFRESH = 37,

            /// <summary>
            /// Notifies the system that the registry settings have been changed so that it verifies the settings on the next call to InternetConnect.
            /// </summary>
            INTERNET_OPTION_SETTINGS_CHANGED = 39
        }
    }
}
