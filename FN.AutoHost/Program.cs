using FN.AutoHost.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FN.AutoHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0) {
                if (args.Contains("-change"))
                {
                    Console.WriteLine("What do you want to change the path to?");
                    Console.Write(">> ");
                    string path2 = Console.ReadLine();
                    Settings.Default.path = path2;
                    Settings.Default.Save();
                    Console.WriteLine("Saved!");
                } else if (args.Contains("-clear"))
                {
                    Settings.Default.Reset();
                    Settings.Default.Save();
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[+] Started FN AutoHost");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[+] Starting Fortnite");
        

            if (!Settings.Default.first)
            {
                Console.WriteLine("[-] Input Fortnite Path");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">> ");
                string path = Console.ReadLine();
                if (!Directory.Exists(path))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[-] Entered Path Doesnt Exist! Exiting in 3 Seconds");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }
            start:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("It seems this is your first time using FN.AutoHost! Would you like to save this path? (you can change by opening 'change.bat'\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Yes/No");
                Console.Write(">> ");
                string output = Console.ReadLine();

                
                if (output.ToLower() != "yes" && output.ToLower() != "no")
                {
                    Console.WriteLine("Invalid response! Please respond with yes or no!");
                    goto start;
                }




                if (output.ToLower() == "yes")
                    Settings.Default.path = path;
                else
                    Console.WriteLine("Warning! this is unsupported!");
                Settings.Default.first = true;
                Settings.Default.Save();
            }
            Fortnite.Launch(Settings.Default.path);
        }
    }
}
