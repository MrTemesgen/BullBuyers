using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplication1.Domain;
using Newtonsoft.Json.Linq;
namespace WebApplication1.Data
{
    public class HtmlRepository
    {
        static String CIKMapping = System.IO.File.ReadAllText(".\\Application\\ticker_cik_map.txt");

        public static string RetriveDataFromStockDocuments(string DataPath, Stock stock, int caseVal)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                switch (caseVal)
                {
                    // First two cases are HTML docs so its better to index using Xpaths
                    case 1:
                        doc = stock.SECCik;
                        break;
                    case 2:
                        doc = stock.SECInsider;
                        break;
                    case 3:
                        //this is actually json so we can just serialize and index
                        doc = stock.StockAnalyzer;
                        return JObject.Parse(doc.DocumentNode.OuterHtml)[DataPath].ToString();
                        
                }
                    
                var node = doc.DocumentNode.SelectSingleNode(DataPath);
                try
                {
                    return node.OuterHtml;
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }


        }





        static  HtmlDocument ReadTextFromUrl(string url)
        {
            WebRequest request;
            request = WebRequest.Create(url);
            ServicePointManager.DefaultConnectionLimit = 200;
            try
            {
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
               
                using (StreamReader sr = new StreamReader(data))
                {
                    html = sr.ReadToEnd();
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc;
            }
            catch { }
            {
                return null;
            }
        }



        public static async Task<Stock> SetUpDocuments(Stock stock)
        {
            try
            {
               //add the CIK to the stock for accessing the correct insider trade page.
                SetUpCiK(stock);
                List<Task<HtmlDocument>> tasks = new List<Task<HtmlDocument>>();
                tasks.Add(Task.Run(() => ReadTextFromUrl($"https://api.stockanalysis.com/wp-json/sa/p?s={stock.Ticker}&t=stocks")));
                tasks.Add(Task.Run(() => ReadTextFromUrl($"https://sec.report/CIK/{stock.CIK}/Insider-Trades")));
                var documents = await Task.WhenAll(tasks);
                stock.StockAnalyzer = documents[0];
                stock.SECInsider = documents[1];

                Console.WriteLine("Finish adding: " + stock.Ticker);
                return stock;
            }
            catch (Exception e) {
                Console.WriteLine("Error Building Stock: " + stock.Ticker + " with error: "+ e);
                return null;
            }
        }


        public static void Update(Stock stock) {
            try
            {
                stock.StockAnalyzer = ReadTextFromUrl($"https://api.stockanalysis.com/wp-json/sa/p?s={stock.Ticker}&t=stocks");
                stock.Price = Stock_Data.GetPrice(stock);
                stock.PriceChange = Stock_Data.GetPriceChange(stock);
                stock.Volume = Stock_Data.GetVolume(stock);
            }
            catch (Exception e) {
                Console.WriteLine("Error Updating stock: " + e);
            }
        }

        public static void SetUpCiK(Stock stock)
        {
            
            //When the CIK is not in our mapping the default is an invlaid CIK 
            long cik = 0;
            String pat = String.Format(@"^{0}\s+(\d+)$", stock.Ticker.ToLower());
            Match reg = Regex.Match(CIKMapping, pat, RegexOptions.Multiline);
            if (reg.Success)
            {
                Int64.TryParse(reg.Groups[1].Value, out cik);
            }

            
            stock.CIK = cik;
        }

        /***
         * This method returns the weekly historical data from when the 
         * stock was made public. 
         * This may return null if the stock for some reason 
         * has not accessible historical data. 
         */
        public static JObject GetStockChartWeekly(String ticker)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    var str = wc.DownloadString($"https://widget3.zacks.com/data/chart/json/{ticker}/price_and_eps_surprise/www.zacks.com?");
                    var json = JObject.Parse(str);
                    return (JObject) json["weekly_price"];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error accessing historical stock data for {ticker}: " + e);
                return null;
            };

        }


    }


   

}
