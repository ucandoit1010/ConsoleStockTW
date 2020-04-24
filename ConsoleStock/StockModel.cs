using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace ConsoleStock
{
    public class StockModel
    {
        [JsonProperty("msgArray")]
        public List<StockData> msgArray { get; set; }

        public DateTime NowDateTime { get; set; }
        

    }
}
