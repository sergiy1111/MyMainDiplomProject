using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMainDiplomProject.Data;
using MyMainDiplomProject.Models;
using System.Diagnostics;

namespace MyMainDiplomProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyMainDiplomProjectDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyMainDiplomProjectDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            List<Post> posts = _context.Posts.Include(p => p.User).Include(p => p.PostHashTags).Include(p => p.Files).ToList();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}