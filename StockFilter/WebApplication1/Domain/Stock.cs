using HtmlAgilityPack;
using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Domain
{
    public class Stock
    {
        //Selected Data
        public string Ticker { get; set; }
        public double Price { get; set; }
        public DateTime ReportDay { get; set; }
        public long Volume { get; set; }
        public String CEO { get; set; }
        public Rating AverageRating { get; set; }
        //Derived Data
        public double PriceChange { get; set; }
        public String Name { get; set; }
        public Int64 CIK {get; set;}
        public HtmlDocument StockAnalyzer { get; set; }
        public HtmlDocument SECInsider { get; set; }
        public HtmlDocument SECCik{ get; set; }

        public Stock(string ticker) {

           Ticker = ticker.ToUpper();
        }

        

        

    }
}
