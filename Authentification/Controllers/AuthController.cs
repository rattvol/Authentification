using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentification.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Authentification.Controllers
{
    public class AuthController : Controller
    {
        AuthContext _context;
        public AuthController(AuthContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Login([Bind("Login,Password")] LoginData loginData)
        {
            if (ModelState.IsValid)
            {
                if (!_context.User.Any(b => b.Login == loginData.Login)) { return RedirectToAction("Registration"); }
                User user = await _context.User.Where(b => b.Login == loginData.Login).FirstAsync();
                if (user.Password == loginData.Password) return new OkObjectResult(GetPublic.GetPublicUser(user));
            }
            return RedirectToAction("Registration"); ;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<RegistrationData>> Registration([Bind("Login,Password,ConfirmPassword,Name,MiddleName,SurName")]RegistrationData user)
        {
            // User user = registrationData;
            if (ModelState.IsValid && !_context.User.Any(b => b.Login == user.Login))
            {
                user.DateOfRegistration = (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
                user.Token = TokenGenerator.TokenGenerate(_context);
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return new OkObjectResult(GetPublic.GetPublicUser(user));
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult PassChange([FromBody]UserIdentity userIdentity)
        {
            if (_context.User.Any(b => b.Token == userIdentity.Token && b.Id == userIdentity.Id && b.Login == userIdentity.Login))
                return View(_context.User.Where(b=>b.Id==userIdentity.Id).Select(b=>new PassChange() 
                { 
                    Id = b.Id,
                    Password = b.Password,
                    Login = b.Login
                }));
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PassChange>> PassChange([Bind("Id,Login,Password,NewPassword,ConfirmNewPassword")]PassChange pass)
        {
            // User user = registrationData;
            if (ModelState.IsValid)
            {
                string token = TokenGenerator.TokenGenerate(_context);
                await _context.User.Where(b=>b.Id==pass.Id).UpdateAsync(b=>new Models.User() { Password = pass.Password, Token = token });
                return new OkObjectResult(GetPublic.GetPublicUser(await _context.User.Where(b => b.Id == pass.Id).FirstAsync()));
            }
            return View(pass);
        }

    }
}
