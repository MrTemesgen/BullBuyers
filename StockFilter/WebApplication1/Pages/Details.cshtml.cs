using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Application;

namespace WebApplication1.Pages
{
    public class DetailsModel : PageModel {
        public Stock stock;
        public string name = "None Found";
        public void OnGet(string Ticker)
        {
            name = Ticker;
                     
        }
    }
}