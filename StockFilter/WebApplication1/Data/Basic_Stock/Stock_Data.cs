using HtmlAgilityPack;
using ServiceStack;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplication1.Domain;
using System.Globalization;

namespace WebApplication1.Data
{
   
    public static class Stock_Data
    {
        public static HtmlDocument DocumentAnalysis = new HtmlDocument();
        
        public static string GlobalURL;
        public static async Task<Stock> GetDoc( Stock stock) {
            await HtmlRepository.SetUpDocuments(stock);
            return stock;
        }
     
        public static DateTime GetEarningsDay(Stock stock) {
            var xss = HtmlRepository.RetriveDataFromStockDocuments("e", stock,3);
            Console.WriteLine("Earnings day: " + xss);
            DateTime.TryParse(new Regex(@"[^\>]+(?=\<)").Match(xss).ToString(), out DateTime date);
            return date;
        }
        public static Double GetPrice(Stock stock) {
            var xss = HtmlRepository.RetriveDataFromStockDocuments("p", stock, 3);
            Console.WriteLine("Price @ " + xss);
            Double.TryParse(xss, out double price);
            return price;
        }
        public static double GetPriceChange(Stock stock) {
            var pc = HtmlRepository.RetriveDataFromStockDocuments("cp", stock, 3);
            Double.TryParse(pc, out double change);
            return change;
        }
        public static long GetVolume(Stock stock) {
            var xss =  HtmlRepository.RetriveDataFromStockDocuments("v", stock, 3);
            Console.WriteLine("volume: "+xss);
            Int64.TryParse(xss, System.Globalization.NumberStyles.AllowThousands, null, out long vol);
            return vol;
        }
        public static String GetCEO(Stock stock) {
            String pattern = @"<li>([a-zA-Z\s]+)(([a-zA-Z\s<>;%\d&]*)((CEO)|(Chief Executive Officer)))";
            var xss = HtmlRepository.RetriveDataFromStockDocuments("/html/body/div/div/div/div/ul", stock, 2);
            var result = Regex.Match(xss, pattern);
            if (result.Success && result.Groups.Count >= 1) {
                TextInfo ti = new CultureInfo("en-US", false).TextInfo;
                return  ti.ToTitleCase(result.Groups[1].Value.ToLower());
            }
            else {
                return "Not Found";
            }
        }

        public static String GetName(Stock stock) {
            String xss = HtmlRepository.RetriveDataFromStockDocuments("//div[contains(@class, 'organization-name')]/text()", stock, 2);
            Console.WriteLine(xss);
            xss = xss.ReplaceAll("amp;", "");
            return xss;
        }


       
    }
}
