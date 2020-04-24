using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleStock
{
    public class StockInfoReader
    {

        public string ReadJSONFromFile(string filePath)
        {

            return System.IO.File.ReadAllText(filePath);
        }

        public string ReadJSONFromUrl(string url)
        {
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            client.Headers.Add("user-agent", GenerateUserAgent());

            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            
            string jsonResult = client.DownloadString(url);

            return jsonResult;
        }

        public StockModel Execute(string jsonResult)
        {
            var jsonContent = JsonConvert.DeserializeObject<JObject>(jsonResult);
            JToken subJson;
            StockModel stockModel = null;

            //避免代碼為上市但查上櫃
            if (jsonResult.IndexOf("\"d\"") < 0)
            {
                return stockModel;
            }

            foreach (var item in jsonContent)
            {
                subJson = item.Value;
                var child = subJson.Children();

                foreach (JToken token in child)
                {
                    var oo = token.Root.ToString();
                    stockModel = JsonConvert.DeserializeObject<StockModel>(oo);
                    stockModel.NowDateTime = ParseMilisecond(stockModel.msgArray[0].Miliseconds.ToString());
                    break;
                }

                if (stockModel != null)
                {
                    break;
                }

            }
            return stockModel;
        }

        private DateTime ParseMilisecond(string dSecs)
        {
            double d = double.Parse(dSecs);
            TimeSpan time = TimeSpan.FromMilliseconds(d);
            DateTime startdate = new DateTime(1970, 1, 1) + time;
            startdate = startdate.AddHours(8d);

            return startdate;
        }

        private string GenerateUserAgent()
        {
            string mozila = "Mozilla/5.0+(Windows+NT+10.0;+Win64;+x64;+rv:65.0)+Gecko/20100101+Firefox/65.0";
            string apple = "Mozilla/5.0+(Windows+NT+10.0;+Win64;+x64)+AppleWebKit/537.36+(KHTML,+like+Gecko)+Chrome/72.0.3626.109+Safari/537.36";

            int min = DateTime.Now.Minute;

            if(min % 2 == 0)
            {
                return mozila;
            }

            return apple;
        }

    }

}
