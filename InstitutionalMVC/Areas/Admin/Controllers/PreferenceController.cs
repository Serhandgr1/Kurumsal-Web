using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class PreferenceController : Controller
    {
        GenericRequests<PreferenceDTO> genericRequests = new GenericRequests<PreferenceDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        public IActionResult Index(string? posts)
        {
            ViewBag.Message = posts;
            return View("PreferenceIndex");
        }
        public async Task<IActionResult> GetAllPreferanceIndex(string? updated)
        {
            ViewBag.Message = updated;
            var data =  await  genericRequests.GetHttpRequest("api/Preferance/get-all-preferance");
            return View("GetPreferenceIndex" , data);
        }
        public async Task<IActionResult> GetUpdatePreferanceIndex(PreferenceDTO preferance)
        {
            string image = preferance.PreferenceImage.Remove(preferance.PreferenceImage.Length - 4);
            preferance.PreferenceImage = image;
            return View("GetUpdatePreferance", preferance);
        }
        public async Task<IActionResult> PostsPreference(PreferenceDTO preference)
        {
            switch (preference.PreferenceImage)
            {
                case "Hız": preference.PreferenceImage = "Hız.png"; break;
                case "İletişim": preference.PreferenceImage = "İletişim.png"; break;
                case "Kadro": preference.PreferenceImage = "Kadro.png"; break;
                case "İşletme": preference.PreferenceImage = "İşletme.png"; break;
            }
            string posts = await  genericRequests.PostRequestGeneric("api/Preferance/post-preferance" , preference);
            return RedirectToAction("Index", "Preference", new { posts= posts });
        }
        public async Task<IActionResult> DeletePreference(int id)
        {
           string delete = await deleteRequest.DeleteRequestGeneric("api/Preferance/delete-preferance", id);
            return RedirectToAction("GetAllPreferanceIndex", "Preference" , new { updated = delete });
        }
        public async Task<IActionResult> UpdatePreference(PreferenceDTO preference)
        {
            switch (preference.PreferenceImage)
            {
                case "Hız": preference.PreferenceImage = "Hız.png"; break;
                case "İletişim": preference.PreferenceImage = "İletişim.png"; break;
                case "Kadro": preference.PreferenceImage = "Kadro.png"; break;
                case "İşletme": preference.PreferenceImage = "İşletme.png"; break;
            }
            string update = await  genericRequests.UpdateRequestGeneric("api/Preferance/update-preferance" , preference);
            return RedirectToAction("GetAllPreferanceIndex", "Preference", new { updated = update });
        }
    }
}
