using WebApplication1.Domain;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace WebApplication1.Pages
{
    public class DetailsModel : PageModel {
        public Stock stock;
        public string name = "None Found";
        public void OnGet(string Ticker)
        {
            
            name = Ticker;        
        }

        public  JsonResult OnGetWeeklyData(string Ticker) {
            JObject data = Data.HtmlRepository.GetStockChartWeekly(Ticker);
            
            return new JsonResult(data.ToString());
        
        }



    }
}