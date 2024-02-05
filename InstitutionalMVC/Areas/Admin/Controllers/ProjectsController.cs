using AutoMapper;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class ProjectsController : Controller
    {
        private static int screenSize;
        private static int pageNumber;
        UploadFiles uploadFiles = new UploadFiles();
        DeleteFiles deleteFiles = new DeleteFiles();
        GenericRequests<ProjectDTO> genericRequests = new GenericRequests<ProjectDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProjectsController(IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string? posts)
        {
            ViewBag.Message = posts;
            return View("ProjectIndex");
        }
        public async Task<IActionResult> GetAllProjectIndex(string? updated, int PageNumber = 1)
        {

            ViewBag.Message = updated;
            pageNumber =PageNumber;
            ViewBag.PageNumber = PageNumber;
            ViewBag.screenSize = screenSize;
            string ProjectUrl = Extancion.Client.BaseAddress + "api/Project/page-project-list";
            HttpResponseMessage ProductResponce = Extancion.Client.GetAsync($"{ProjectUrl}?PageNumber={pageNumber}&PageSize={screenSize}").Result;
            List<ProjectDTO> ProjectApi = await ProductResponce.Content.ReadFromJsonAsync<List<ProjectDTO>>();
            return View("GetProjectIndex" , ProjectApi);
        }

     
        public async Task<IActionResult> GetUpdateProjeIndex(ProjectDTO project)
        {
            var data = _mapper.Map<NewProjeClient>(project);
            ViewBag.Image = project.ProjectImage;
            return View("GetUpdateProje", data);
        }
        [HttpPost]
        public async Task<IActionResult> PostsProject(NewProjeClient newProje)
        {

           var project =  _mapper.Map<ProjectDTO>(newProje);
            if (newProje.ProjectImage != null)
            {
                string name = uploadFiles.UploadFile(newProje.ProjectImage);
                project.ProjectImage = name;
            }
            string Posts = await genericRequests.PostRequestGeneric("api/Project/post-project", project);
            return RedirectToAction("Index", "Projects", new { posts = Posts });
        }
        public async Task<IActionResult> DeleteProject(int id)
        {
            string  delete = await deleteRequest.DeleteRequestGeneric("api/Project/delete-project", id);
            return RedirectToAction("GetAllProjectIndex", "Projects", new { updated = delete });
        }
        public void UserScreenSize(int size)
        {
            screenSize = size;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(NewProjeClient newProje)
        {
            
            ProjectDTO ProjectApi = await genericRequests.GetByIdGeneric("api/Project/get-project-by-id", (int)newProje.Id);
            var project = _mapper.Map<ProjectDTO>(newProje);
            if (newProje.ProjectImage != null && newProje.ProjectImage.FileName != ProjectApi.ProjectImage)
            {
                deleteFiles.DeleteFile(_webHostEnvironment , ProjectApi.ProjectImage);
                string name = uploadFiles.UploadFile(newProje.ProjectImage);
                project.ProjectImage = name;
            }
            else { project.ProjectImage = ProjectApi.ProjectImage; }
            string update = await genericRequests.UpdateRequestGeneric("api/Project/update-project", project);
            return RedirectToAction("GetAllProjectIndex", "Projects", new { updated = update });

        }
    }
}
