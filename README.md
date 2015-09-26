# NativeWorkbench
C# Tools and Mods for GTAV PC

There have been numerous requests for a tool that will allow you to write code in real time and see the results in game instantly.  Native Workbench combines, native lookup, native to C# syntax code generation, highlight and run, and real time variable watching all in one app.  

Whatever you write gets compiled instantly and run all with a single click. 
Any OnTick code you write runs in it’s own window so you can experiment with running single or multiple lines of code in the 
immediate window and OnTick code window, along with real time property output all at the same time.   

Just like Native Watcher, Native Workbench runs in it’s own forms application. This keeps it completely isolated and does not 
require any output to be placed or read from the game window.
You do however install it to the scripts folder just like any C# based mod 

Source code is included as a buildable VS 2013 project, so you can be confident that you are running a malware-free tool.

It is currently C# based, but since it auto-formats natives into ScriptHookVDotNet syntax, you do not have to know C# to use it

All work in the immediate and OnTick windows as well as properties are auto saved to an .ini file

There are 10 static data slots each for all common data types which are kept in memory as long as the game is running.  

I have been using this release of the tool heavily for over 2 weeks now and it has been totally stable. You may be able to crash the game by calling natives with parameters it’s not expecting but I have only been able to do it once  

Native Workbench does not alter any game or profile files. The only writing of data is to its own ini file so there is no worry of corrupting game files. 

Please check out the video tutorials as they are not only instruction on how to use the tool, but lessons in native programming of graphic UI, Cameras, and ped interaction


