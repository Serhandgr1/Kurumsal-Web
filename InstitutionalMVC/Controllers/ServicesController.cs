using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace InstitutionalMVC.Controllers
{
    public class ServicesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            GetRequest<ServicesDTO> request = new GetRequest<ServicesDTO>();
            List<ServicesDTO> ServiceApi = await request.GetHttpRequest("api/Service/get-all-service");
            return View("Index" , ServiceApi);
        }
    }
}
