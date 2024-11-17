using Microsoft.AspNetCore.Mvc;
using green_haven_store.Models;
using green_haven_store.Data;
using System.Diagnostics;

namespace green_haven_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AllDBContext allDBContext;

        public HomeController(ILogger<HomeController> logger, AllDBContext allDBContext)
        {
            _logger = logger;
            this.allDBContext = allDBContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            ViewBag.TotalQuantity = this.GetCartCount();
            ViewBag.TotalOrderCount = this.GetOrderCount();
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public int GetCartCount()
        {
            var UserId = HttpContext.Session.GetInt32("_UId");
            var Cart = allDBContext.Carts.Where(e => e.UserId == UserId && e.isOrderAdded != true);
            var TotalQuantity = 0;
            if (Cart is not null)
            {
                TotalQuantity = (int)Cart.Count();
            }
            return TotalQuantity;
        }

        public int GetOrderCount()
        {
            var UserId = HttpContext.Session.GetInt32("_UId");
            var Order = allDBContext.Orders.Where(e => e.UserId == UserId);
            var TotalOrderCount = 0;
            if (Order is not null)
            {
                TotalOrderCount = (int)Order.Count();
            }
            return TotalOrderCount;
        }
    }
}