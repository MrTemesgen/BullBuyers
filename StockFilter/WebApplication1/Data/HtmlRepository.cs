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

namespace WebApplication1.Data
{
    public class HtmlRepository
    {
        
        public static int call = 0;
        public static  string RetriveDataFromJsonAnalysis(string DataPath, string URL, HtmlDocument DocumentAnalysis = null, string GlobalURL = null)
        {
            try
            {
                if (DocumentAnalysis == null || GlobalURL != URL)
                {

                    GlobalURL = URL;

                    var html = URL;

                    DocumentAnalysis = ReadTextFromUrl(html);
                 

                }



                var node = DocumentAnalysis.DocumentNode.SelectSingleNode(DataPath);
                try
                {
                    return node.OuterHtml;
                }
                catch (NullReferenceException e)
                {
                    return null;
                }
            }
            catch (WebException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
                return null;
            }


        }


        public static string RetriveDataFromStockDocuments(string DataPath, Stock stock, int caseVal)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                switch (caseVal)
                {
                    case 1:
                        doc = stock.SECCik;
                        break;
                    case 2:
                        doc = stock.SECInsider;
                        break;
                    case 3:
                        doc = stock.StockAnalyzer;
                        break;
                }
                    
                var node = doc.DocumentNode.SelectSingleNode(DataPath);
                try
                {
                    return node.OuterHtml;
                }
                catch (NullReferenceException e)
                {
                    return null;
                }
            }
            catch (WebException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
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
                Console.WriteLine("Getting Resource");
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
                Stopwatch watch = new Stopwatch();
                watch.Start();
                SetUpCiK(stock);
                watch.Stop();
                
                Console.WriteLine("Loading CIK took: " + watch.ElapsedMilliseconds);
                Console.WriteLine("Begin adding: " + stock.Ticker);
                
                watch.Start();
                List<Task<HtmlDocument>> tasks = new List<Task<HtmlDocument>>();
                List<string> links = new List<string>();
                links.Add($"https://stockanalysis.com/stocks/{stock.Ticker.ToLower()}/");
                links.Add($"https://sec.report/CIK/{stock.CIK}/Insider-Trades");
                Console.WriteLine(links.Count);
                foreach (var link in links)
                {
                    Console.WriteLine("Added a page for: " + stock.Ticker);
                    tasks.Add(Task.Run(() => ReadTextFromUrl(link)));
                }
                var documents = await Task.WhenAll(tasks);
                watch.Stop();
                Console.WriteLine("Loading other pages took: " + watch.ElapsedMilliseconds);
                Console.WriteLine("Begin adding: " + stock.Ticker);
                Console.WriteLine("Documents: " + documents[0]);

                Console.WriteLine("Finished adding pages for: " + stock.Ticker);

                stock.StockAnalyzer = documents[0];
                stock.SECInsider = documents[1];

                Console.WriteLine("Finish adding: " + stock.Ticker);
                return stock;
            }
            catch (Exception e) {
                Console.WriteLine("Error Building Stock: "  + e);
                return null;
            
            }
            

        }

        public static void SetUpCiK(Stock stock)
        {
            var html = $"https://sec.report/Ticker/{stock.Ticker.ToUpper()}/";
            stock.SECCik = ReadTextFromUrl(html);
            stock.CIK = GetInsiderFilingsCIK(stock);
        }

        public static Int64 GetInsiderFilingsCIK(Stock stock)
        {
            System.Text.StringBuilder str = new StringBuilder();
            MatchCollection matches = Regex.Matches(RetriveDataFromStockDocuments("//div[contains(@class, 'jumbotron')]/h2/text()", stock, 1), "[0-9]*");
            for (int i = 0; i < matches.Count; i++)
            {
                Match match = (Match)matches[i];
                str.Append(match.Value);
              
            }
            return Int64.Parse(str.ToString());
        }
    }


   

}
