using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ConsoleStock
{
    class Program
    {
        static void Main(string[] args)
        {

            do
            {
                Console.WriteLine("Give me number ...");

                SetWindowSize();

                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Environment.Exit(0);
                }
                else
                {
                    if (input.Length > 4)
                    {
                        Console.WriteLine("Wrong Code");
                    }

                    StockInfoReader reader = new StockInfoReader();
                    List<StockData> modelData = new List<StockData>();
                    //string content = reader.ReadJSONFromFile(@"D:\stock.json");
                    string url = string.Format(
                        "http://mis.twse.com.tw/stock/api/getStockInfo.jsp?ex_ch=tse_{0}.tw",
                         input);
                    string content;
                    content = reader.ReadJSONFromUrl(url);
                    StockModel stockModel = reader.Execute(content);

                    if (stockModel == null)
                    {
                        url = string.Format(
                            "http://mis.twse.com.tw/stock/api/getStockInfo.jsp?ex_ch=otc_{0}.tw",
                             input);
                        content = reader.ReadJSONFromUrl(url);
                        stockModel = reader.Execute(content);

                        
                    }
                    if (stockModel == null)
                    {
                        Console.WriteLine("********Code:{0} No Data********", input);
                    }
                    else
                    {
                        modelData = (stockModel == null ? new List<StockData>() : stockModel.msgArray);
                        StockData data = modelData[0];

                        Console.WriteLine("************Code:{0}***********", data.Code); Console.WriteLine("H:{0}  , L:{1}", data.High.ToString("##.##"), data.Low.ToString("##.##"));
                        Console.WriteLine("O:{0}  , C:{1} , Sbt:{2}",
                            data.Open.ToString("##.##"), data.Current.ToString("##.##"), ((data.Current - data.Open) * 1000).ToString("##.##"));
                        Console.WriteLine("Now:{0}", stockModel.NowDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        Console.WriteLine("*************END************");                        
                    }

                    Console.WriteLine();
                }
            }
            while (true);

        }


        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetMyWindowPosition(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, uint wFlags);

        private static void SetWindowSize()
        {

            var thisWindow = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            SetMyWindowPosition(thisWindow, 1, 0, 750, 0, 0, 1 | 4);


            Console.SetWindowSize(40, 15);
        }

    }
}
