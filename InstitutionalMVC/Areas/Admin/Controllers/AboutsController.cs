using AutoMapper;
using EntitiesLayer.ModelDTO;
using System.IO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Hosting;
using Humanizer;
using Elfie.Serialization;
using NuGet.ContentModel;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class AboutsController : Controller
    {

        UploadFiles uploadFiles = new UploadFiles();
        DeleteFiles deleteFiles = new DeleteFiles();
        GenericRequests<AboutDTO> genericRequests = new GenericRequests<AboutDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public AboutsController(IMapper mapper , IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
       
        public IActionResult Index(string? posts)
        {
            ViewBag.Message = posts;
            return View("AbouteIndex");
        }
        public async Task<IActionResult> GetAllIndex(string? updated)
        {
            ViewBag.Message = updated;
            var Aboute = genericRequests.GetHttpRequest("api/Aboute/get-all-about");
            return View("GetAboutIndex" , Aboute.Result);
        }
        public async Task<IActionResult> GetUpdateAboutIndex(AboutDTO about)
        {
            var data = _mapper.Map<NewAboutClient>(about);
            ViewBag.Image = about.Image;
            ViewBag.Image2 = about.Image2;
            ViewBag.Image3 = about.Image3;
            return View("GetUpdateAbout", data);
        }
       
        [HttpPost]
        public async Task<IActionResult> PostsAboute(NewAboutClient newAbout)
        {

            var aboutDTO =  _mapper.Map<AboutDTO>(newAbout);
            if (newAbout.Image != null)
            {
          
                string name = uploadFiles.UploadFile(newAbout.Image);
                aboutDTO.Image = name;
            }
            if (newAbout.Image2 != null)
            {
                string name = uploadFiles.UploadFile(newAbout.Image2);
                aboutDTO.Image2 = name;
            }
            if (newAbout.Image3 != null)
            {
                string name = uploadFiles.UploadFile(newAbout.Image3);
                aboutDTO.Image3 = name;
            }
           string posts = await genericRequests.PostRequestGeneric("api/Aboute/post-aboute", aboutDTO);
            return RedirectToAction("Index", "Abouts" , new { posts = posts });
        }
        public async Task<IActionResult> DeleteAboute(int id)
        {
           string delete = await deleteRequest.DeleteRequestGeneric("api/Aboute/delete-aboute", id);
            return RedirectToAction("GetAllIndex", "Abouts", new { updated = delete });
        }
        public async Task<IActionResult> UpdateAboute(NewAboutClient newAbout)
        {

            var data =await  genericRequests.GetByIdGeneric("api/Aboute/get-by-id-about", newAbout.Id);
            var aboutDTO = _mapper.Map<AboutDTO>(newAbout);
            if (newAbout.Image != null && newAbout.Image.FileName != data.Image)
            {
                deleteFiles.DeleteFile(_webHostEnvironment, data.Image);
                string name = uploadFiles.UploadFile(newAbout.Image);
                aboutDTO.Image = name;
            }
            else { aboutDTO.Image = data.Image; }
            if (newAbout.Image2 != null && newAbout.Image2.FileName != data.Image2)
            {
                deleteFiles.DeleteFile(_webHostEnvironment, data.Image2);
                string name = uploadFiles.UploadFile(newAbout.Image2);
                aboutDTO.Image2 = name;
            }else
            { aboutDTO.Image2 = data.Image2; }
            if (newAbout.Image3 != null && newAbout.Image3.FileName != data.Image3)
            {
                deleteFiles.DeleteFile(_webHostEnvironment, data.Image3);
                string name = uploadFiles.UploadFile(newAbout.Image3);
                aboutDTO.Image3 = name;
            }else
            { aboutDTO.Image3 = data.Image3; }
            string update = await genericRequests.UpdateRequestGeneric("api/Aboute/update-aboute", aboutDTO);
            return RedirectToAction("GetAllIndex", "Abouts", new { updated = update });

        }
    }
}
