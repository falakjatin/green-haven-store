using green_haven_store.Data;
using Microsoft.AspNetCore.Mvc;

namespace green_haven_store.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly AllDBContext allDBContext;

        public AdminUserController(AllDBContext allDBContext)
        {
            this.allDBContext = allDBContext;
        }

        public IActionResult Index()
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var Users = allDBContext.Users.Where(e => e.U_Type != "admin");
            ViewBag.Users = Users;
            ViewBag.UsersCount = Users.Count();

            return View();
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");

            var user = allDBContext.Users.FirstOrDefault(c => c.U_Id == id);

            if (user == null)
            {
                // no data found
                return RedirectToAction("Index", "AdminUser");
            }

            allDBContext.Users.Remove(user);
            allDBContext.SaveChanges();

            return RedirectToAction("Index", "AdminUser");
        }
    }
}
