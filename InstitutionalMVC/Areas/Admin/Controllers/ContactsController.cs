using AutoMapper;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class ContactsController : Controller
    {
        GenericRequests<ContactDTO> genericRequests = new GenericRequests<ContactDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        public IActionResult Index()
        {
            return View("ContactIndex");
        }
        public IActionResult GetAllContactIndex()
        {
            var data = genericRequests.GetHttpRequest("api/ContactApi/get-all-contact");
            return View("GetContactIndex" , data.Result);
        }
        public async Task<IActionResult> DeleteContact(int id)
        {
           await deleteRequest.DeleteRequestGeneric("api/ContactApi/delete-contact", id);
            return RedirectToAction("GetAllContactIndex", "Contacts");
        }
    }
} 