using green_haven_store.Data;
using green_haven_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace green_haven_store.Controllers
{
    public class UserController : Controller
    {
        private readonly AllDBContext allDBContext;

        public UserController(AllDBContext allDBContext)
        {
            this.allDBContext = allDBContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("_type", "");
            HttpContext.Session.SetString("_Name", "");
            TempData.Clear();
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            TempData["type"] = HttpContext.Session.GetString("_type");
            TempData["userName"] = HttpContext.Session.GetString("_Name");
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            var UserEmail = Request.Form["U_Email"];
            var UserPassword = Request.Form["U_Password"];
            const string SessionUserName = "_Name";
            const string SessionUserType = "_type";
            const string SessionUserId = "_UId";

            var Users = allDBContext.Users.ToList();
            var User = Users.Where(e => e.U_Email == UserEmail && e.U_Password == UserPassword).FirstOrDefault();

            if (User is not null)
            {
                // User found so we will redirect to home page
                HttpContext.Session.SetString(SessionUserName, User.U_Name);
                HttpContext.Session.SetString(SessionUserType, User.U_Type);
                HttpContext.Session.SetInt32(SessionUserId, User.U_Id);

                TempData["type"] = HttpContext.Session.GetString("_type");
                TempData["userName"] = HttpContext.Session.GetString("_Name");

                if (User.U_Type.ToString() == "admin")
                {
                    return RedirectToAction("Index", "AdminProduct");
                } else
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            else
            {
                TempData["errorMsg"] = "Please Enter valid Email & Password! or User Does not Exists!";
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        [ActionName("SignUp")]
        public IActionResult SignUp(User userReq)
        {

            var UserData = new User
            {
                U_Name = userReq.U_Name,
                U_Email = userReq.U_Email,
                U_Address = userReq.U_Address,
                U_Password = userReq.U_Password,
                U_Pincode = userReq.U_Pincode,
                U_Type = "user",
                U_Date = DateTime.Now,
                U_IsActive = true
            };

            var Users = allDBContext.Users.ToList();
            string msg = CheckUserExists(Users, userReq);

            if (!string.IsNullOrEmpty(msg))
            {
                TempData["errorMsg"] = msg;
                return View();
            } else {
                allDBContext.Users.Add(UserData);
                allDBContext.SaveChanges();
                TempData["message"] = $"Now Login with {userReq.U_Email} & Password!";
                return RedirectToAction("Index", "User");
            }
        }

        public static string CheckUserExists(List<User> data, User reqUser)
        {
            var user = data.Any(e => e.U_Email == reqUser.U_Email);

            if (!user)
            {
                return "";
            }
            else
            {
                return $"{reqUser.U_Email} is already exits please choose another one!.";
            }
        }
    }
}
