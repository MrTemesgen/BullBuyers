﻿using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Win32.SafeHandles;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Domain;


namespace WebApplication1.Application
{       
    public class StockListBuilder
    {
        public static List<Stock> stocks;
        public static string[] TickersFile = System.IO.File.ReadAllLines(".\\Application\\top-100-stocks-to-buy-07-27-2020.csv");
        public static DateTime prev = DateTime.UtcNow;
        public static async Task<List<Stock>> GetStocks()
        {
            if(stocks != null)
            {
                return stocks;  
            }
            
            var tickers = TickersFile.ToList<string>();
            var empty_stocks = new List<Stock>();
            foreach (string ticker in tickers) 
            {
                empty_stocks.Add(new Stock(ticker));
            }

            List<Task<Stock>> tasks = new List<Task<Stock>>();
            stocks = new List<Stock>();
            foreach (Stock stock in empty_stocks)
            {
                
                tasks.Add(Task.Run(() => HtmlRepository.SetUpDocuments(stock)));
                
            }


            var data  = await Task.WhenAll(tasks);


            foreach (var d in data) {
                stocks.Add(d);
            }


            foreach (Stock stock in stocks)
            {
                GetStock(stock);
            }

            return stocks;
        }



        private static Rating GetAverageRating(Stock stock)
        {
            
           
            if (stock.PriceChange < -5)
            {
                return Rating.Strong_Buy;
            }
            else if(stock.PriceChange < -3)
            {
                return Rating.Buy;
            }
            else if (stock.PriceChange > 3 && stock.PriceChange < 5)
            {
                return Rating.Sell;
            }
            else if(stock.PriceChange > 5)
            {
                return Rating.Strong_Sell;
            }
            return Rating.Hold;

        }

        public static Stock GetStock(Stock stock)
        {

            
            try {

                Stopwatch watch = new Stopwatch();

                watch.Start();
                stock.Price = Stock_Data.GetPrice(stock);
                stock.ReportDay = Stock_Data.GetEarningsDay(stock);
                stock.Volume = Stock_Data.GetVolume(stock);
                //stock.Risk = Stock_Data.GetATR(ticker);
                stock.PriceChange = Stock_Data.GetPriceChange(stock);
                stock.AverageRating = GetAverageRating(stock);
                stock.CEO = Stock_Data.GetCEO(stock);
                stock.Name = Stock_Data.GetName(stock);
                watch.Stop();
                Console.WriteLine(stock.Ticker + " traded at: " + stock.Price + " Estimated earnings date is: " + stock.ReportDay);
                Console.WriteLine("Total Time (ms): " + watch.ElapsedMilliseconds);

            }
            catch(Exception e)
            {
                Console.WriteLine(e + " " + stock.Ticker);
                return null;
            }
            return stock;

        }

        public static async Task<Stock> GetStock(string Ticker) {
            Stock stock = new Stock(Ticker);
            Stopwatch watch = new Stopwatch();

            watch.Start();
            await Stock_Data.GetDoc(stock);
            watch.Stop();
            Console.WriteLine("Document Loading part:" + watch.ElapsedMilliseconds);

            stock = GetStock(stock);
            return stock;
        
        }

       


    }
}
