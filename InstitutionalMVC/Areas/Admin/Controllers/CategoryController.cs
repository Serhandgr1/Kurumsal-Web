using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        GenericRequests<CategoryDTO> genericRequests = new GenericRequests<CategoryDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        public IActionResult Index(string? posts)
        {
            ViewBag.Message = posts;
            return View("CategoryIndex");
        }
        public async Task<IActionResult> GetAllCategory(string? updated)
        {
            ViewBag.Message = updated;
            var data = await genericRequests.GetHttpRequest("api/Categories/get-all-category");
            return View("GetCategoryIndex" , data);
        }
        public async Task<IActionResult> GetUpdateCategoryIndex(CategoryDTO categoryDTO)
        {
            return View("UpdateCategoryIndex", categoryDTO);
        }
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            var deger = await genericRequests.UpdateRequestGeneric("api/Categories/update-category", categoryDTO);
            return RedirectToAction("GetAllCategory", "Category", new { updated = deger});
        }
        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryDTO category)
        {
            string deger=  await genericRequests.PostRequestGeneric("api/Categories/post-category", category);
                // post category
            return RedirectToAction("Index" , "Category", new {posts= deger });
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            string deger = await deleteRequest.DeleteRequestGeneric("api/Categories/delete-category" , id);

            if (deger == "Başarısız")
            {
                return RedirectToAction("GetAllCategory", "Category", new { updated = "true" });
            }
            else 
            {// id li categoriyi sil
                return RedirectToAction("GetAllCategory", "Category", new { updated = "Başarılı" });
            }
        }
    }
}
