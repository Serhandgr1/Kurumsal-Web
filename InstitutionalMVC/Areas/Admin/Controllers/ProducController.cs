using AutoMapper;
using EntitiesLayer.ModelDTO;
using Humanizer;
using InstitutionalMVC.Helper;
using InstitutionalMVC.HttpRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.IdentityModel.Protocols.WSFederation.WSFederationConstants;

namespace InstitutionalMVC.Areas.Admin.Controllers
{
 
    public class ProducController : Controller
    {
        private readonly IMapper _mapper;
        private static int screenSize;
        private static string deger;
        private static int categorys;
        private static int pageNumber = 1;
        private static TokenDTO tokenDTO;
        UploadFiles uploadFiles = new UploadFiles();
        DeleteFiles deleteFiles = new DeleteFiles();
        GenericRequests<ProductDTO> genericRequests= new GenericRequests<ProductDTO>();
        DeleteRequest deleteRequest = new DeleteRequest();
        GetPaginationProduct paginationProduct  = new GetPaginationProduct();
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProducController(IMapper mapper , IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(string? posts)
        {

            ViewBag.Message = posts;
            return View();
        }
        public async Task<IActionResult> GetIndex(int? category, string? search, string? updated,int PageNumber = 1 )
        {
            if (search != "" && search != null)
            {
                deger = search.Trim();
                categorys = 0;

            }
            else if (category != 0 && category != null)
            {
                deger = "";
                categorys = (int)category;

            }
            else if (category == 0)
            {
                deger = "";
                categorys = 0;


            }
            ViewBag.PageNumber = PageNumber;
            ViewBag.screenSize = screenSize;
            ViewBag.Message = updated;
            List<ProductDTO> ProductApi = await paginationProduct.GetPagination("api/Product/page-product-list", PageNumber, screenSize, categorys, deger);
            var Category = GetCategory();
            ViewBag.Category = Category.Result;
            return View("GetProductIndex" , ProductApi);
        }
        public void UserScreenSize(int size)
        {
            screenSize = size;
        }
        public async Task<List<CategoryDTO>> GetCategory()
        {
            GenericRequests<CategoryDTO> genericRequestsCategory = new GenericRequests<CategoryDTO>();
            var data =  await genericRequestsCategory.GetHttpRequest("api/Categories/get-all-category");
            return data;
        }
        public async Task<IActionResult> GetUpdateIndex(ProductDTO productDTO)
        {
            //string image = productDTO.CategoryName.Remove(productDTO.CategoryName.Length - 4);
            //productDTO.CategoryName = image;
            var data = _mapper.Map<NewProductClient>(productDTO);
            ViewBag.ProductImage = productDTO.ProductImage;
            ViewBag.ProductImage2 = productDTO.ProductImage2;
            ViewBag.ProductImage3 = productDTO.ProductImage3;
            var category = GetCategory();
            foreach (var item in category.Result) 
            {
                if (item.Id == productDTO.CategoryId) 
                {
                    data.CategoryName = item.CategoryName;
                }
            }
            ViewBag.Category = category.Result;
            return View("GetUpdateProductIndex", data);

        }
        
        [HttpPost]
        public async Task<IActionResult> PostsProduct(NewProductClient newProduct) 
        {
            var data = await PostProductUploadFile(newProduct);
           string Posts = await genericRequests.PostRequestGeneric("api/Product/add-product", data);
            return RedirectToAction("Index", "Produc", new { posts = Posts });
        }
        public IActionResult Token(TokenDTO token) 
        {
            tokenDTO = token;
            return RedirectToAction("AdminIndex", "Admin");
        }
        public async Task<IActionResult> DeleteProduct(int id) 
        {
           string Delete = await deleteRequest.DeleteRequestGeneric("api/Product/delete-product", id);
            return RedirectToAction("GetIndex", "Produc", new { updated = Delete });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(NewProductClient newProduct)
        {

            var data = await MapProductImage(newProduct);
          string Update = await genericRequests.UpdateRequestGeneric("api/Product/update-product", data);
            return RedirectToAction("GetIndex", "Produc" , new { updated = Update });

        }
        public async Task<ProductDTO> MapProductImage(NewProductClient newProduct) 
        {
            ProductDTO ProductApi = await genericRequests.GetByIdGeneric("api/Product/get-product-by-id", (int)newProduct.Id);
            var Category = GetCategory();
            var productDto = _mapper.Map<ProductDTO>(newProduct);
            foreach (var item in Category.Result)
            {
                if (item.CategoryName == newProduct.CategoryName)
                {
                    productDto.CategoryId = item.Id;
                }
            }
            if (newProduct.ProductImage != null && newProduct.ProductImage.FileName != ProductApi.ProductImage)
            {
                deleteFiles.DeleteFile(_webHostEnvironment , ProductApi.ProductImage);
                string name = uploadFiles.UploadFile(newProduct.ProductImage);
                productDto.ProductImage = name;
            }
            else { productDto.ProductImage = ProductApi.ProductImage; }
            if (newProduct.ProductImage2 != null && newProduct.ProductImage2.FileName != ProductApi.ProductImage2)
            {
                deleteFiles.DeleteFile(_webHostEnvironment, ProductApi.ProductImage2);
                string name = uploadFiles.UploadFile(newProduct.ProductImage2);
                productDto.ProductImage2 = name;
            }
            else { productDto.ProductImage2 = ProductApi.ProductImage2; }
            if (newProduct.ProductImage3 != null && newProduct.ProductImage3.FileName != ProductApi.ProductImage3)
            {
                deleteFiles.DeleteFile(_webHostEnvironment, ProductApi.ProductImage3);
                string name = uploadFiles.UploadFile(newProduct.ProductImage3);
                productDto.ProductImage3 = name;
            }
            else { productDto.ProductImage3 = ProductApi.ProductImage3; }
            return productDto;
        }
        public async Task<ProductDTO> PostProductUploadFile(NewProductClient newProduct) 
        {
            ProductDTO productDTO = _mapper.Map<ProductDTO>(newProduct);
            if (newProduct.ProductImage != null)
            {
                string name = uploadFiles.UploadFile(newProduct.ProductImage);
                productDTO.ProductImage = name;
            }
            if (newProduct.ProductImage2 != null)
            {
                string name = uploadFiles.UploadFile(newProduct.ProductImage2);
                productDTO.ProductImage2 = name;
            }
            if (newProduct.ProductImage3 != null)
            {
                string name = uploadFiles.UploadFile(newProduct.ProductImage3);
                productDTO.ProductImage3 = name;
            }
            return productDTO;
        }
    }
}
