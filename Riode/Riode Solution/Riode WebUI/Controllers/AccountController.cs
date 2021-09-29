using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Riode_WebUI.AppCode.Extensions;
using Riode_WebUI.Models.Entities.Membership;
using Riode_WebUI.Models.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        readonly UserManager<RiodeUser> userManager;
        readonly SignInManager<RiodeUser> signInManager;
        public AccountController(UserManager<RiodeUser> userManager,
                                 SignInManager<RiodeUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [Authorize(Policy = "ui.account.index")]
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Policy = "ui.account.signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        
        [HttpPost]
        //[Authorize(Policy = "ui.account.signin")]
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

                if (foundedUser == null)
                {
                    ViewBag.Message = "Your username or password is incorrect!";
                    return View(user);
                }

                if(!await userManager.IsInRoleAsync(foundedUser, "User"))
                {
                    ViewBag.Message = "Your username or password is incorrect!";
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

                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Message = "Your username or password is incorrect!";
            return View();
        }

        [Authorize(Policy = "ui.account.signout")]
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        [Authorize(Policy = "ui.account.wishlist")]
        public IActionResult Wishlist()
        {
            return View();
        }
    }
}
