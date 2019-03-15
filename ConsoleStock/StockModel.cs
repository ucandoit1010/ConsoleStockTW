using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace ConsoleStock
{
    public class StockModel
    {

        [JsonProperty("o")]
        public decimal Open { get; set; }

        [JsonProperty("z")]
        public decimal Current { get; set; }

        [JsonProperty("h")]
        public decimal High { get; set; }

        [JsonProperty("l")]
        public decimal Low { get; set; }

        [JsonProperty("ch")]
        public string Code { get; set; }

        [JsonProperty("tlong")]
        public double Miliseconds { get; set; }

        [JsonProperty("t")]
        public string Time { get; set; }


        public DateTime NowDateTime { get; set; }
        

    }
}
