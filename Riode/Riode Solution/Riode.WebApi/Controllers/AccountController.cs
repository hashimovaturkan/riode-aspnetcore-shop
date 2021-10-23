using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Riode.Application.Core.Extensions;
using Riode.Domain.Models.DataContexts;
using Riode.Domain.Models.Entities.Membership;
using Riode.Domain.Models.FormModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Riode.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly UserManager<RiodeUser> userManager;
        readonly SignInManager<RiodeUser> signInManager;
        readonly RiodeDbContext db;
        readonly IConfiguration configuration;
        public AccountController(UserManager<RiodeUser> userManager,
                                 SignInManager<RiodeUser> signInManager,
                                 RiodeDbContext db,
                                 IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.db = db;
            this.configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            RiodeUser foundedUser = null;
            if (model.UserName.IsEmail() == true)
            {
                foundedUser = await userManager.FindByEmailAsync(model.UserName);
            }
            else
            {
                foundedUser = await userManager.FindByNameAsync(model.UserName);
            }

            if (foundedUser == null)
            {
                return NotFound(new {
                    error = true,
                    token = (string)null,
                    message = "Your username or password is incorrect!"
                });

            }

            //model.Password = userManager.PasswordHasher.HashPassword(foundedUser, model.Password);
            var result = userManager.PasswordHasher.VerifyHashedPassword(foundedUser, foundedUser.PasswordHash, model.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                return NotFound(new
                {
                    error = true,
                    token = (string)null,
                    message = "Your username or password is incorrect!"

                });
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, foundedUser.UserName),
                new Claim(ClaimTypes.Email, foundedUser.Email),
                new Claim(ClaimTypes.NameIdentifier, foundedUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var _token = new JwtSecurityToken(
                issuer: configuration["JWT:validIssuer"],
                audience: configuration["JWT:validAudience"],
                expires: DateTime.Now.AddMinutes(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                error = false,
                token = new JwtSecurityTokenHandler().WriteToken(_token),
                expiration = _token.ValidTo
            });
        }
    }
}
