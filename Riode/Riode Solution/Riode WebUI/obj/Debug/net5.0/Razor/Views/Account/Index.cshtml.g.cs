#pragma checksum "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\Account\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "23214c825615dff464f61524c101a978bb14a434"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Index), @"mvc.1.0.view", @"/Views/Account/Index.cshtml")]
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
using Riode_WebUI.AppCode.Extentions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\_ViewImports.cshtml"
using Riode_WebUI.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23214c825615dff464f61524c101a978bb14a434", @"/Views/Account/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"abdbe44a2c578b4d41664c23ce7c5de0d9fedd3c", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("#"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Views\Account\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<nav class=""breadcrumb-nav"">
    <div class=""container"">
        <ul class=""breadcrumb"">
            <li><a href=""demo1.html""><i class=""d-icon-home""></i></a></li>
            <li>Account</li>
        </ul>
    </div>
</nav>
<div class=""page-content mt-4 mb-10 pb-6"">
    <div class=""container"">
        <h2 class=""title title-center mb-10"">My Account</h2>
        <div class=""tab tab-vertical gutter-lg"">
            <ul class=""nav nav-tabs mb-4 col-lg-3 col-md-4"" role=""tablist"">
                <li class=""nav-item"">
                    <a class=""nav-link active"" href=""#dashboard"">Dashboard</a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link"" href=""#orders"">Orders</a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link"" href=""#downloads"">Downloads</a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link"" href=""#address"">Address</a>
              ");
            WriteLiteral(@"  </li>
                <li class=""nav-item"">
                    <a class=""nav-link"" href=""#account"">Account details</a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link"" href=""login.html"">Logout</a>
                </li>
            </ul>
            <div class=""tab-content col-lg-9 col-md-8"">
                <div class=""tab-pane active"" id=""dashboard"">
                    <p class=""mb-0"">
                        Hello <span>User</span> (not <span>User</span>? <a href=""#""
                                                                           class=""text-primary"">Log out</a>)
                    </p>
                    <p class=""mb-8"">
                        From your account dashboard you can view your <a href=""#orders""
                                                                         class=""link-to-tab text-primary"">recent orders</a>, manage your shipping and billing
                        addresses,<br>and edit your password ");
            WriteLiteral(@"and account details</a>.
                    </p>
                    <a href=""shop.html"" class=""btn btn-dark btn-rounded"">Go To Shop<i class=""d-icon-arrow-right""></i></a>
                </div>
                <div class=""tab-pane"" id=""orders"">
                    <table class=""order-table"">
                        <thead>
                            <tr>
                                <th class=""pl-2"">Order</th>
                                <th>Date</th>
                                <th>Status</th>
                                <th>Total</th>
                                <th class=""pr-2"">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class=""order-number""><a href=""#"">#3596</a></td>
                                <td class=""order-date""><time>February 24, 2021</time></td>
                                <td class=""order-status""><span>On hold</span");
            WriteLiteral(@"></td>
                                <td class=""order-total""><span>$900.00 for 5 items</span></td>
                                <td class=""order-action""><a href=""#"" class=""btn btn-primary btn-link btn-underline"">View</a></td>
                            </tr>
                            <tr>
                                <td class=""order-number""><a href=""#"">#3593</a></td>
                                <td class=""order-date""><time>February 21, 2021</time></td>
                                <td class=""order-status""><span>On hold</span></td>
                                <td class=""order-total""><span>$290.00 for 2 items</span></td>
                                <td class=""order-action""><a href=""#"" class=""btn btn-primary btn-link btn-underline"">View</a></td>
                            </tr>
                            <tr>
                                <td class=""order-number""><a href=""#"">#2547</a></td>
                                <td class=""order-date""><time>January 4, 2021</ti");
            WriteLiteral(@"me></td>
                                <td class=""order-status""><span>On hold</span></td>
                                <td class=""order-total""><span>$480.00 for 8 items</span></td>
                                <td class=""order-action""><a href=""#"" class=""btn btn-primary btn-link btn-underline"">View</a></td>
                            </tr>
                            <tr>
                                <td class=""order-number""><a href=""#"">#2549</a></td>
                                <td class=""order-date""><time>January 19, 2021</time></td>
                                <td class=""order-status""><span>On hold</span></td>
                                <td class=""order-total""><span>$680.00 for 5 items</span></td>
                                <td class=""order-action""><a href=""#"" class=""btn btn-primary btn-link btn-underline"">View</a></td>
                            </tr>
                            <tr>
                                <td class=""order-number""><a href=""#"">#4523</a></");
            WriteLiteral(@"td>
                                <td class=""order-date""><time>Jun 6, 2021</time></td>
                                <td class=""order-status""><span>On hold</span></td>
                                <td class=""order-total""><span>$564.00 for 3 items</span></td>
                                <td class=""order-action""><a href=""#"" class=""btn btn-primary btn-link btn-underline"">View</a></td>
                            </tr>
                            <tr>
                                <td class=""order-number""><a href=""#"">#4526</a></td>
                                <td class=""order-date""><time>Jun 19, 2021</time></td>
                                <td class=""order-status""><span>On hold</span></td>
                                <td class=""order-total""><span>$123.00 for 8 items</span></td>
                                <td class=""order-action""><a href=""#"" class=""btn btn-primary btn-link btn-underline"">View</a></td>
                            </tr>
                        </tbody>
   ");
            WriteLiteral(@"                 </table>
                </div>
                <div class=""tab-pane"" id=""downloads"">
                    <p class=""mb-4 text-body"">No downloads available yet.</p>
                    <a href=""#"" class=""btn btn-primary btn-link btn-underline"">Browser Products<i class=""d-icon-arrow-right""></i></a>
                </div>
                <div class=""tab-pane"" id=""address"">
                    <p class=""mb-2"">
                        The following addresses will be used on the checkout page by default.
                    </p>
                    <div class=""row"">
                        <div class=""col-sm-6 mb-4"">
                            <div class=""card card-address"">
                                <div class=""card-body"">
                                    <h5 class=""card-title text-uppercase"">Billing Address</h5>
                                    <p>
                                        John Doe<br>
                                        Riode Company<br>
        ");
            WriteLiteral(@"                                Steven street<br>
                                        El Carjon, CA 92020
                                    </p>
                                    <a href=""#"" class=""btn btn-link btn-secondary btn-underline"">
                                        Edit <i class=""far fa-edit""></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class=""col-sm-6 mb-4"">
                            <div class=""card card-address"">
                                <div class=""card-body"">
                                    <h5 class=""card-title text-uppercase"">Shipping Address</h5>
                                    <p>You have not set up this type of address yet.</p>
                                    <a href=""#"" class=""btn btn-link btn-secondary btn-underline"">
                                        Edit <i class=""far fa-edit""></i>
                ");
            WriteLiteral(@"                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""tab-pane"" id=""account"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "23214c825615dff464f61524c101a978bb14a43413408", async() => {
                WriteLiteral("\r\n                        <div class=\"row\">\r\n                            <div class=\"col-sm-6\">\r\n                                <label>First Name *</label>\r\n                                <input type=\"text\" class=\"form-control\" name=\"first_name\"");
                BeginWriteAttribute("required", " required=\"", 8771, "\"", 8782, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            </div>\r\n                            <div class=\"col-sm-6\">\r\n                                <label>Last Name *</label>\r\n                                <input type=\"text\" class=\"form-control\" name=\"last_name\"");
                BeginWriteAttribute("required", " required=\"", 9022, "\"", 9033, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            </div>\r\n                        </div>\r\n\r\n                        <label>Display Name *</label>\r\n                        <input type=\"text\" class=\"form-control mb-0\" name=\"display_name\"");
                BeginWriteAttribute("required", " required=\"", 9250, "\"", 9261, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                        <small class=""d-block form-text mb-7"">
                            This will be how your name will be displayed
                            in the account section and in reviews
                        </small>

                        <label>Email Address *</label>
                        <input type=""email"" class=""form-control"" name=""email""");
                BeginWriteAttribute("required", " required=\"", 9639, "\"", 9650, 0);
                EndWriteAttribute();
                WriteLiteral(@">
                        <fieldset>
                            <legend>Password Change</legend>
                            <label>Current password (leave blank to leave unchanged)</label>
                            <input type=""password"" class=""form-control"" name=""current_password"">

                            <label>New password (leave blank to leave unchanged)</label>
                            <input type=""password"" class=""form-control"" name=""new_password"">

                            <label>Confirm new password</label>
                            <input type=""password"" class=""form-control"" name=""confirm_password"">
                        </fieldset>

                        <button type=""submit"" class=""btn btn-primary"">SAVE CHANGES</button>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("addjs", async() => {
                WriteLiteral("\r\n\r\n<script>\r\n    const main = document.querySelector(\".main\");\r\n    main.classList.add(\"account\");\r\n</script>\r\n\r\n");
            }
            );
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
