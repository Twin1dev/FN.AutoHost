using FN.AutoHost.Classes;
using System.Runtime.InteropServices;

[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

Gui gui = new Gui();
Thread guiThread = new Thread(gui.Start().Wait);
guiThread.Start();

var handle = GetConsoleWindow();

ShowWindow(handle,0);
