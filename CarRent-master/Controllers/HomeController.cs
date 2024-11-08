using CarRent.Data;
using CarRent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarRent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CarDbContext _context;

        public HomeController(ILogger<HomeController> logger, CarDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public IActionResult Index()
        {
           
            var car = _context.Car.ToList();
            return View(car);
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