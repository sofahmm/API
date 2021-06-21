// OMdB
// KSofia 220
using System;
using System.IO;
using Telegram.Bot;
using Newtonsoft.Json;
using lib_pars;
using System.Net;
using System.Collections.Generic;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {

            TelegramBotClient bot = new TelegramBotClient("1753600323:AAFvd9LuJq7Cew9HlfaJzVqDjo3rQAitwnI");

            bot.OnMessage += (s, arg) =>
            {
                Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                

                if(arg.Message.Text == "/start")
                {
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Ваш бот стартовал - введите название фильма"); 
                }

                else
                {
                    string result = Omdb(arg.Message.Text);
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, result);
                }
            };


            bot.StartReceiving();
            

            Console.ReadKey();
        }
        public static string Omdb(string film)
        {
            var apikey = "9ee77822";
            var movie = "Split";
            var url = $"http://www.omdbapi.com/?apikey={apikey}&t={movie}";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return null;
            }
            Root OMdB;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                Console.WriteLine(result);
                OMdB = JsonConvert.DeserializeObject<Root>(result);
                Console.WriteLine(OMdB.Website);
            }
            return OMdB.Website;
        }
    }
}