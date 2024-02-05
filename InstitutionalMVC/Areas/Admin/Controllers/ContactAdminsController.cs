using EntitiesLayer.ModelDTO;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class ContactAdminsController : Controller
    {
        GenericRequests<ContactAdminDTO> genericRequests = new GenericRequests<ContactAdminDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        public IActionResult Index(string? posts)
        {
            ViewBag.Message = posts;
            return View("ContactAdminsIndex");
        }
        public  async Task <IActionResult> GetUpdateContactAdmin(ContactAdminDTO contactAdmin) 
        {
            return View("GetUpdateContactAdmin" , contactAdmin);
        }
        public  async Task<IActionResult> GetAllAdminContact(string? updated) 
        {
            ViewBag.Message = updated;
            var data = genericRequests.GetHttpRequest("api/ContactAdmin/get-all-contact-admin");
            return View("GetAllAdminContactIndex" , data.Result);
        }
        [HttpPost]
        public async Task<IActionResult> PostContactAdmin(ContactAdminDTO contactAdmin)
        {
            var data = genericRequests.PostRequestGeneric("api/ContactAdmin/create-admin-contact", contactAdmin);
            return RedirectToAction("Index", "ContactAdmins" , new { posts = data});
        }
        public async Task<IActionResult> UpdateContactAdmin(ContactAdminDTO contactAdmin) 
        {
            var data =  genericRequests.UpdateRequestGeneric("api/ContactAdmin/update-contact-admin", contactAdmin);
            return RedirectToAction("GetAllAdminContact", "ContactAdmins", new { updated = data });
        }
        public  async Task<IActionResult> DeleteAdminContact(int id) 
        {
           var data = deleteRequest.DeleteRequestGeneric("api/ContactAdmin/delete-contact-admin", id);
            return RedirectToAction("GetAllAdminContact", "ContactAdmins", new {updated=data });
        }
    }
}
