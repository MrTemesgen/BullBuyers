using HtmlAgilityPack;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplication1.Domain;

namespace WebApplication1.Data.SEC_Data
{
    public  static class SEC_Data
    {
        public static HtmlDocument DocumentAnalysis = new HtmlDocument();
 
        public static string GlobalURL;
       

        public static string GetInsiderLink(Stock stock)
        {
            try
            {
                return RetriveDataFromJsonAnalysis(stock.Ticker, "//div[contains(@class, 'organization-name')]/text()", $"https://sec.report/CIK/{stock.CIK}/Insider-Trades");
            } catch (Exception e)
            {
                Debug.WriteLine(e);
                return "N/A";
            }
        } 

        private static string RetriveDataFromJsonAnalysis(string ticker, string path, string SepUrl = "")
        {
            string URL = SepUrl == "" ? $"https://sec.report/Ticker/{ticker.ToUpper()}/" : SepUrl;

            return  HtmlRepository.RetriveDataFromJsonAnalysis(path, URL, DocumentAnalysis, GlobalURL);
        }
        

    }
}


