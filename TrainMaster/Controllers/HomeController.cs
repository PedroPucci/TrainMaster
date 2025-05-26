using Microsoft.AspNetCore.Mvc;

namespace TrainMaster.Controllers
{
    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    var userCpf = HttpContext.Session.GetString("UserId");

        //    if (string.IsNullOrEmpty(userCpf))
        //        return RedirectToAction("Index", "Login");

        //    ViewBag.UserCpf = userCpf;
        //    return View();
        //}
        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            ViewBag.UserId = userId;

            return View();
        }
    }
}