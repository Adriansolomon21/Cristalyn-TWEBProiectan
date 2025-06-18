using Microsoft.AspNetCore.Mvc;
using Cristalyn.Models;
using Cristalyn.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Cristalyn.Helpers;
using Microsoft.Extensions.Caching.Memory;

namespace Cristalyn.Controllers
{
    public class PuncteFidelitateController : Controller
    {
        private readonly CristalynContext _context;
        private readonly IMemoryCache _cache;
        private const string USER_POINTS_CACHE_KEY = "UserPoints_{0}"; // {0} = email
        private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(5);

        public PuncteFidelitateController(CristalynContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: /PuncteFidelitate
        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Cont");
            }

            var cacheKey = string.Format(USER_POINTS_CACHE_KEY, userEmail);
            
            if (!_cache.TryGetValue(cacheKey, out PuncteFidelitate puncte))
            {
                puncte = _context.PuncteFidelitate
                    .AsNoTracking()
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

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(CACHE_DURATION)
                    .SetPriority(CacheItemPriority.High);

                _cache.Set(cacheKey, puncte, cacheOptions);
            }

            // Obținem istoricul comenzilor pentru afișarea punctelor acumulate
            // Cache this separately as it changes more frequently
            var comenzi = _context.Comenzi
                .AsNoTracking()
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
                .AsNoTracking()
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

            // Invalidate the cache since points will be used
            _cache.Remove(string.Format(USER_POINTS_CACHE_KEY, userEmail));

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
            var userEmail = HttpContext.Session.GetString("UserEmail");
            HttpContext.Session.Remove("PuncteFidelitateUtilizate");
            
            if (!string.IsNullOrEmpty(userEmail))
            {
                // Invalidate the cache since points usage was cancelled
                _cache.Remove(string.Format(USER_POINTS_CACHE_KEY, userEmail));
            }
            
            return Json(new { succes = true });
        }
    }
} 