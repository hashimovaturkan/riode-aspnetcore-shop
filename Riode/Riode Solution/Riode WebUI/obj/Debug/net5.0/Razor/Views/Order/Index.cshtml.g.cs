#pragma checksum "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\Order\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d9e666a914997091f4a5383cd2d4148e9711d337"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_Index), @"mvc.1.0.view", @"/Views/Order/Index.cshtml")]
namespace AspNetCore
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
#line 4 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\_ViewImports.cshtml"
using Riode_WebUI.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\_ViewImports.cshtml"
using Riode_WebUI.AppCode.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\_ViewImports.cshtml"
using Riode_WebUI.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\_ViewImports.cshtml"
using Resources;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\_ViewImports.cshtml"
using Riode_WebUI.Models.FormModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d9e666a914997091f4a5383cd2d4148e9711d337", @"/Views/Order/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4a20e49e3ac0973d957196a59aea09d5bdb274a", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "order", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "cart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "checkout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "shop", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-icon-left btn-dark btn-back btn-rounded btn-md mb-4"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\Order\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"page-content pt-7 pb-10 mb-10\">\r\n    <div class=\"step-by pr-4 pl-4\">\r\n        <h3 class=\"title title-simple title-step\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d9e666a914997091f4a5383cd2d4148e9711d3376079", async() => {
                WriteLiteral("1. Shopping Cart");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</h3>\r\n        <h3 class=\"title title-simple title-step\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d9e666a914997091f4a5383cd2d4148e9711d3377507", async() => {
                WriteLiteral("2. Checkout");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</h3>\r\n        <h3 class=\"title title-simple title-step active\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d9e666a914997091f4a5383cd2d4148e9711d3378937", async() => {
                WriteLiteral("3. Order Complete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"</h3>
    </div>
    <div class=""container mt-8"">
        <div class=""order-message mr-auto ml-auto"">
            <div class=""icon-box d-inline-flex align-items-center"">
                <div class=""icon-box-icon mb-0"">
                    <svg version=""1.1"" id=""Layer_1"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" x=""0px"" y=""0px"" viewBox=""0 0 50 50"" enable-background=""new 0 0 50 50"" xml:space=""preserve"">
                    <g>
                    <path fill=""none"" stroke-width=""3"" stroke-linecap=""round"" stroke-linejoin=""bevel"" stroke-miterlimit=""10"" d=""
											M33.3,3.9c-2.7-1.1-5.6-1.8-8.7-1.8c-12.3,0-22.4,10-22.4,22.4c0,12.3,10,22.4,22.4,22.4c12.3,0,22.4-10,22.4-22.4
											c0-0.7,0-1.4-0.1-2.1""></path>
                    <polyline fill=""none"" stroke-width=""4"" stroke-linecap=""round"" stroke-linejoin=""bevel"" stroke-miterlimit=""10"" points=""
											48,6.9 24.4,29.8 17.2,22.3 	""></polyline>
									</g>
								</svg>
                </div>
        ");
            WriteLiteral(@"        <div class=""icon-box-content text-left"">
                    <h5 class=""icon-box-title font-weight-bold lh-1 mb-1"">Thank You!</h5>
                    <p class=""lh-1 ls-m"">Your order has been received</p>
                </div>
            </div>
        </div>


        <div class=""order-results"">
            <div class=""overview-item"">
                <span>Order number:</span>
                <strong>4935</strong>
            </div>
            <div class=""overview-item"">
                <span>Status:</span>
                <strong>Processing</strong>
            </div>
            <div class=""overview-item"">
                <span>Date:</span>
                <strong>November 20, 2020</strong>
            </div>
            <div class=""overview-item"">
                <span>Email:</span>
                <strong>12345@gmail.com</strong>
            </div>
            <div class=""overview-item"">
                <span>Total:</span>
                <strong>$312.00</strong>
  ");
            WriteLiteral(@"          </div>
            <div class=""overview-item"">
                <span>Payment method:</span>
                <strong>Cash on delivery</strong>
            </div>
        </div>

        <h2 class=""title title-simple text-left pt-4 font-weight-bold text-uppercase"">Order Details</h2>
        <div class=""order-details"">
            <table class=""order-details-table"">
                <thead>
                    <tr class=""summary-subtotal"">
                        <td>
                            <h3 class=""summary-subtitle"">Product</h3>
                        </td>
                        <td></td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class=""product-name"">
                            Beige knitted shoes <span>
                                <i class=""fas fa-times""></i>
                                1
                            </span>
                        </td>
                   ");
            WriteLiteral(@"     <td class=""product-price"">$84.00</td>
                    </tr>
                    <tr>
                        <td class=""product-name"">
                            Best dark blue pedestrian <span>
                                <i class=""fas fa-times""></i> 1
                            </span>
                        </td>
                        <td class=""product-price"">$76.00</td>
                    </tr>
                    <tr>
                        <td class=""product-name"">
                            Women's fashion handing <span>
                                <i class=""fas fa-times""></i>
                                2
                            </span>
                        </td>
                        <td class=""product-price"">$152.00</td>
                    </tr>
                    <tr class=""summary-subtotal"">
                        <td>
                            <h4 class=""summary-subtitle"">Subtotal:</h4>
                        </td>
              ");
            WriteLiteral(@"          <td class=""summary-subtotal-price"">$312.00</td>
                    </tr>
                    <tr class=""summary-subtotal"">
                        <td>
                            <h4 class=""summary-subtitle"">Shipping:</h4>
                        </td>
                        <td class=""summary-subtotal-price"">Free shipping</td>
                    </tr>
                    <tr class=""summary-subtotal"">
                        <td>
                            <h4 class=""summary-subtitle"">Payment method:</h4>
                        </td>
                        <td class=""summary-subtotal-price"">Cash on delivery</td>
                    </tr>
                    <tr class=""summary-subtotal"">
                        <td>
                            <h4 class=""summary-subtitle"">Total:</h4>
                        </td>
                        <td>
                            <p class=""summary-total-price"">$312.00</p>
                        </td>
                    </tr>
     ");
            WriteLiteral(@"           </tbody>
            </table>
        </div>
        <h2 class=""title title-simple text-left pt-10 mb-2"">Billing Address</h2>
        <div class=""address-info pb-8 mb-6"">
            <p class=""address-detail pb-2"">
                John Doe<br>
                Riode Company<br>
                Steven street<br>
                El Carjon, CA 92020<br>
                123456789
            </p>
            <p class=""email"">mail@riode.com</p>
        </div>

        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d9e666a914997091f4a5383cd2d4148e9711d33716209", async() => {
                WriteLiteral("<i class=\"d-icon-arrow-left\"></i> Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
