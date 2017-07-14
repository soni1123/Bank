
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.AlertMessages
{
    public class Notification
    {
        public Notification()
        {

        }
        public string messageinfo(string message, string title = "")
        {
            string sb = " <div class=\"example-box-wrapper\">" +
            "  <div class=\"alert alert-close alert-info\" style='position:fixed; right:0px; top:0px; z-index:999999;width: 100%;'>" +
            "      <a href=\"#\" title=\"Close\" class=\"glyph-icon alert-close-btn icon-remove\"></a>" +
            "      <div class=\"bg-black alert-icon\">" +
            "          <i class=\"glyph-icon icon-comment\"></i>" +
            "      </div>" +
            "      <div class=\"alert-content\">";

            if (title != "") { sb += "<h4 class=\"alert-title\">" + title + "</h4>"; } else { sb += "<h4 class=\"alert-title\">" + " " + "</h4>"; }
            sb += "<p>" + message + "</p>" +
            "        </div>" +
            "    </div>" +
            "</div>";


            return sb;
        }

        public string messagewarning(string message, string title = "")
        {

            string sb = " <div class=\"example-box-wrapper\">" +
             "<div class=\"alert alert-close alert-warning\" style='position:fixed; right:0px; top:0px; z-index:999999;width: 100%;'>" +
             "<a href=\"#\" title=\"Close\" class=\"glyph-icon alert-close-btn icon-remove\"></a>" +
             "<div class=\"bg-orange  alert-icon\">" +
             "    <i class=\"glyph-icon icon-warning\"></i>" +
             "</div>" +
             "<div class=\"alert-content\">";

            if (title != "") { sb += "<h4 class=\"alert-title\">" + title + "</h4>"; } else { sb += "<h4 class=\"alert-title\">" + " " + "</h4>"; }
            sb += "<p>" + message + "</p>" +
            "</div>" +
            "</div>" +
            "</div>";

            return sb;
        }

        public string messageerror(string message, string title = "")
        {
            string sb = " <div id =\"toast-container\" class=\"toast-top-full-width toast-successMSG\" aria-live=\"polite\" role=\"alert\">" +
             "  <div class=\"toast toast-success\">" +

           " <div class=\"toast-title\">" + message + "</div>";
            sb += "</div ></ div >";
            return sb;
        }

        public string messagesuccess(string message, string title = "")
        {
            string sb = " <div id =\"toast-container\" class=\"toast-top-full-width toast-successMSG\" aria-live=\"polite\" role=\"alert\">" +
             "  <div class=\"toast toast-success\">" +

           " <div class=\"toast-title\">"+message+"</div>";
            sb += "</div ></ div >";
            return sb;
        }
        public string progressbar(string percent, string Message)
        {
            string strmessage = "";

            strmessage += "<div style='text-align:center'>" + Message + " " + percent + "% Completed</div>";
            strmessage += "<div class='progress progress-striped active progress-xs'>";
            strmessage += "<div class='progress-bar' role='progressbar' aria-valuenow='" + percent + "' aria-valuemin='0' aria-valuemax='100' style='width: " + percent + "%'>";
            strmessage += "<span class='sr-only'> " + percent + "% Complete</span>";
            strmessage += "</div>";
            strmessage += "</div>";

            return strmessage;
        }

    }
}