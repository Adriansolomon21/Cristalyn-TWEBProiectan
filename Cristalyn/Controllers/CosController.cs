using Microsoft.AspNetCore.Mvc;
using Cristalyn.Models;
using Cristalyn.Helpers;
using Cristalyn.Data; // pentru contextul EF
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cristalyn.Controllers
{
    public class CosController : Controller
    {
        private const string SessionKey = "Cos";
        private readonly CristalynContext _context;

        public CosController(CristalynContext context)
        {
            _context = context;
        }

        // Afișare coș
        public IActionResult Index()
        {
            var cos = HttpContext.Session.GetObject<List<CosItem>>(SessionKey) ?? new List<CosItem>();
            return View(cos);
        }

        // Adaugă produs în coș
        [HttpPost]
        public IActionResult Adauga(int id, string nume, decimal pret)
        {
            var cos = HttpContext.Session.GetObject<List<CosItem>>(SessionKey) ?? new List<CosItem>();

            var item = cos.FirstOrDefault(p => p.ProdusId == id);
            if (item != null)
                item.Cantitate++;
            else
                cos.Add(new CosItem { ProdusId = id, Nume = nume, Pret = pret, Cantitate = 1 });

            HttpContext.Session.SetObject(SessionKey, cos);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AdaugaInCos(int id)
        {
            var produs = _context.Produse.FirstOrDefault(p => p.Id == id);
            if (produs == null) return NotFound();

            var cos = HttpContext.Session.GetObject<List<CosItem>>(SessionKey) ?? new List<CosItem>();

            var existent = cos.FirstOrDefault(p => p.Id == id);
            if (existent != null)
            {
                existent.Cantitate++;
            }
            else
            {
                cos.Add(new CosItem
                {
                    Id = produs.Id,
                    Nume = produs.Nume,
                    Pret = produs.Pret,
                    Cantitate = 1
                });
            }

            HttpContext.Session.SetObject(SessionKey, cos);
            TempData["Mesaj"] = "Produsul a fost adăugat în coș.";

            return RedirectToAction("Detalii", "Magazin", new { id = id });
        }
        // Comenzile utilizatorului (temporar: toate)
        public IActionResult ComenzileMele()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var rol = HttpContext.Session.GetString("UserRol");

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Cont");

            List<Comanda> comenzi;

            if (rol == "admin")
            {
                comenzi = _context.Comenzi
                    .Include(c => c.Produse)
                    .OrderByDescending(c => c.Data)
                    .ToList();
            }
            else
            {
                comenzi = _context.Comenzi
                    .Where(c => c.EmailUtilizator == email)
                    .Include(c => c.Produse)
                    .OrderByDescending(c => c.Data)
                    .ToList();
            }

            return View(comenzi);
        }

        [HttpPost]
        public IActionResult Sterge(int id)
        {
            var cos = HttpContext.Session.GetObject<List<CosItem>>("Cos") ?? new List<CosItem>();

            var itemDeSters = cos.FirstOrDefault(i => i.Id == id);
            if (itemDeSters != null)
            {
                cos.Remove(itemDeSters);
                HttpContext.Session.SetObject("Cos", cos);
            }

            return RedirectToAction("Index");
        }


        public IActionResult DetaliiComanda(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var rol = HttpContext.Session.GetString("UserRol");

            var comanda = _context.Comenzi
                .Include(c => c.Produse)
                .FirstOrDefault(c => c.Id == id);

            if (comanda == null)
                return NotFound();

            if (rol != "admin" && comanda.EmailUtilizator != email)
                return Unauthorized();

            return View(comanda);
        }
        [HttpPost]

        [HttpGet]
        public IActionResult Finalizeaza()
        {
            var cos = HttpContext.Session.GetObject<List<CosItem>>(SessionKey);

            if (cos == null || !cos.Any())
            {
                TempData["Mesaj"] = "Coșul este gol.";
                return RedirectToAction("Index");
            }

            // Construim comanda
            var comanda = new Comanda
            {
                EmailUtilizator = HttpContext.Session.GetString("UserEmail"), // Poți lega de un utilizator real
                Data = DateTime.Now,
                Produse = cos
            };

            _context.Comenzi.Add(comanda);
            _context.SaveChanges();

            HttpContext.Session.Remove(SessionKey); // Golește coșul

            TempData["MesajSucces"] = "Comanda a fost plasată cu succes!";
            return RedirectToAction("ComenzileMele");

        }


    }
}
