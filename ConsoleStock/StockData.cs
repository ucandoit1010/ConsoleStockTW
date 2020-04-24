using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;



namespace ConsoleStock
{
    public class StockData
    {

        [JsonProperty("o")]
        public decimal Open { get; set; }

        [JsonProperty("h")]
        public decimal High { get; set; }

        [JsonProperty("l")]
        public decimal Low { get; set; }

        [JsonProperty("z")]
        public decimal Current { get; set; }

        [JsonProperty("ch")]
        public string Code { get; set; }

        [JsonProperty("t")]
        public string Time { get; set; }

        [JsonProperty("tlong")]
        public double Miliseconds { get; set; }
    }
}
