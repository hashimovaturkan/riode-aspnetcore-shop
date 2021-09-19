using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.DataContexts;
using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly RiodeDbContext db;
        readonly IConfiguration configuration;
        public HomeController(RiodeDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;

            //SIFRELEME
            //string myKey = "Riode";
            //string plainText = "test";
            ////hashing alg
            //string hashText = plainText.ToMd5();

            ////symmetric alg
            //string chiperText = plainText.Encrypt(myKey);
            //string myPlainText = chiperText.Decrypt(myKey);

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactPost model)
        {
            if (ModelState.IsValid)
            {
                db.ContactPosts.Add(model);

                db.SaveChanges();

                return Json(new { 
                    error=false,
                    message= "Your request has been successfully submitted"

                });
            }
            return Json(new
            {
                error = true,
                message = "Please try again.."

            });
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult FAQ()
        {
            var datas = db.Faqs
                .Where(f => f.DeletedByUserId == null)
                .ToList();
            return View(datas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribe([Bind("Email")]Subscribe model)
        {
            if (ModelState.IsValid)
            {
                db.Database.BeginTransaction();
                var current = db.Subscribes.FirstOrDefault(s => s.Email.Equals(model.Email));
                if (current != null && current.EmailConfirmed == true)
                {
                    return Json(new
                    {
                        error = true,
                        message = "You have already subscribed!"
                    });
                }
                else if (current != null && (current.EmailConfirmed ?? false == false))
                {
                    return Json(new
                    {
                        error = true,
                        message = "You have to check your email, please!"
                    });
                }
                db.Subscribes.Add(model);
                db.SaveChanges();

                string token = $"subscribetoken-{model.Id}-{DateTime.Now:yyyyMMddHHmmss}";

                string path = $"{Request.Scheme}://{Request.Host}/subscribe-confirm?token={token}";

                var mailSend=configuration.SendEmail(model.Email, "Riode NewsLetter Subscribe", $"Please, use <a href={path}>this link</a> for subscribing");

                if (mailSend == false)
                {
                    db.Database.RollbackTransaction();
                    return Json(new
                    {
                        error = true,
                        message = "Please, try again"
                    });
                }
                db.Database.CommitTransaction();
                return Json(new
                {
                    error = false,
                    message = "Please, check your email!"
                });
            }
            
            return Json(new{
                error = true,
                message = "Error! Please try again!"
            });
        }

        [HttpGet]
        [Route("subscribe-confirm")]
        public IActionResult SubscribeConfirm(string token)
        {
            Match match=Regex.Match(token, @"subscribetoken-(?<id>\d+)-(?<executeTimeStamp>\d{14})");
            

            if (match.Success)
            {
                int id = Convert.ToInt32(match.Groups["id"].Value);
                string executeTimeStamp = match.Groups["executeTimeStamp"].Value;

                var subscribe = db.Subscribes.FirstOrDefault(s => s.Id == id && s.DeletedByUserId ==null);

                if (subscribe == null)
                {
                    ViewBag.Message = Tuple.Create(true, "Token Error");
                    goto end;
                }
                if ((subscribe.EmailConfirmed ?? false)==true)
                {
                    ViewBag.Message = Tuple.Create(true, "It was confirmed");
                    goto end;
                }
                subscribe.EmailConfirmed = true;
                subscribe.EmailConfirmedDate = DateTime.Now;
                db.SaveChanges();
                ViewBag.Message = Tuple.Create(false, "You were confirmed!");

            }
            end:
            return View();
        }
    }
}
