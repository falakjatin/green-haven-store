using green_haven_store.Data;
using green_haven_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace green_haven_store.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly AllDBContext allDBContext;

        public AdminProductController(AllDBContext allDBContext)
        {
            this.allDBContext = allDBContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Products = allDBContext.Products;
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            ViewBag.Categories = allDBContext.Categories;
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var product = allDBContext.Products.FirstOrDefault(u => u.P_Id == id);

            if (product == null)
            {
                // no data found
                return RedirectToAction("Index", "AdminProduct");
            }

            allDBContext.Products.Remove(product);
            allDBContext.SaveChanges();

            ViewBag.Products = allDBContext.Products;
            return RedirectToAction("Index", "AdminProduct");
        }


        [HttpPost]
        [ActionName("Add")]
        public IActionResult AddProduct(Product productReq)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");


            if(productReq.P_Name != null && productReq.P_Description != null && productReq.P_Price != null && productReq.CategoryId != 0 && productReq.P_PictureUri != null) 
            {

                var ProductData = new Product
                {
                    P_Name = productReq.P_Name,
                    P_Description = productReq.P_Description,
                    P_Price = productReq.P_Price,
                    CategoryId  = productReq.CategoryId,
                    P_PictureUri = productReq.P_PictureUri,
                    P_Date = DateTime.Now,
                    P_IsActive = true,
                };

                // file upload
                /*var memoryStream = new MemoryStream();
                productReq.P_Picture.CopyTo(memoryStream);
                ProductData.P_Picture = memoryStream.ToArray();*/

                allDBContext.Products.Add(ProductData);
                allDBContext.SaveChanges();
                TempData["message"] = $"Product {productReq.P_Name} added!";
            } else
            {
                TempData["error"] = $"Please provide required details! like Product Name, Description, Price, Category and Picture-Uri";
            }

            
            ViewBag.Categories = allDBContext.Categories;
            return View("Add");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var product = allDBContext.Products.FirstOrDefault(u => u.P_Id == id);

            if (product == null)
            {
                // no data found
                return RedirectToAction("Index", "AdminProduct");
            }

            ViewBag.Categories = allDBContext.Categories;
            ViewBag.ProductEdit = product;
            return View(product);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditProduct(ProductEdit productReq)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var product = allDBContext.Products.FirstOrDefault(u => u.P_Id == productReq.P_Id);

            if (product is not null)
            {
                product.P_Name = productReq.P_Name;
                product.P_Description = productReq.P_Description;
                product.P_Price = productReq.P_Price;
                product.CategoryId = productReq.CategoryId;
                product.P_PictureUri = productReq.P_PictureUri;
                allDBContext.SaveChanges();
            }

            TempData["message"] = $"Product {productReq.P_Name} Updated!";

            ViewBag.Products = allDBContext.Products;
            return RedirectToAction("Index", "AdminProduct");
        }
    }
}
