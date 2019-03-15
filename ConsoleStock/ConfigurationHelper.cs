using System;
using System.Configuration;


namespace ConsoleStock
{
    public class ConfigurationHelper
    {

        public static string GetURLString()
        {


            return ConfigurationManager.AppSettings["StockUrl"].ToString();
        }

    }
}
