#pragma checksum "C:\repos\PhoneBookWithAPI\PhoneBook\Views\Logout\LogoutUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bced6a831086cda04547aa63f663d11b74cdde9b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Logout_LogoutUser), @"mvc.1.0.view", @"/Views/Logout/LogoutUser.cshtml")]
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
#line 2 "C:\repos\PhoneBookWithAPI\PhoneBook\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\repos\PhoneBookWithAPI\PhoneBook\Views\_ViewImports.cshtml"
using PhoneBook.Domain;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\repos\PhoneBookWithAPI\PhoneBook\Views\_ViewImports.cshtml"
using PhoneBook.Domain.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\repos\PhoneBookWithAPI\PhoneBook\Views\_ViewImports.cshtml"
using PhoneBook.Views.Login;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\repos\PhoneBookWithAPI\PhoneBook\Views\_ViewImports.cshtml"
using PhoneBook.Views.Register;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\repos\PhoneBookWithAPI\PhoneBook\Views\_ViewImports.cshtml"
using PhoneBook.Views.Logout;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\repos\PhoneBookWithAPI\PhoneBook\Views\_ViewImports.cshtml"
using PhoneBook.Views.Roles;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bced6a831086cda04547aa63f663d11b74cdde9b", @"/Views/Logout/LogoutUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"338754819e6bcb346728f25cef57067a3c7c6759", @"/Views/_ViewImports.cshtml")]
    public class Views_Logout_LogoutUser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\repos\PhoneBookWithAPI\PhoneBook\Views\Logout\LogoutUser.cshtml"
  
    ViewData["Title"] = "Log out";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<header>\r\n    <h1>");
#nullable restore
#line 6 "C:\repos\PhoneBookWithAPI\PhoneBook\Views\Logout\LogoutUser.cshtml"
   Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    <p>You have successfully logged out of the application.</p>\r\n</header>\r\n");
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
