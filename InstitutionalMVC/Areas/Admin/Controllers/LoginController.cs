using EntitiesLayer.ModelDTO;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BadLogin()
        {
                string Baddata = "Bilgileriniz hatalıdır kontrol ederek tekrar deneyin";
                ViewBag.Baddata = Baddata;  
            return View("Index");
        }
    }
}
