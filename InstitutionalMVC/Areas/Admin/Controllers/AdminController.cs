using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private static TokenDTO tokenDTO;
        public async Task<IActionResult> Index(UserForAuthenticationDTO userForAuthentication)
        {
            string url = Extancion.Client.BaseAddress + "api/Authentication/login";
            var data =  await Extancion.Client.PostAsJsonAsync(url, userForAuthentication);
            tokenDTO = await data.Content.ReadFromJsonAsync<TokenDTO>();
            if (tokenDTO.AccessToken != null ) 
            {
                Extancion.Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenDTO.AccessToken);
            }
            
            if (data.ReasonPhrase == "OK")
            {
               
                ViewBag.Token = tokenDTO.AccessToken;
               return View("IndexAdmin");
            }
            else 
            {
                //string Baddata = "Bilgileriniz hatalıdır kontrol ederek tekrar deneyin";
                return RedirectToAction("BadLogin", "Login");
            }
        }
         public async Task<IActionResult> TokenDelete() 
        {
            tokenDTO = new TokenDTO();
            return RedirectToAction("Index", "Login");
        }
      
    }
}
