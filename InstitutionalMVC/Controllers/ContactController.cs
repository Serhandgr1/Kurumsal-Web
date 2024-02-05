using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Json;

namespace InstitutionalMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostMail(ContactDTO contact)
        {
            string url = Extancion.Client.BaseAddress + "api/ContactApi/PostMail";

            await Extancion.Client.PostAsJsonAsync(url, contact);
            // send e mail admin
            return View();
        }

        
    }
}
