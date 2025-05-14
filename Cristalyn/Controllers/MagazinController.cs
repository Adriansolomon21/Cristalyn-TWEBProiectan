using Microsoft.AspNetCore.Mvc;
using Cristalyn.Models;
using Microsoft.EntityFrameworkCore;
using Cristalyn.Data;
using System.Linq;

namespace Cristalyn.Controllers
{
    public class MagazinController : Controller
    {
        private readonly CristalynContext _context;

        public MagazinController(CristalynContext context)
        {
            _context = context;
        }

        // Home page - produse evidențiate
        public IActionResult Index()
        {
            var produse = _context.Produse.ToList();
            return View(produse);
        }

        // Toate produsele
        public IActionResult Lista()
        {
            var produse = _context.Produse.ToList();
            return View(produse);
        }

        // Detalii pentru un produs
        public IActionResult Detalii(int id)
        {
            var produs = _context.Produse.FirstOrDefault(p => p.Id == id);
            if (produs == null)
            {
                return NotFound();
            }

            var produseSimilare = _context.Produse
                .Where(p => p.Categorie == produs.Categorie && p.Id != id)
                .Take(4)
                .ToList();

            ViewBag.ProdusSimilare = produseSimilare;

            return View(produs);
        }
    }
}
