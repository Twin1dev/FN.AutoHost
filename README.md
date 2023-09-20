# FN.AutoHost
 Fortnite AutoHost for any gameserver and version! For any issues go to here: [issues](https://github.com/Twin1dev/FN.AutoHost#common-issues)

 ## Requirements
 - [Cobalt](https://github.com/Milxnor/Cobalt/tree/main) (MUST REMOVE ```#define SHOW_WINDOWS_CONSOLE``` IN SETTINGS.h!)
 - [Gameserver](https://github.com/Milxnor/Project-Reboot-3.0) (Reboot V3 or anything else)
 - Common knowlege on how to run a computer (apparently thats hard for alot of people that use this)

 ## Installation

 1. Download Release
 2. Put your redirect in the folder of the FN.AutoHost.exe (make sure its named ``Redirect.dll``)
 3. Put your gameserver in the folder of FN.AutoHost.exe (make sure its named ``Gameserver.dll``)
 4. Open FN.AutoHost.exe and follow the instructions!
 5. (FOR LAWINV2 USERS) Make a new user with the details you provided in Settings.ini!

 ## Common issues
 - When using reboot v3 i get a "Network Connection Lost" error. how do i fix this?
   - Make a new account in your LawinV2 backend with the email ```server@projectreboot.dev``` and Password ```Rebooted```, you can change this in Settings.ini
 - My game keeps crashing on startup??
   - Most common issues on vps's, they dont support games. you can easily fix startup crashes by changing your cpu settings. Go to this message for how to do it: [Message](https://discord.com/channels/1097271368896217108/1110484555581886594/1142995356871827489)
 ## FAQ 
 - How do i change the saved path?
   - Open Change Path.bat
 - My autohost crashed?
   - Make sure you have .net framework 4.8 installed and that everything is correct (path, gameserver, redirect)
 - My game isnt closing after ive won?
   - Right now i am not providing support for the gameserver side of autohosting. you will need someone or yourself to add that.
