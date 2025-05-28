using Microsoft.AspNetCore.Mvc;
using Cristalyn.Models;
using Cristalyn.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cristalyn.Controllers
{
    public class PuncteFidelitateController : Controller
    {
        private readonly CristalynContext _context;

        public PuncteFidelitateController(CristalynContext context)
        {
            _context = context;
        }

        // GET: /PuncteFidelitate
        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Cont");
            }

            var puncte = _context.PuncteFidelitate
                .FirstOrDefault(p => p.EmailUtilizator == userEmail);

            if (puncte == null)
            {
                puncte = new PuncteFidelitate
                {
                    EmailUtilizator = userEmail,
                    PuncteAcumulate = 0,
                    PuncteFolosite = 0
                };
            }

            // Obținem istoricul comenzilor pentru afișarea punctelor acumulate
            var comenzi = _context.Comenzi
                .Where(c => c.EmailUtilizator == userEmail)
                .OrderByDescending(c => c.Data)
                .Take(5)
                .ToList();

            ViewBag.Comenzi = comenzi;

            return View(puncte);
        }

        // POST: /PuncteFidelitate/Foloseste
        [HttpPost]
        public IActionResult Foloseste(int puncteDorite)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Json(new { succes = false, mesaj = "Trebuie să fiți autentificat" });
            }

            var puncte = _context.PuncteFidelitate
                .FirstOrDefault(p => p.EmailUtilizator == userEmail);

            if (puncte == null || puncte.PuncteDisponibile < puncteDorite)
            {
                return Json(new { 
                    succes = false, 
                    mesaj = "Nu aveți suficiente puncte disponibile" 
                });
            }

            // Salvăm în sesiune pentru a fi folosite la finalizarea comenzii
            HttpContext.Session.SetInt32("PuncteFidelitateUtilizate", puncteDorite);

            return Json(new { 
                succes = true, 
                reducere = puncteDorite,
                mesaj = $"Vor fi folosite {puncteDorite} puncte pentru o reducere de {puncteDorite} MDL" 
            });
        }

        // POST: /PuncteFidelitate/Anuleaza
        [HttpPost]
        public IActionResult Anuleaza()
        {
            HttpContext.Session.Remove("PuncteFidelitateUtilizate");
            return Json(new { succes = true });
        }
    }

    // Extensie pentru a lucra cu int în sesiune
    public static class SessionExtensions
    {
        public static void SetInt32(this ISession session, string key, int value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }

        public static int? GetInt32(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null || data.Length < 4)
                return null;
            return BitConverter.ToInt32(data, 0);
        }
    }
} 