using AutoMapper;
using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class ReferenceController : Controller
    {
        UploadFiles uploadFiles = new UploadFiles();
        GenericRequests<ReferangeDTO> genericRequests = new GenericRequests<ReferangeDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        DeleteFiles deleteFiles = new DeleteFiles();
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReferenceController(IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string? posts)
        {
            ViewBag.Message = posts;
            return View("ReferanceIndex");
        }
        public async Task<IActionResult> GetAllReferanceIndex(string? updated)
        {
            ViewBag.Message = updated;
            var data =await  genericRequests.GetHttpRequest("api/Referance/get-all-referance");
            return View("GetReferenceIndex" , data);
        }
        public async Task<IActionResult> GetUpdateReferance(ReferangeDTO referange)
        {
            var data = _mapper.Map<NewReferanceClient>(referange);
            ViewBag.Image = referange.ReferangeImage;
            return View("GetUpdateReferance", data);
        }
        [HttpPost]
        public async Task<IActionResult> PostsReferance(NewReferanceClient newReferance)
        {
            var data =  _mapper.Map<ReferangeDTO>(newReferance);
            if (newReferance.ReferangeImage != null)
            {
                string name = uploadFiles.UploadFile(newReferance.ReferangeImage);
                data.ReferangeImage = name;
            }
            string Posts = await genericRequests.PostRequestGeneric("api/Referance/post-referance", data);
            return RedirectToAction("Index", "Reference", new { posts = Posts });
        }
        public async Task<IActionResult> DeleteReferance(int id)
        {
           string delete = await deleteRequest.DeleteRequestGeneric("api/Referance/delete-referance" , id);
            return RedirectToAction("GetAllReferanceIndex", "Reference", new { updated = delete });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateReferance(NewReferanceClient newReferance)
        { 
            ReferangeDTO ReferanceApi = await genericRequests.GetByIdGeneric("api/Referance/get-referance-by-id", (int)newReferance.Id);
            var dto= _mapper.Map<ReferangeDTO>(newReferance);
            if (newReferance.ReferangeImage != null && newReferance.ReferangeImage.FileName != ReferanceApi.ReferangeImage)
            {
                deleteFiles.DeleteFile(_webHostEnvironment, ReferanceApi.ReferangeImage);
                string name = uploadFiles.UploadFile(newReferance.ReferangeImage);
                dto.ReferangeImage = name;
            }else { dto.ReferangeImage = ReferanceApi.ReferangeImage; }
            string update = await genericRequests.UpdateRequestGeneric("api/Referance/update-referance", dto);
            return RedirectToAction("GetAllReferanceIndex", "Reference", new { updated = update });

        }
    }
}
