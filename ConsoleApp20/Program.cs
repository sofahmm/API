// OMdB
// KSofia 220

using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using lib_pars;

namespace OpenWeather
{
    class Program
    {
        static void Main(string[] args)
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
                return;
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                Console.WriteLine(result);
                var OMdB = JsonConvert.DeserializeObject<Root>(result);
                Console.WriteLine(OMdB.Website);
            }

        }
        
    }
    
}