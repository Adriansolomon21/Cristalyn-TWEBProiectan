using Microsoft.AspNetCore.Mvc;
using Cristalyn.Models;
using Cristalyn.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cristalyn.Controllers
{
    public class CupoaneController : Controller
    {
        private readonly CristalynContext _context;

        public CupoaneController(CristalynContext context)
        {
            _context = context;
        }

        // GET: /Cupoane
        public IActionResult Index()
        {
            var cupoane = _context.Cupoane.ToList();
            return View(cupoane);
        }

        // GET: /Cupoane/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Cupoane/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cupon cupon)
        {
            if (ModelState.IsValid)
            {
                if (_context.Cupoane.Any(c => c.Cod == cupon.Cod))
                {
                    ModelState.AddModelError("Cod", "Acest cod de cupon există deja");
                    return View(cupon);
                }

                _context.Cupoane.Add(cupon);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cupon);
        }

        // POST: /Cupoane/Aplica
        [HttpPost]
        public IActionResult Aplica(string cod)
        {
            var cupon = _context.Cupoane
                .FirstOrDefault(c => c.Cod == cod);

            if (cupon == null)
            {
                return Json(new { succes = false, mesaj = "Cuponul nu există" });
            }

            if (!cupon.EsteValid())
            {
                return Json(new { succes = false, mesaj = "Cuponul nu mai este valid" });
            }

            // Obținem coșul curent din sesiune
            var cosTotal = HttpContext.Session.GetDecimal("CosTotal") ?? 0;

            if (cupon.ValoareMinimaComanda.HasValue && cosTotal < cupon.ValoareMinimaComanda.Value)
            {
                return Json(new { 
                    succes = false, 
                    mesaj = $"Cuponul poate fi folosit doar pentru comenzi de minimum {cupon.ValoareMinimaComanda} MDL" 
                });
            }

            decimal reducere = cupon.EsteProcentual 
                ? cosTotal * (cupon.Valoare / 100)
                : cupon.Valoare;

            HttpContext.Session.SetDecimal("ReducereCupon", reducere);
            HttpContext.Session.SetString("CodCupon", cod);

            return Json(new { 
                succes = true, 
                reducere = reducere,
                mesaj = $"Cupon aplicat cu succes! Reducere: {reducere:N2} MDL" 
            });
        }
    }

    // Extensie pentru a lucra cu decimal în sesiune
    public static class SessionExtensions
    {
        public static void SetDecimal(this ISession session, string key, decimal value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }

        public static decimal? GetDecimal(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null || data.Length < 8)
                return null;
            return BitConverter.ToDecimal(data, 0);
        }
    }
} 