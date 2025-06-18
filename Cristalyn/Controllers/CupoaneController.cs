using Microsoft.AspNetCore.Mvc;
using Cristalyn.Models;
using Cristalyn.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Cristalyn.Helpers;
using Microsoft.Extensions.Caching.Memory;

namespace Cristalyn.Controllers
{
    public class CupoaneController : Controller
    {
        private readonly CristalynContext _context;
        private readonly IMemoryCache _cache;
        private const string ALL_CUPOANE_CACHE_KEY = "AllCupoane";
        private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(10);

        public CupoaneController(CristalynContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: /Cupoane
        public IActionResult Index()
        {
            if (!_cache.TryGetValue(ALL_CUPOANE_CACHE_KEY, out List<Cupon> cupoane))
            {
                cupoane = _context.Cupoane
                    .AsNoTracking()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(CACHE_DURATION)
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(ALL_CUPOANE_CACHE_KEY, cupoane, cacheOptions);
            }

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
                
                // Invalidate cache when new cupon is added
                _cache.Remove(ALL_CUPOANE_CACHE_KEY);
                
                return RedirectToAction(nameof(Index));
            }
            return View(cupon);
        }

        // POST: /Cupoane/Aplica
        [HttpPost]
        public IActionResult Aplica(string cod)
        {
            var cupon = _context.Cupoane
                .AsNoTracking()
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
} 