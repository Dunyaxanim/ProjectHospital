#pragma checksum "C:\Users\Dunya - PC\source\repos\Hospital\Hospital\Views\User\ShowRole.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "57c8af9e3d1658a9ec43b35c5d1653b2de091003"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_ShowRole), @"mvc.1.0.view", @"/Views/User/ShowRole.cshtml")]
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
#line 1 "C:\Users\Dunya - PC\source\repos\Hospital\Hospital\Views\_ViewImports.cshtml"
using Hospital.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Dunya - PC\source\repos\Hospital\Hospital\Views\_ViewImports.cshtml"
using Hospital.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Dunya - PC\source\repos\Hospital\Hospital\Views\_ViewImports.cshtml"
using Hospital.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Dunya - PC\source\repos\Hospital\Hospital\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57c8af9e3d1658a9ec43b35c5d1653b2de091003", @"/Views/User/ShowRole.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cf8d7033a1eea668c5f366ff52f15b5f1d9e7ef8", @"/Views/_ViewImports.cshtml")]
    public class Views_User_ShowRole : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<IdentityRole>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Dunya - PC\source\repos\Hospital\Hospital\Views\User\ShowRole.cshtml"
  
    ViewData["Title"] = "ShowRole";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


<div class=""card"">
    <div class=""card-header"">
        <h1 class=""card-title"">Bütün vəzifələr</h1>
    </div>

    <!-- /.card-header -->
    <div class=""card-body"">
        <table id=""example1"" class=""table table-bordered table-striped"">
            <thead>
                <tr>
                    <th>Adı</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 22 "C:\Users\Dunya - PC\source\repos\Hospital\Hospital\Views\User\ShowRole.cshtml"
                 foreach (IdentityRole item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 25 "C:\Users\Dunya - PC\source\repos\Hospital\Hospital\Views\User\ShowRole.cshtml"
                       Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 29 "C:\Users\Dunya - PC\source\repos\Hospital\Hospital\Views\User\ShowRole.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n    <!-- /.card-body -->\r\n</div>\r\n<!-- /.card -->\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script src=""../../plugins/jszip/jszip.min.js""></script>
    <script src=""../../plugins/pdfmake/pdfmake.min.js""></script>
    <script src=""../../plugins/pdfmake/vfs_fonts.js""></script>
    <script>
        $(function () {
            $(""#example1"").DataTable({
                ""responsive"": true, ""lengthChange"": false, ""autoWidth"": false,
                ""buttons"": [""copy"", ""csv"", ""excel"", ""pdf"", ""print"", ""colvis""]
            }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            $('#example2').DataTable({
                ""paging"": true,
                ""lengthChange"": false,
                ""searching"": false,
                ""ordering"": true,
                ""info"": true,
                ""autoWidth"": false,
                ""responsive"": true,
            });
        });
    </script>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<IdentityRole>> Html { get; private set; }
    }
}
#pragma warning restore 1591
