#pragma checksum "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5baf5f9235c8eeb6fe5c10c047289747004973e5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(WebApplication1.Pages.Pages_Details), @"mvc.1.0.razor-page", @"/Pages/Details.cshtml")]
namespace WebApplication1.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\_ViewImports.cshtml"
using WebApplication1;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "{ticker}")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5baf5f9235c8eeb6fe5c10c047289747004973e5", @"/Pages/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cd66e6c0b2d0593b97c0d67f6f506054866fe040", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Details : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Console.WriteLine(Model.name);
    IDictionary<string, Domain.Stock> dict = Application.StockListBuilder.stocks.ToDictionary(s => s.Ticker);

    Domain.Stock stock;
   dict.TryGetValue(Model.name, out stock);


    

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""justify-content-lg-start"">
    <table class=""table-bordered col-3 col-md-offset-2 table shadow"">
        <thead>
            <tr class=""text-center"">

                <th style=""font-size:larger; color:darkgoldenrod"" colspan=""4"">
                    <div style=""border-color:darkgoldenrod;"">Key Data for ");
#nullable restore
#line 19 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
                                                                     Write(stock.Ticker);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                </th>\r\n\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n            <tr>\r\n                <td><strong>Price</strong></td>\r\n                <td>$");
#nullable restore
#line 27 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
                Write(stock.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <td><strong>Earnings Day</strong></td>\r\n                <td>\r\n");
#nullable restore
#line 32 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
                     if (stock.ReportDay == DateTime.MinValue)
                    {
                        string res = "N/A";
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
                   Write(res);

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
                            ;
                    }
                    else
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
                   Write(stock.ReportDay.ToString("M/d/yyyy"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
                                                             ;
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n\r\n            </tr>\r\n            <tr>\r\n                <td><strong>Price Change</strong></td>\r\n                <td>");
#nullable restore
#line 46 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
               Write(stock.PriceChange);

#line default
#line hidden
#nullable disable
            WriteLiteral("%</td>\r\n            </tr>\r\n            <tr>\r\n                <td><strong>Volume</strong></td>\r\n                <td>");
#nullable restore
#line 50 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
               Write(String.Format("{0:n0}", stock.Volume));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
            </tr>

        </tbody>

    </table>

    <table class=""table-bordered col-3 col-md-offset-2 table shadow"">
        <thead>
            <tr class=""text-center"">

                <th style=""font-size:larger; color:darkgoldenrod"" colspan=""4"">
                    <div style=""border-color:darkgoldenrod;"">Key Data for ");
#nullable restore
#line 62 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
                                                                     Write(stock.Ticker);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                </th>\r\n\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n            <tr>\r\n                <td><strong>CHK</strong></td>\r\n                <td>");
#nullable restore
#line 70 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
               Write(stock.CIK);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 74 "C:\Users\tabre\source\BullBuyers\StockFilter\WebApplication1\Pages\Details.cshtml"
               Write(Data.SEC_Data.SEC_Data.GetInsiderLink(@stock));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n      \r\n        </tbody>\r\n\r\n    </table>\r\n\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication1.Pages.DetailsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApplication1.Pages.DetailsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<WebApplication1.Pages.DetailsModel>)PageContext?.ViewData;
        public WebApplication1.Pages.DetailsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591