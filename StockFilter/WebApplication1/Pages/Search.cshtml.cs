using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceStack;
using WebApplication1.Data;
using WebApplication1.Domain;

namespace WebApplication1.Pages
{
    public class SearchModel : PageModel
    {
        public List<Stock> Stocks;
        public Stock selectedStock;
        public bool tickerFound = true;
        private static Boolean symbol;
        private static Boolean price;
        private static Boolean run;
        private static Boolean rating;
        private static Boolean volume;
        private static Boolean report;
        
       

        public  SearchModel()
        {
            
            Stocks = Application.StockListBuilder.GetStocks().Result;

        }
        public void OnGet()
        {
            ViewData["Heading"] = "Stock data for: " + DateTime.UtcNow.ToString("M/d/yyyy");
        }

        public void OnPostSearch(string ticker)
        {
            OnGet();
            try
            {
                Predicate<Stock> p = s => s.Ticker.EqualsIgnoreCase(ticker);
                if (!Stocks.Exists(p))
                {
                    Stock stock = Application.StockListBuilder.GetStock(ticker).Result;
                    if (stock != null)
                    {
                        Stocks.Insert(0, stock);
                        tickerFound = true;
                    }
                    else {

                        tickerFound = false;
                    }
                }
                else
                {
                    Stock stock = Stocks.Find(p);
                    Stocks.RemoveAll(p);
                    Stocks.Insert(0, stock);
                }
            }
            catch (Exception) { }
        }

        public IActionResult OnPostDetail(String stock)
        {
            return new RedirectToPageResult("/Details", Application.StockListBuilder.GetStock(stock).Result.Ticker);
            
        }

        public void OnPostSort(string sortOrder)
        {
            OnGet();
            
            
            switch (sortOrder)
            {
                case "symbol":
                    Flip(ref symbol);
                    Stocks = symbol ? Stocks = Stocks.OrderByDescending(s => s.Ticker).ToList() : Stocks = Stocks.OrderBy(s => s.Ticker).ToList(); 
                    break;
                case "Price":
                    Flip(ref price);
                    Stocks = price ? Stocks = Stocks.OrderByDescending(s => s.Price).ToList() : Stocks = Stocks.OrderBy(s => s.Price).ToList();
                    break;
                case "run":
                    Flip(ref run);
                    Stocks = run ? Stocks = Stocks.OrderByDescending(s => s.PriceChange).ToList() : Stocks = Stocks.OrderBy(s => s.PriceChange).ToList();
                    break;
                case "rating":
                    Flip(ref rating);
                    Stocks = rating ? Stocks = Stocks.OrderByDescending(s => s.AverageRating).ToList() : Stocks = Stocks.OrderBy(s => s.AverageRating).ToList();
                    break;
                case "volume":
                    Flip(ref volume);
                    Stocks = volume ? Stocks = Stocks.OrderByDescending(s => s.Volume).ToList() : Stocks = Stocks.OrderBy(s => s.Volume).ToList();
                    break;
                case "report":
                    Flip(ref report);
                    Stocks = report ? Stocks = Stocks.OrderByDescending(s => s.ReportDay).ToList() : Stocks = Stocks.OrderBy(s => s.ReportDay).ToList();
                    break;
                default:
                    break;
            }
            OnGet();
            
        }

        public void Flip(ref Boolean value) { value = value ? false : true; }



       
    }
}