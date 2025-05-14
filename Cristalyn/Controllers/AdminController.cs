using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cristalyn.Models;
using Cristalyn.Data;
using System.Linq;

namespace Cristalyn.Controllers
{
    public class AdminController : Controller
    {
        private readonly CristalynContext _context;

        public AdminController(CristalynContext context)
        {
            _context = context;
        }

        private bool EsteAdmin()
        {
            return HttpContext.Session.GetString("UserRol") == "admin";
        }

        // GET: Adăugare produs
        [HttpGet]
        public IActionResult AdaugaProdus()
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: Adăugare produs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdaugaProdus(Produs produs)
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                _context.Produse.Add(produs);
                _context.SaveChanges();
                return RedirectToAction("ListaProduse");
            }

            return View(produs);
        }

        // GET: Listare produse
        public IActionResult ListaProduse()
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var produse = _context.Produse.ToList();
            return View(produse);
        }

        // GET: Listare comenzi
        public IActionResult Comenzi()
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var comenzi = _context.Comenzi
                .Include(c => c.Produse)
                .OrderByDescending(c => c.Data)
                .ToList();

            return View(comenzi);
        }

        // POST: Modificare status comandă
        [HttpPost]
        [HttpPost]
        public IActionResult ModificaStatus(int id, string statusNou)
        {
            var comanda = _context.Comenzi.Include(c => c.Produse).FirstOrDefault(c => c.Id == id);
            if (comanda == null)
                return NotFound();

            comanda.Status = statusNou;
            _context.SaveChanges();

            TempData["Mesaj"] = "Statusul comenzii a fost actualizat.";
            return RedirectToAction("Comenzi"); // Redirecționează înapoi la listă
        }

    }
}
