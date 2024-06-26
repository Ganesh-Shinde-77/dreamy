using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System;
using System.IO;
using System.Web;
using WebApplicationn.Database.Dreamy;
using WebApplicationn.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting.Server;

namespace WebApplicationn.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult FeedbackContact(string name, string emailAddress, string message)
        {
            Records record = new Records();
            record.name = name;
            record.emailAddress = emailAddress;
            record.message = message;


            string output = Database.Dreamy.Feedback.AddFeedback(record);


            return View("~/views/home/contactus.cshtml");
        }




    }

}