using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Cristalyn.Models;
using Cristalyn.Data;
using System.Linq;

namespace Cristalyn.Controllers
{
    /// <summary>
    /// Controller for handling user account operations
    /// </summary>
    public class ContController : Controller
    {
        private readonly CristalynContext _context;

        public ContController(CristalynContext context)
        {
            _context = context;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string parola)
        {
            var user = _context.Utilizatori.FirstOrDefault(u => u.Email == email && u.Parola == parola);
            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email ?? "");
                HttpContext.Session.SetString("UserRol", user.Rol ?? "");
                return user.Rol == "admin"
                    ? RedirectToAction("Comenzi", "Admin")
                    : RedirectToAction("Index", "Home");
            }

            ViewBag.Eroare = "Email sau parolă incorectă";
            return View();
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(Utilizator utilizator)
        {
            if (ModelState.IsValid)
            {
                // Setăm automat rolul în funcție de email
                if (utilizator.Email?.ToLower() == "adrian.solomon04.as@gmail.com")
                    utilizator.Rol = "admin";
                else
                    utilizator.Rol = "user";

                _context.Utilizatori.Add(utilizator);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(utilizator);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
