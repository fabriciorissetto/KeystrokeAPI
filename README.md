# KeyloggerAPI
A simple Keylogger API written in C# that works for any version of Windows. It abstracts the access to the *win32.dll* and the manipulation of low level hooks (only keyboard for now). 

## Instalation
```sh
Install-Package KeyloggerAPI
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


## 'Console Application' example
```c#
class Program
{
	static void Main(string[] args)
	{
		using (var api = new API())
		{
			api.CreateKeyboardHook((character) => { Console.Write(character); });
			Application.Run();
		}
	}
}
```

   [Click here to know why]: <http://stackoverflow.com/a/7460728/890890>

