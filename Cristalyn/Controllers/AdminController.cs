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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(CristalynContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private bool EsteAdmin()
        {
            return HttpContext.Session.GetString("UserRol") == "admin";
        }

        // GET: Admin Dashboard
        public IActionResult Index()
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var dashboardData = new
            {
                TotalProduse = _context.Produse.Count(),
                TotalComenzi = _context.Comenzi.Count(),
                ComenziNoi = _context.Comenzi.Count(c => c.Status == "Nouă"),
                TotalUtilizatori = _context.Utilizatori.Count()
            };

            return View(dashboardData);
        }

        // GET: Adăugare produs
        [HttpGet]
        public IActionResult AdaugaProdus()
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");
            
            ViewBag.Categorii = new List<string> { "Brățări", "Coliere", "Inele", "Cercei", "Talismane", "Seturi" };
            return View();
        }

        // POST: Adăugare produs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdaugaProdus(Produs produs, IFormFile? imagine)
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                // Handle image upload
                if (imagine != null && imagine.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "produse");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imagine.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagine.CopyToAsync(fileStream);
                    }

                    produs.ImagineUrl = "/img/produse/" + uniqueFileName;
                }

                produs.DataAdaugare = DateTime.Now;
                _context.Produse.Add(produs);
                await _context.SaveChangesAsync();
                
                TempData["Mesaj"] = "Produsul a fost adăugat cu succes!";
                return RedirectToAction("ListaProduse");
            }

            ViewBag.Categorii = new List<string> { "Brățări", "Coliere", "Inele", "Cercei", "Talismane", "Seturi" };
            return View(produs);
        }

        // GET: Listare produse
        public IActionResult ListaProduse()
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var produse = _context.Produse
                .OrderByDescending(p => p.DataAdaugare)
                .ToList();
            return View(produse);
        }

        // GET: Editare produs
        [HttpGet]
        public IActionResult EditeazaProdus(int id)
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var produs = _context.Produse.Find(id);
            if (produs == null)
                return NotFound();

            ViewBag.Categorii = new List<string> { "Brățări", "Coliere", "Inele", "Cercei", "Talismane", "Seturi" };
            return View(produs);
        }

        // POST: Editare produs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditeazaProdus(int id, Produs produs, IFormFile? imagine)
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var produsExistent = await _context.Produse.FindAsync(id);
            if (produsExistent == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                // Handle image upload
                if (imagine != null && imagine.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "produse");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imagine.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagine.CopyToAsync(fileStream);
                    }

                    produsExistent.ImagineUrl = "/img/produse/" + uniqueFileName;
                }

                produsExistent.Nume = produs.Nume;
                produsExistent.Pret = produs.Pret;
                produsExistent.Descriere = produs.Descriere;
                produsExistent.Categorie = produs.Categorie;
                produsExistent.Stoc = produs.Stoc;

                await _context.SaveChangesAsync();
                
                TempData["Mesaj"] = "Produsul a fost actualizat cu succes!";
                return RedirectToAction("ListaProduse");
            }

            ViewBag.Categorii = new List<string> { "Brățări", "Coliere", "Inele", "Cercei", "Talismane", "Seturi" };
            return View(produs);
        }

        // POST: Ștergere produs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StergeProdus(int id)
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var produs = await _context.Produse.FindAsync(id);
            if (produs == null)
                return NotFound();

            _context.Produse.Remove(produs);
            await _context.SaveChangesAsync();
            
            TempData["Mesaj"] = "Produsul a fost șters cu succes!";
            return RedirectToAction("ListaProduse");
        }

        // GET: Listare comenzi
        public IActionResult Comenzi()
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var query = _context.Comenzi
                .Include(c => c.Produse)
                .AsQueryable();

            // Filter by date range if provided
            if (Request.Query.ContainsKey("deLa") && !string.IsNullOrEmpty(Request.Query["deLa"]))
            {
                var deLa = DateTime.Parse(Request.Query["deLa"]);
                query = query.Where(c => c.Data >= deLa);
            }

            if (Request.Query.ContainsKey("panaLa") && !string.IsNullOrEmpty(Request.Query["panaLa"]))
            {
                var panaLa = DateTime.Parse(Request.Query["panaLa"]).AddDays(1);
                query = query.Where(c => c.Data < panaLa);
            }

            var comenzi = query.OrderByDescending(c => c.Data).ToList();
            return View(comenzi);
        }

        // POST: Modificare status comandă
        [HttpPost]
        public async Task<IActionResult> ModificaStatus(int id, string statusNou)
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var comanda = await _context.Comenzi.Include(c => c.Produse).FirstOrDefaultAsync(c => c.Id == id);
            if (comanda == null)
                return NotFound();

            comanda.Status = statusNou;
            await _context.SaveChangesAsync();

            TempData["Mesaj"] = "Statusul comenzii a fost actualizat cu succes!";
            return RedirectToAction("Comenzi");
        }

        // POST: Ștergere comandă
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StergeComanda(int id)
        {
            if (!EsteAdmin()) return RedirectToAction("Index", "Home");

            var comanda = await _context.Comenzi.FindAsync(id);
            if (comanda == null)
                return NotFound();

            _context.Comenzi.Remove(comanda);
            await _context.SaveChangesAsync();
            
            TempData["Mesaj"] = "Comanda a fost ștearsă cu succes!";
            return RedirectToAction("Comenzi");
        }
    }
}
