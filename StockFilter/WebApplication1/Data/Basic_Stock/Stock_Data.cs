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
        public static string GetEarningsDay(string ticker) {return  RetriveDataFromJsonAnalysis(ticker, "/html/body/div/div[2]/main/div[2]/div[1]/div[2]/table/tbody/tr[1]/td[2]", $"https://stockanalysis.com/stocks/{ticker.ToLower()}/statistics/");}
        public static string GetPrice(Stock stock) { return HtmlRepository.RetriveDataFromStockDocuments("/html/body/div/div[2]/main/div[1]/section/div/span[1]", stock, 3); }
        public static double GetPriceChange(Stock stock) {return Double.Parse(HtmlRepository.RetriveDataFromStockDocuments("//div[contains(@class, 'quote')]/table/tbody/tr[5]/td[2]/text()", stock, 3).TrimEnd('%')); }
        public static string GetVolume(Stock stock) { return HtmlRepository.RetriveDataFromStockDocuments("//div[contains(@class, 'quote')]/table/tbody/tr[8]/td[2]/text()", stock, 3); }
        private static string RetriveDataFromJsonAnalysis(string ticker, string path, string SepUrl = "")
        {
            string URL = SepUrl == "" ? $"https://stockanalysis.com/stocks/{ticker.ToLower()}/" : SepUrl;
   
            return  HtmlRepository.RetriveDataFromJsonAnalysis(path, URL, DocumentAnalysis, GlobalURL);
        }

       
    }
}
