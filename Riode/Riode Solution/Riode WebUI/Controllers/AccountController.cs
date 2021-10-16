using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Riode.Application.Core.Extensions;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities.Membership;
using Riode.Domain.Models.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        readonly UserManager<RiodeUser> userManager;
        readonly SignInManager<RiodeUser> signInManager;
        readonly RoleManager<RiodeRole> roleManager;
        readonly IConfiguration configuration;
        readonly RiodeDbContext db;
        public AccountController(UserManager<RiodeUser> userManager,
                                 SignInManager<RiodeUser> signInManager,
                                 RoleManager<RiodeRole> roleManager,
                                 IConfiguration configuration,
                                 RiodeDbContext db)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.db = db;
        }

        [Authorize(Policy = "account.index")]
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Policy = "account.signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        
        [HttpPost]
        //[Authorize(Policy = "account.signin")]
        public async Task<IActionResult> SignIn(LoginFormModel user)
        {
            if (ModelState.IsValid)
            {
                RiodeUser foundedUser = null;
                if (user.UserName.IsEmail() == true)
                {
                    foundedUser = await userManager.FindByEmailAsync(user.UserName);
                }
                else
                {
                    foundedUser = await userManager.FindByNameAsync(user.UserName);
                }

                if (foundedUser == null || !await userManager.IsInRoleAsync(foundedUser, "User"))
                {
                    ViewBag.Message = "Your username or password is incorrect!";
                    return View(user);
                }

                //if(!await userManager.IsInRoleAsync(foundedUser, "User"))
                //{
                //    ViewBag.Message = "Your username or password is incorrect!";
                //    return View(user);
                //}

                if (!await userManager.IsEmailConfirmedAsync(foundedUser))
                {
                    ViewBag.Message = "Please,confirm your email!";
                    return View(user);
                }

                var sResult = await signInManager.PasswordSignInAsync(foundedUser, user.Password, true, true);
                
                if (sResult.Succeeded != true)
                {
                    ViewBag.Message = "Your username or password is incorrect!";
                    return View(user);
                }

                var redirectUrl = Request.Query["ReturnUrl"];
                if (!string.IsNullOrWhiteSpace(redirectUrl))
                {
                    return Redirect(redirectUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Your username or password is incorrect!";
            return View();
        }
        //[Authorize(Policy = "account.register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel user)
        {
            if (ModelState.IsValid)
            {
                db.Database.BeginTransaction();
                var username = await userManager.FindByNameAsync(user.UserName);
                var email = await userManager.FindByEmailAsync(user.Email);
                if(username != null)
                    return Json(new
                    {
                        error = true,
                        message = "Your username is already used!"
                    });

                if (email != null)
                    return Json(new
                    {
                        error = true,
                        message = "Your email is already registered!"
                    });

                RiodeRole riodeRole = new RiodeRole
                {
                    Name="User"
                };

                if(!roleManager.RoleExistsAsync(riodeRole.Name).Result)
                {
                    var createRole = roleManager.CreateAsync(riodeRole).Result;
                    if(!createRole.Succeeded)
                    {
                        return Json(new
                        {
                            error = true,
                            message = "Error, please try again!"
                        });
                    }
                }
                else
                {
                    //todo
                    //var role = roleManager.FindByNameAsync(riodeRole.Name).Result;

                }

                string password = user.Password;
                var riodeUser = new RiodeUser()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = false
                };
                //password 3den yxuari olmaliid
                var createdUser = userManager.CreateAsync(riodeUser, password).Result;

                if (createdUser.Succeeded)
                {
                    userManager.AddToRoleAsync(riodeUser, riodeRole.Name).Wait();

                    //string token = $"emailconfirmtoken-{riodeUser.Id}-{DateTime.Now:yyyyMMddHHmmss}";
                    //token = token.Encrypt();
                    string token = userManager.GenerateEmailConfirmationTokenAsync(riodeUser).Result;
                    string path = $"{Request.Scheme}://{Request.Host}/email-confirm?email={riodeUser.Email}&token={token}";
                    var sendMail = configuration.SendEmail(user.Email, "Riode email confirming", $"Please, use <a href={path}>this link</a> for confirming");

                    if (sendMail == false)
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
                        message = "Successfully, please check your email!"
                    });
                }
                else
                {
                    return Json(new
                    {
                        error = true,
                        message = "Error, please try again!"
                    });
                }

            }

            return Json(new { 
                error=true,
                message="Incomplete data"
            });
        }

        [Route("email-confirm")]
        [Authorize(Policy = "account.emailconfirm")]
        public async Task<IActionResult> EmailConfirm(string email, string token)
        {
            var user = userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                ViewBag.Message = "Token error!";
                return View();
            }

            if (user.EmailConfirmed == true)
            {
                ViewBag.Message = "You have already confirmed.";
                return View();
            }

            IdentityResult result = await userManager.
                        ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.Message = "We're excited to have you get started. Your account is confirmed successfully.";
                return View();
            }
            else
            {
                ViewBag.Message = "Error while confirming your email!";
                return View();
            }

            //    token = token.Decrypt();

            //    Match match =Regex.Match(token, @"emailconfirmtoken-(?<id>\d+)-(?<executeTimeStamp>\d{14})");

            //    if (match.Success)
            //    {
            //        long id = Convert.ToInt64(match.Groups["id"].Value);
            //        string executeTimeStamp = match.Groups["executeTimeStamp"].Value;

            //        var user = db.Users.FirstOrDefault(u => u.Id == id);

            //        if (user == null)
            //        {
            //            ViewBag.Message = "Token error!";
            //            goto end;
            //        }
            //        if (user.EmailConfirmed == true)
            //        {
            //            ViewBag.Message = "You have already confirmed.";
            //            goto end;
            //        }
            //        user.EmailConfirmed = true;
            //        db.SaveChanges();
            //        ViewBag.Message = "We're excited to have you get started. Your account is confirmed successfully.";

            //    }
            //    else
            //    {
            //        ViewBag.Message = "Wrong Application!";
            //    }
            //end:
            //    return View();
        }

        [Authorize(Policy = "account.signout")]
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        [Authorize(Policy = "account.wishlist")]
        public IActionResult Wishlist()
        {
            return View();
        }
    }
}
