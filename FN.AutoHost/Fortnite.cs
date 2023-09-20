using IniParser.Model;
using IniParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FN.AutoHost
{

    internal class Fortnite
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
        public static void SafeKillProcess(string processName)
        {
            try
            {
                Process[] processesByName = Process.GetProcessesByName(processName);
                for (int i = 0; i < processesByName.Length; i++)
                {
                    processesByName[i].Kill();
                }
            }
            catch
            {
            }
        }

        internal static void DownloadFile(string URL, string path)
        {
            new WebClient().DownloadFile(URL, path);
        }
        public static bool bGameserver = false;
        public static void Inject(int pid, string path)
        {
            if (!File.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[!] Could not find {path.Split('\\').Last()}. Exiting in 3 Seconds");
                Thread.Sleep(3000);
                Environment.Exit(0);
                return;
            }
            IntPtr hProcess = Win32.OpenProcess(1082, false, pid);
            IntPtr procAdress = Win32.GetProcAddress(Win32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            uint num = checked((uint)((path.Length + 1) * Marshal.SizeOf(typeof(char))));
            IntPtr intPtr = Win32.VirtualAllocEx(hProcess, IntPtr.Zero, num, 12288U, 4U);
            UIntPtr uintPtr;
            Win32.WriteProcessMemory(hProcess, intPtr, Encoding.Default.GetBytes(path), num, out uintPtr);
            Win32.CreateRemoteThread(hProcess, IntPtr.Zero, 0U, procAdress, intPtr, 0U, IntPtr.Zero);
        }

        public static void Launch(string path)
        {
            try
            {
                var Parser = new FileIniDataParser();
                IniData data = Parser.ReadFile(Directory.GetCurrentDirectory() + "\\Settings.ini");
               

                // fake anticheats (its a empty exe that just stays open nothing bad)
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\FortniteClient-Win64-Shipping_BE.exe"))
                {
                    DownloadFile("https://cdn.discordapp.com/attachments/958139296936783892/1000707724507623424/FortniteClient-Win64-Shipping_BE.exe", Directory.GetCurrentDirectory() + "\\FortniteClient-Win64-Shipping_BE.exe");
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\FortniteLauncher.exe"))
                {
                    DownloadFile("https://cdn.discordapp.com/attachments/958139296936783892/1000707724818006046/FortniteLauncher.exe", Directory.GetCurrentDirectory() + "\\FortniteLauncher.exe");
                }
                // commented out because dumb virus flag
                if (!File.Exists("C:\\Windows\\System32\\D3DCompiler_43.dll"))
                {
                    // most vps dont have this, it removes the error that you get when fortnite crashes
                    DownloadFile("https://cdn.discordapp.com/attachments/1097279364145614859/1129091037680369716/D3DCompiler_43.dll", "C:\\Windows\\System32\\D3DCompiler_43.dll");
                }
                Process.Start(new ProcessStartInfo
                {
                    FileName = Directory.GetCurrentDirectory() + "\\FortniteLauncher.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                Process.Start(new ProcessStartInfo
                {
                    FileName = Directory.GetCurrentDirectory() + "\\FortniteClient-Win64-Shipping_BE.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
            start:
                SafeKillProcess("FortniteClient-Win64-Shipping_BE");
                SafeKillProcess("FortniteLauncher");
                SafeKillProcess("FortniteClient-Win64-Shipping");
                SafeKillProcess("CrashReportClient");
                Process proc = new Process();
                proc.StartInfo.FileName = path + @"\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe";
                proc.StartInfo.Arguments = $"-epicapp=Fortnite -epicenv=Prod -epicportal -AUTH_TYPE=epic -AUTH_LOGIN={data["Settings"]["Email"]} -AUTH_PASSWORD={data["Settings"]["Password"]} -epiclocale=en-us -fltoken=7a848a93a74ba68876c36C1c -fromfl=none -noeac -nobe -skippatchcheck -nullrhi -nosound ";
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();

                Inject(proc.Id, Directory.GetCurrentDirectory() + "\\Redirect.dll");

                // Interval for Checking if FortniteClient-Win64-Shipping Closed.

                string[] LoginFailureErrs = {  "port 3551 failed: Connection refused", "Unable to login to Fortnite servers", "HTTP 400 response from ", "Network failure when attempting to check platform restrictions","UOnlineAccountCommon::ForceLogout" };

                try
                {
                    for (; ; )
                    {
                        string output = proc.StandardOutput.ReadLine();
                        if (output != null)
                        {
                            // wont inject until login because if your not gs will have a stroke
                            if (output.Contains("Region "))
                            {

                                Thread.Sleep(5000);
                                Inject(proc.Id, Directory.GetCurrentDirectory() + $"\\{data["Settings"]["GameServerDllName"]}");
                                bGameserver = true;

                            }

                            if (LoginFailureErrs.Any(err => output.Contains(err)))
                            {
                                Console.WriteLine($"A token error occured, restarting server, Error: {output}");
                                goto start;
                            }
                        }
                        else
                        {
                            var fortnite = Process.GetProcessesByName("FortniteClient-Win64-Shipping");
                            if (fortnite.Length == 0 && bGameserver)
                            {
                                Console.WriteLine("Restarting Server");
                                goto start;
                            }
                        /*    // Meaning Cobalt is on the wrong setting
                            if (!bGameserver && fortnite.Length != 0)
                            {
                                SafeKillProcess("FortniteClient-Win64-Shipping_BE");
                                SafeKillProcess("FortniteLauncher");
                                SafeKillProcess("FortniteClient-Win64-Shipping");
                                SafeKillProcess("CrashReportClient");

                                MessageBox((IntPtr)0, "It seems you have the incorrect setting on your cobalt dll! Make sure that you removed the '#define SHOW_WINDOWS_CONSOLE' line in Settings.h!", "Error", 0);
                                Environment.Exit(0);
                            }*/

                        }



                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {

                }
            } catch
            {

            }
        }
    }
}
