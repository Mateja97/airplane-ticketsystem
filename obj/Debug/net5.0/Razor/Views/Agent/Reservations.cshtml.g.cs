#pragma checksum "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "88fcfb666877cebbeb9c8c9c6afcdef640f3e3fb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Agent_Reservations), @"mvc.1.0.view", @"/Views/Agent/Reservations.cshtml")]
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
#line 1 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/_ViewImports.cshtml"
using airplane_ticketsystem;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/_ViewImports.cshtml"
using airplane_ticketsystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"88fcfb666877cebbeb9c8c9c6afcdef640f3e3fb", @"/Views/Agent/Reservations.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40604f972a6fae995c15fcafca7d83000206d304", @"/Views/_ViewImports.cshtml")]
    public class Views_Agent_Reservations : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<airplane_ticketsystem.Models.ReservationListModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
  
    ViewBag.Title = "Info";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
#nullable restore
#line 7 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
 foreach (ReservationModel reservation in Model.ReservationList)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\n        Username: ");
#nullable restore
#line 10 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
             Write(reservation.username);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        Flight: ");
#nullable restore
#line 11 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
           Write(reservation.flight.startDestination);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 11 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
                                                  Write(reservation.flight.endDestination);

#line default
#line hidden
#nullable disable
            WriteLiteral(" on: ");
#nullable restore
#line 11 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
                                                                                         Write(reservation.flight.date.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\n");
#nullable restore
#line 12 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
         if(reservation.accepted == true){

#line default
#line hidden
#nullable disable
            WriteLiteral("            <span color=\"gray\">Accepted </span>\n");
#nullable restore
#line 14 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
        }else{
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
       Write(Html.ActionLink("Accept","AcceptReservation","Agent", @reservation.reservationId));

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
                                                                                              
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\n    <br><br>\n");
#nullable restore
#line 19 "/home/mateja/Desktop/get/airplane-ticketsystem/Views/Agent/Reservations.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<airplane_ticketsystem.Models.ReservationListModel> Html { get; private set; }
    }
}
#pragma warning restore 1591