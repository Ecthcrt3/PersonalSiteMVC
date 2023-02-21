﻿using Microsoft.AspNetCore.Mvc;
using MimeKit;
using PersonalSiteMVC.Models;
using System.Diagnostics;
using MailKit.Net.Smtp;

namespace PersonalSiteMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Resume()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View();
        }

        public IActionResult Links()
        {
            return View();
        }

        [HttpGet]//optional
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel cvm)
        {

            if (!ModelState.IsValid)
            {

                return View(cvm);
            }


            string message = $"You have recieved a new email from your site's contact form!<br>" +
                $"Sender: {cvm.Name}<br>" +
                $"Email: {cvm.Email}<br>" +
                $"Message: {cvm.Message}<br>";
            var mm = new MimeMessage();

            mm.From.Add(new MailboxAddress("Sender", _config.GetValue<string>("Credentials:Email:User")));
            mm.To.Add(new MailboxAddress("Personal", _config.GetValue<string>("Credentials:Email:Recipient")));
            mm.ReplyTo.Add(new MailboxAddress("User", cvm.Email));

            mm.Subject = cvm.Subject;
            mm.Body = new TextPart("HTML") { Text = message };
            mm.Priority = MessagePriority.Urgent;
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_config.GetValue<string>("Credentials:Email:Client"));
                    client.Authenticate(
                        _config.GetValue<string>("Credentials:Email:User"),
                        _config.GetValue<string>("Credentials:Email:Password")
                        );
                    client.Send(mm);
                }

                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"There was an error processing  your request. please" +
                        $"try again later. <br>" +
                        $"Error Message: {ex.Message}";
                    return View(cvm);
                }//end try catch
            }//end using smtp client
            return View("EmailConfirmation", cvm);
        }

    }
}