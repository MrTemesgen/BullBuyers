using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Application
{
    public static class ValidateCompany
    {

        public static string GetTicker(string company)
        {
            https://www.marketwatch.com/tools/quotes/lookup.asp?siteID=mktw&Lookup=tesla&Country=us&Type=All
            
            var html = $@"https://www.marketwatch.com/tools/quotes/lookup.asp?siteID=mktw&Lookup={company.ToLower()}&Country=us&Type=All";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("div[contains(@class, 'results')]/table/tbody/tr/td/a/text()");
            return node.OuterHtml;
        }
    }
}
