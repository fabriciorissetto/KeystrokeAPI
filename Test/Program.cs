using Keystroke.API;
using System;
using System.Windows.Forms;

namespace ConsoleApplicationTest
{
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
}
