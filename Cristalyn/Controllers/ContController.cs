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

        /// <summary>
        /// Afișează pagina de autentificare
        /// </summary>
        /// <returns>Pagina de autentificare</returns>
        public IActionResult Login() => View();

        /// <summary>
        /// Procesează cererea de autentificare
        /// </summary>
        /// <param name="email">Adresa de email a utilizatorului</param>
        /// <param name="parola">Parola utilizatorului</param>
        /// <returns>Redirecționare către pagina corespunzătoare rolului sau înapoi la login în caz de eroare</returns>
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

        /// <summary>
        /// Afișează pagina de înregistrare
        /// </summary>
        /// <returns>Pagina de înregistrare</returns>
        public IActionResult Register() => View();

        /// <summary>
        /// Procesează cererea de înregistrare a unui nou utilizator
        /// </summary>
        /// <param name="utilizator">Datele noului utilizator</param>
        /// <returns>Redirecționare către pagina de autentificare sau înapoi la înregistrare în caz de eroare</returns>
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

        /// <summary>
        /// Procesează cererea de deconectare a utilizatorului
        /// </summary>
        /// <returns>Redirecționare către pagina de autentificare</returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
