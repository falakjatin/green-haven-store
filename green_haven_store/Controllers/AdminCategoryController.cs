using green_haven_store.Data;
using green_haven_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace green_haven_store.Controllers
{
    public class AdminCategoryController : Controller
    {
        private readonly AllDBContext allDBContext;

        public AdminCategoryController(AllDBContext allDBContext)
        {
            this.allDBContext = allDBContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Categories = allDBContext.Categories;
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult AddCategory(Category categoryReq)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var CategoryData = new Category
            {
                Cat_Name = categoryReq.Cat_Name,
                Cat_Date = categoryReq.Cat_Date,
                Cat_IsActive = true
            };

            var Categories = allDBContext.Categories.ToList();
            string msg = CheckCategoryExists(Categories, categoryReq);

            if (!string.IsNullOrEmpty(msg))
            {
                TempData["errorMsg"] = msg;
                return View();
            }
            else
            {
                allDBContext.Categories.Add(CategoryData);
                allDBContext.SaveChanges();
                TempData["message"] = $"Category {categoryReq.Cat_Name} added!";
                return View("Add");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var category = allDBContext.Categories.FirstOrDefault(u => u.Cat_Id == id);

            if (category == null)
            {
                // no data found
                return RedirectToAction("Index", "AdminCategory");
            }

            allDBContext.Categories.Remove(category);
            allDBContext.SaveChanges();

            ViewBag.Categories = allDBContext.Categories;
            return RedirectToAction("Index", "AdminCategory");
        }

        public static string CheckCategoryExists(List<Category> data, Category reqCategory)
        {
            var category = data.Any(e => e.Cat_Name == reqCategory.Cat_Name);

            if (!category)
            {
                return "";
            }
            else
            {
                return $"{reqCategory.Cat_Name} is already exits please add another one!.";
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var category = allDBContext.Categories.FirstOrDefault(u => u.Cat_Id == id);

            if (category == null)
            {
                // no data found
                return RedirectToAction("Index", "AdminCategory");
            }

            ViewBag.Categories = allDBContext.Categories;
            ViewBag.CategoryEdit = category;
            return View(category);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditCategory(Category categoryReq)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var category = allDBContext.Categories.FirstOrDefault(u => u.Cat_Id == categoryReq.Cat_Id);

            if (category is not null)
            {
                category.Cat_Name = categoryReq.Cat_Name;
                category.Cat_Date = categoryReq.Cat_Date;
                allDBContext.SaveChanges();
            }

            TempData["message"] = $"Category {categoryReq.Cat_Name} Updated!";

            ViewBag.Categories = allDBContext.Categories;
            return RedirectToAction("Index", "AdminCategory");
        }
    }
}
