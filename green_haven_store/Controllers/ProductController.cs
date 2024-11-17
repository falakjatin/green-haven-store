using green_haven_store.Data;
using Microsoft.AspNetCore.Mvc;

namespace green_haven_store.Controllers
{
    public class ProductController : Controller
    {
        private readonly AllDBContext allDBContext;

        public ProductController(AllDBContext allDBContext)
        {
            this.allDBContext = allDBContext;
        }


        [HttpGet]
        public IActionResult Index()
        {

            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            ViewBag.Products = allDBContext.Products;

            ViewBag.TotalQuantity = this.GetCartCount();
            ViewBag.TotalOrderCount = this.GetOrderCount();
            return View();
        }

        [HttpGet]
        public IActionResult ProductSingleView(int id)
        {

            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            ViewBag.Product = allDBContext.Products.Where((p) => p.P_Id == id).FirstOrDefault();

            ViewBag.TotalQuantity = this.GetCartCount();
            ViewBag.TotalOrderCount = this.GetOrderCount();
            return View();
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
