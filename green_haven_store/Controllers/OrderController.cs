using green_haven_store.Data;
using green_haven_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace green_haven_store.Controllers
{
    public class OrderController : Controller
    {
        private readonly AllDBContext allDBContext;

        public OrderController(AllDBContext allDBContext)
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
            var AllOrder = (from odr in allDBContext.Orders
                            where odr.UserId == UserId
                            orderby odr.Order_Id
                                select new OrderViewModel
                                {
                                    Order_Id = odr.Order_Id,
                                    Order_PayableAmount = odr.Order_PayableAmount,
                                    Order_Tax = 0.0,
                                    Order_Quantity = odr.Order_Quantity,
                                    Order_Date = odr.Order_Date,
                                    Order_IsPaymentDone = odr.Order_IsPaymentDone
                                }).ToList();
            return View(AllOrder);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            var UserId = HttpContext.Session.GetInt32("_UId");

            var order = allDBContext.Orders.FirstOrDefault(c => c.Order_Id == id);

            if (order == null)
            {
                // no data found
                return RedirectToAction("Index", "Order");
            }

            allDBContext.Orders.Remove(order);
            allDBContext.SaveChanges();

            return RedirectToAction("Index", "Order");

        }

        [HttpGet]
        public IActionResult MakePayment(int id)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            var UserId = HttpContext.Session.GetInt32("_UId");

            var order = allDBContext.Orders.FirstOrDefault(e => e.Order_Id == id && e.UserId == UserId);

            if (order == null)
            {
                // no data found
                return RedirectToAction("Index", "Order");
            }

            order.Order_IsPaymentDone = true;
            allDBContext.SaveChanges();

            return RedirectToAction("Index", "Order");

        }

        [HttpGet("CheckOut/{id}")]
        public IActionResult CheckOut(string id)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var UserId = HttpContext.Session.GetInt32("_UId");
            int[] idArray = id.Split(',').Select(int.Parse).ToArray();
            var CartIds = idArray;
            var MatchingCarts = new List<CartViewModel>();
            var MakeOrderList = new List<Order>();

            if (CartIds is not null)
            {

                MatchingCarts = (from pd in allDBContext.Products
                                join ct in allDBContext.Carts on pd.P_Id equals ct.ProductId
                                where CartIds.Contains(ct.Cart_Id)
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

                if (MatchingCarts.Count > 0)
                {
                    // has data
                    double totalPrice = 0.0;
                    int totalCartQuantity = 0;
                    List<int> ProductIdList = new List<int>(Array.Empty<int>());
                    List<int> CartIdList = new List<int>(Array.Empty<int>());

                    foreach (var cart in MatchingCarts)
                    {
                        double signlePrice = double.Parse(cart.P_Price);

                        if (signlePrice != 0.0)
                        {
                            double amount = signlePrice * (double)cart.Cart_Quantity;
                            totalPrice += amount;
                            totalCartQuantity += (int)cart.Cart_Quantity;

                        }
                        ProductIdList.Add(cart.P_Id);
                        CartIdList.Add(cart.Cart_Id);

                    }

                    if(ProductIdList.Count > 0 && CartIdList.Count > 0)
                    {


                        string idsQueryProductString = string.Join(",", ProductIdList);
                        string idsQueryCartString = string.Join(",", CartIdList);
                        var order = new Order
                        {
                            ProductIds = Int32.Parse(idsQueryProductString),
                            CartIds = Int32.Parse(idsQueryCartString),
                            UserId = (int)UserId,
                            Order_Tax = 0,
                            Order_PayableAmount = totalPrice,
                            Order_Quantity = totalCartQuantity,
                            Order_IsPaymentDone = false,
                            Order_Date = DateTime.Now,
                        };

                        if (order is not null)
                        {
                           
                            try
                            {
                                allDBContext.Orders.Add(order);
                                allDBContext.SaveChanges();

                                foreach (var cart in MatchingCarts)
                                {
                                    var Cart = allDBContext.Carts.FirstOrDefault(e => e.Cart_Id == cart.Cart_Id);

                                    if (Cart is not null)
                                    {
                                        Cart.isOrderAdded = true;
                                        allDBContext.SaveChanges();
                                    }
                                }
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

                            
                            MakeOrderList.Add(order);
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Order");
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
