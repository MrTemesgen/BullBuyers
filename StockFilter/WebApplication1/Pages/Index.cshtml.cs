using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApplication1.Domain;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<String> Quotes;
        public List<Stock> Stocks;
        public String TableHeading;

        public IndexModel(ILogger<IndexModel> logger)
        {
           
            _logger = logger;
        }

        public void OnGet()
        {
            Stocks = Application.StockListBuilder.GetStocks().Result.OrderByDescending(s => s.PriceChange).ToList();
           
            TableHeading = "Top Earners For " + @DateTime.UtcNow.ToString("M/d/yyyy");
        }
    }
}
