using Microsoft.AspNetCore.Mvc;
using BigAssignment.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace BigAssignment.Controllers
{
    public class RegisterController : Controller
    {
       private readonly List<User> _users= new List<User>();
        private readonly MovieWebContext _dbContext;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
    

        public RegisterController(MovieWebContext context)
        {
            _dbContext = context;
        }

        //GET: Register

        public IActionResult Register()
        {
            return View();
        }

        /*   //POST: Register
           [HttpPost]
           [ValidateAntiForgeryToken]
           public ActionResult Register(User _user)
           {
               if (ModelState.IsValid)
               {
                   var check = _dbContext.Users.FirstOrDefault(s => s.Email == _user.Email);
                   if (check == null)
                   {
                       _user.Password = GetMD5(_user.Password);

                       _dbContext.Users.Add(_user);
                       _dbContext.SaveChanges();
                       return RedirectToAction("Login", "Access");
                   }
                   else
                   {
                       ViewBag.error = "Email already exists";
                       return View();
                   }


               }
               return View();


           }*/

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Fullname))
            {
                ModelState.AddModelError("Fullname", "Fullname is required");
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                ModelState.AddModelError("Email", "Email is required");
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                ModelState.AddModelError("Password", "Password is required");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(user);
            }

            var model = new User
            {
                Fullname = user.Fullname,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            _dbContext.Users.Add(model);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


    }
}
