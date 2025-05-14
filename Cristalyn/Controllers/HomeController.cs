using Microsoft.AspNetCore.Mvc;
using Cristalyn.Data; // asigură-te că ai namespace-ul corect
using System.Linq;

namespace Cristalyn.Controllers
{
    public class HomeController : Controller
    {
        private readonly CristalynContext _context;

        public HomeController(CristalynContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var produse = _context.Produse.ToList();
            return View(produse);
        }
    }
}
