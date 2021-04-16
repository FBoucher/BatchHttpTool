using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BatchHttpTools.Domain;
using System.Configuration;
using Newtonsoft.Json;

namespace BatchHttpTools
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await MainMenu();
                if (showMenu)
                {
                    Console.WriteLine("\n\n\n  Press any key to continue.");
                    Console.ReadLine();
                }
            }
        }

        private static async Task<bool> MainMenu()
        {
            Console.Clear();

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Execute All calls");
            Console.WriteLine("2) Approve/skip them one by one");
            Console.WriteLine("3) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("1) Execute All calls - Selected");
                    await DoHttpCalls(false);
                    return true;
                case "2":
                    Console.WriteLine("2) Approve/skip them one by one - Selected");
                    await DoHttpCalls(true);
                    return true;
                case "3":
                    Console.WriteLine("3) Quit - Selected");
                    return false;
                default:
                    return true;
            }
        }

        static async Task DoHttpCalls(bool isAutimatic){

            var callsToDo = GetCallList();
            foreach(HttpCall c in callsToDo.Calls){
                try{
                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync(c.UrlRequest);
                    var filePath = Path.Combine( callsToDo.OutPutFolder, c.FilePath );
                    using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                }
                catch(Exception ex){
                    Console.WriteLine(ex.Message);
                }
            }

        }

        static HttpCallList GetCallList(){
            string listFilename = ConfigurationManager.AppSettings.Get("HttpCallsFilename");
            var todos = JsonConvert.DeserializeObject<HttpCallList>(File.ReadAllText(listFilename));
            return todos;
        }

    }

    
}
