using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using InstitutionalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace InstitutionalMVC.Controllers
{
	public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
        //GenericRequests<ContactAdminDTO> request = new GenericRequests<ContactAdminDTO>();
        public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
    
        public async Task<IActionResult> Index()
        {

            GetRequest< CommentDTO > command= new GetRequest< CommentDTO >();
            List<CommentDTO> CommendApi = await command.GetHttpRequest("api/Commend/get-all-commend");
            GetRequest<ProjectDTO> project = new GetRequest<ProjectDTO>();
            List<ProjectDTO> ProjectApi = await project.GetHttpRequest("api/Project/get-all-project");

            GetRequest<PreferenceDTO> preferance = new GetRequest<PreferenceDTO>();
            List<PreferenceDTO> PreferanceApi = await preferance.GetHttpRequest("api/Preferance/get-all-preferance");

  
            return View("Index" , Tuple.Create(ProjectApi, CommendApi, PreferanceApi));

		}
        //public async Task<IActionResult> FooterView()
        //{
        //    var data = request.GetHttpRequest("api/ContactAdmin/get-all-contact-admin");
        //    return PartialView("_footerview", data.Result);
        //}
    }
}