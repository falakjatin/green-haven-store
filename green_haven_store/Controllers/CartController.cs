using green_haven_store.Data;
using green_haven_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace green_haven_store.Controllers
{
    public class CartController : Controller
    {
        private readonly AllDBContext allDBContext;

        public CartController(AllDBContext allDBContext)
        {
            this.allDBContext = allDBContext;
        }
        public IActionResult Index()
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            ViewBag.TotalQuantity = this.GetCartCount();
            ViewBag.TotalOrderCount = this.GetOrderCount();
            var UserId = HttpContext.Session.GetInt32("_UId");
            var AllCart = (from pd in allDBContext.Products
                                   join ct in allDBContext.Carts on pd.P_Id equals ct.ProductId
                                   where ct.isOrderAdded != true
                                   orderby ct.Cart_Id
                                   select new CartViewModel
                                   {
                                       Cart_Id = ct.Cart_Id,
                                       P_Id = pd.P_Id,
                                       P_Name = pd.P_Name,
                                       P_Price = pd.P_Price,
                                       P_PictureUri = pd.P_PictureUri,
                                       Cart_Quantity = ct.Cart_Quantity,
                                       Cart_Date = ct.Cart_Date,
                                   }).ToList();
            return View(AllCart);
        }

        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            var UserId = HttpContext.Session.GetInt32("_UId");

            var Cart = allDBContext.Carts.FirstOrDefault(e => e.ProductId == id && e.UserId == UserId && e.isOrderAdded != true);

            if (Cart is not null)
            {
                Cart.Cart_Quantity += 1;
                allDBContext.SaveChanges();
            }
            else
            {
                var cartData = new Cart
                {
                    ProductId = id,
                    UserId = (int)UserId,
                    Cart_Quantity = 1,
                    Cart_Date = DateTime.Now,
                };

                if (id != 0 && UserId != 0)
                {
                    try {
                        allDBContext.Carts.Add(cartData);
                        allDBContext.SaveChanges();
                        // TempData["message"] = "Product to Cart!";
                    }
                    catch (DbUpdateException ex)
                            {
                        SqlException innerException = ex.InnerException?.InnerException as SqlException;
                        if (innerException != null && innerException.Number == 2627) // Error number for duplicate key violation
                        {
                            // Handle the duplicate key violation here
                            // You can choose to ignore, log, or take any appropriate action
                        }
                        else
                        {
                            // Handle other DbUpdateException cases
                        }
                    }
                }
                else
                {
                    // TempData["message"] = "Product is not added to Cart!";
                }
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            var UserId = HttpContext.Session.GetInt32("_UId");

            var cart = allDBContext.Carts.FirstOrDefault(c => c.Cart_Id == id);

            if (cart == null)
            {
                // no data found
                return RedirectToAction("Index", "Cart");
            }

            allDBContext.Carts.Remove(cart);
            allDBContext.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult IncrementQuantity(int id)
        {

            var UserId = HttpContext.Session.GetInt32("_UId");

            var Cart = allDBContext.Carts.FirstOrDefault(e => e.Cart_Id == id && e.UserId == UserId);

            if (Cart is not null)
            {
                Cart.Cart_Quantity += 1;
                allDBContext.SaveChanges();
            }

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult DecrimentQuantity(int id)
        {
            var UserId = HttpContext.Session.GetInt32("_UId");

            var Cart = allDBContext.Carts.FirstOrDefault(e => e.Cart_Id == id && e.UserId == UserId);

            if (Cart is not null && Cart.Cart_Quantity > 0)
            {
                Cart.Cart_Quantity -= 1;
                allDBContext.SaveChanges();
            }

            return RedirectToAction("Index", "Cart");
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
