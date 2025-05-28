using Microsoft.AspNetCore.Mvc;
using Cristalyn.Models;
using Cristalyn.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cristalyn.Controllers
{
    public class ReduceriController : Controller
    {
        private readonly CristalynContext _context;

        public ReduceriController(CristalynContext context)
        {
            _context = context;
        }

        // GET: /Reduceri
        public IActionResult Index()
        {
            var reduceri = _context.ReduceriCategorii
                .OrderBy(r => r.Categorie)
                .ToList();
            return View(reduceri);
        }

        // GET: /Reduceri/Create
        public IActionResult Create()
        {
            // Obținem lista unică de categorii din produse
            ViewBag.Categorii = _context.Produse
                .Select(p => p.Categorie)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            return View();
        }

        // POST: /Reduceri/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReducereCategorie reducere)
        {
            if (ModelState.IsValid)
            {
                // Verificăm dacă există deja o reducere activă pentru această categorie
                var existingReducere = _context.ReduceriCategorii
                    .Any(r => r.Categorie == reducere.Categorie && 
                         r.ValidPanaLa > DateTime.Now);

                if (existingReducere)
                {
                    ModelState.AddModelError("", "Există deja o reducere activă pentru această categorie");
                    return View(reducere);
                }

                _context.ReduceriCategorii.Add(reducere);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorii = _context.Produse
                .Select(p => p.Categorie)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            return View(reducere);
        }

        // GET: /Reduceri/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var reducere = _context.ReduceriCategorii.Find(id);
            if (reducere == null)
                return NotFound();

            ViewBag.Categorii = _context.Produse
                .Select(p => p.Categorie)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            return View(reducere);
        }

        // POST: /Reduceri/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ReducereCategorie reducere)
        {
            if (id != reducere.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reducere);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ReduceriCategorii.Any(r => r.Id == reducere.Id))
                        return NotFound();
                    throw;
                }
            }

            ViewBag.Categorii = _context.Produse
                .Select(p => p.Categorie)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            return View(reducere);
        }

        // POST: /Reduceri/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var reducere = _context.ReduceriCategorii.Find(id);
            if (reducere != null)
            {
                _context.ReduceriCategorii.Remove(reducere);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 