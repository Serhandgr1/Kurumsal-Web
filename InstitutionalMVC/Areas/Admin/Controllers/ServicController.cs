using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class ServicController : Controller
    {
        GenericRequests<ServicesDTO> genericRequests = new GenericRequests<ServicesDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        public IActionResult Index(string? posts)
        {
            ViewBag.Message = posts;
            return View("ServiceIndex");
        }
        public async Task<IActionResult> GetAllServiceIndex(string? updated)
        {
            ViewBag.Message = updated;
            var data = await genericRequests.GetHttpRequest("api/Service/get-all-service");
            return View("GetServiceIndex" , data);
        }
        public async Task<IActionResult> GetUpdateServiceIndex(ServicesDTO services)
        {
            string image = services.ServiceImage.Remove(services.ServiceImage.Length - 4);
            services.ServiceImage = image;
            return View("GetUpdateService", services);
        }
        public async Task<IActionResult> PostsService(ServicesDTO services)
        {
            switch (services.ServiceImage)
            {
                case "Montaj": services.ServiceImage = "Montaj.png"; break;
                case "Mühendis": services.ServiceImage = "Mühendis.png"; break;
                case "Üretim": services.ServiceImage = "Üretim.png"; break;
                case "Taşıyıcı": services.ServiceImage = "Taşıyıcı.png"; break;
            }
            string posts = await genericRequests.PostRequestGeneric("api/Service/post-service", services);
            return RedirectToAction("Index", "Servic", new { posts = posts });
        }
        public async Task<IActionResult> DeleteService(int id)
        {
            string delete = await deleteRequest.DeleteRequestGeneric("api/Service/delete-service", id);
            return RedirectToAction("GetAllServiceIndex", "Servic", new { updated = delete });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateService(ServicesDTO services)
        {
            switch (services.ServiceImage)
            {
                case "Montaj": services.ServiceImage = "Montaj.png"; break;
                case "Mühendis": services.ServiceImage = "Mühendis.png"; break;
                case "Üretim": services.ServiceImage = "Üretim.png"; break;
                case "Taşıyıcı": services.ServiceImage = "Taşıyıcı.png"; break;
            }
            var update = await genericRequests.UpdateRequestGeneric("api/Service/update-service", services);
            return RedirectToAction("GetAllServiceIndex", "Servic", new { updated = update });

        }
    }
}