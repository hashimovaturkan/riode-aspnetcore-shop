#pragma checksum "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a60f9f52e54b525f5c11eab8c9128e08ccc5e957"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Products_Details), @"mvc.1.0.view", @"/Areas/Admin/Views/Products/Details.cshtml")]
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
#line 4 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\_ViewImports.cshtml"
using Riode_WebUI.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\_ViewImports.cshtml"
using Riode_WebUI.AppCode.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\_ViewImports.cshtml"
using Riode_WebUI.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\_ViewImports.cshtml"
using Resources;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\_ViewImports.cshtml"
using Riode_WebUI.AppCode.Application.BrandsModule;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a60f9f52e54b525f5c11eab8c9128e08ccc5e957", @"/Areas/Admin/Views/Products/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"909d733fc9ab81fe6ffe4a71ab0f28fe435f80dd", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Products_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Product>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("70"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("70"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("mr-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "BlogPosts", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("edit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""container-xl"">
    <div class=""table-responsive"">
        <div class=""table-wrapper"">
            <div class=""table-title"">
                <div class=""row"">
                    <div class=""col-sm-12"">
                        <h2 class=""text-center""><b>Product</b></h2>
                    </div>
                </div>
            </div>
        </div>
        <table class=""table table-striped table-hover"">

            <tbody>
                <tr>
                    <th>Images</th>
                    <td>
");
#nullable restore
#line 25 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                         foreach (var item in Model.Images)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <span class=\"image-box\">\r\n");
#nullable restore
#line 28 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                                 if (item.IsMain == true)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"image-desc\">It is main image</span>\r\n");
#nullable restore
#line 31 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a60f9f52e54b525f5c11eab8c9128e08ccc5e9578292", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1006, "~/uploads/images/product/", 1006, 25, true);
#nullable restore
#line 32 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
AddHtmlAttributeValue("", 1031, item.FileName, 1031, 14, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </span>\r\n");
#nullable restore
#line 34 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Name</th>\r\n                    <td>");
#nullable restore
#line 39 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                   Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Sku Code</th>\r\n                    <td>");
#nullable restore
#line 43 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                   Write(Model.SkuCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Short Description</th>\r\n                    <td class=\"blog-content\">");
#nullable restore
#line 47 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                                        Write(Model.ShortDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th scope=\"row\">Description</th>\r\n                    <td class=\"blog-content\">");
#nullable restore
#line 51 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                                        Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th scope=\"row\">Category</th>\r\n                    <td>");
#nullable restore
#line 55 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                   Write(ViewData["CategoryId"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th scope=\"row\">Brand</th>\r\n                    <td>");
#nullable restore
#line 59 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                   Write(ViewData["BrandId"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Created User</th>\r\n                    <td>");
#nullable restore
#line 63 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                   Write(Model.CreatedByUserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Created Date</th>\r\n                    <td>");
#nullable restore
#line 67 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                   Write(Model.CreatedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Deleted User</th>\r\n                    <td>");
#nullable restore
#line 71 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                   Write(Model.DeletedByUserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Deleted Date</th>\r\n                    <td>");
#nullable restore
#line 75 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                   Write(Model.DeletedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n            </tbody>\r\n        </table>\r\n        <div>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a60f9f52e54b525f5c11eab8c9128e08ccc5e95714603", async() => {
                WriteLiteral("<i class=\"fa fa-edit\" data-toggle=\"tooltip\" title=\"Edit\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 80 "E:\Lesson folder\riode-aspnetcore-shop\Riode\Riode Solution\Riode WebUI\Areas\Admin\Views\Products\Details.cshtml"
                                   WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            |\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a60f9f52e54b525f5c11eab8c9128e08ccc5e95717163", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n\r\n");
            DefineSection("addcss", async() => {
                WriteLiteral(@"
    <style>
        .image-box{
            display:inline-block;
            position:relative;
        }
        .image-desc {
            position:absolute;
            top:-10px;
            left:0;
            font-size:12px;
        }
    </style>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Product> Html { get; private set; }
    }
}
#pragma warning restore 1591
