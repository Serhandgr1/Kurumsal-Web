using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        GenericRequests<CommentDTO> genericRequests = new GenericRequests<CommentDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        public IActionResult Index(string? posts)
        {
            ViewBag.Message = posts;
         
             return View();
        }
        public async Task<IActionResult> GetAllCommendIndex(string? updated)
        {
            ViewBag.Message = updated;
            var data = await genericRequests.GetHttpRequest("api/Commend/get-all-commend");
            return View("GetCommentIndex" , data);
        }
        public async Task<IActionResult> GetUpdateIndex(CommentDTO comment) 
        {
            string image=comment.CommentImage.Remove(comment.CommentImage.Length-4);
            comment.CommentImage = image;
            return View("UpdateCommentIndex" , comment);
        }
        public async Task<IActionResult> PostsComment(CommentDTO comment)
        {
            switch (comment.CommentImage) 
            {
                case "Kadın": comment.CommentImage = "Kadın.jpg"; break;
                case "Erkek": comment.CommentImage = "Erkek.jpg"; break;
            }
            
           string posts =  await genericRequests.PostRequestGeneric("api/Commend/post-commend" , comment);
            return RedirectToAction("Index", "Comments" , new { posts  = posts });
        }
        public async Task<IActionResult> DeleteComment(int id)
        {
            string delete =  await  deleteRequest.DeleteRequestGeneric("api/Commend/delete-commend", id);
            return RedirectToAction("GetAllCommendIndex", "Comments", new { updated  = delete });
        }
        public async Task<IActionResult> UpdateComment(CommentDTO comment)
        {
            switch (comment.CommentImage)
            {
                case "Kadın": comment.CommentImage = "Kadın.jpg"; break;
                case "Erkek": comment.CommentImage = "Erkek.jpg"; break;
            }
            var update = await  genericRequests.UpdateRequestGeneric("api/Commend/update-commend" , comment);
            return RedirectToAction("GetAllCommendIndex", "Comments", new { updated = update });

        }
    }
}