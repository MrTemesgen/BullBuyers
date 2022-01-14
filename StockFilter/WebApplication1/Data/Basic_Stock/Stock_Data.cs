using HtmlAgilityPack;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WebApplication1.Data;
using WebApplication1.Domain;

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
     
        public static DateTime GetEarningsDay(string ticker) {
            var xss = RetriveDataFromJsonAnalysis(ticker, "/html/body/div/div/div[2]/main/div/div[2]/div[1]/div[2]/table/tbody/tr[1]/td[2]", $"https://stockanalysis.com/stocks/{ticker.ToLower()}/statistics/");
            Console.WriteLine("Earnings day: " + xss);
            DateTime.TryParse(new Regex(@"[^\>]+(?=\<)").Match(xss).ToString(), out DateTime date);
            return date;
        }
        public static Double GetPrice(Stock stock) { 
            var xss = HtmlRepository.RetriveDataFromStockDocuments("/html/body/div/div/div[2]/main/div/div[1]/section/div/span[1]", stock, 3);
            Double.TryParse(new Regex(@"[^\>]+(?=\<)").Match(xss).ToString(), out double price);
            return price;
        }
        public static double GetPriceChange(Stock stock) {
            var xss = HtmlRepository.RetriveDataFromStockDocuments("/html/body/div/div/div[2]/main/div/div[1]/section/div/span[2]/span", stock, 3);
            var result = new Regex(@"[^\(]+(?=\))").Match(xss).ToString().TrimEnd('%');
            Console.WriteLine("Price change: " + result);
            return Double.Parse(result);
        }
        public static long GetVolume(Stock stock) {
            var xss =  HtmlRepository.RetriveDataFromStockDocuments("/html/body/div/div/div[2]/main/div/div[2]/div[2]/table[2]/tbody/tr[1]/td[2]", stock, 3);
            Int64.TryParse(Regex.Replace(xss, "[^0-9|.]", ""), out long vol);
            return vol;
        }
        private static string RetriveDataFromJsonAnalysis(string ticker, string path, string SepUrl = "")
        {
            string URL = SepUrl == "" ? $"https://stockanalysis.com/stocks/{ticker.ToLower()}/" : SepUrl;
   
            return  HtmlRepository.RetriveDataFromJsonAnalysis(path, URL, DocumentAnalysis, GlobalURL);
        }

       
    }
}
