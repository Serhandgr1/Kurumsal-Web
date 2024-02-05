using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class GetRequest<T>
    {
        public async Task<List<T>> GetHttpRequest(string url , string? search="" , int? category=0) 
        {
            string ProductCategoryUrl = Extancion.Client.BaseAddress + url;
            HttpResponseMessage ProductCategoryResponce = Extancion.Client.GetAsync($"{ProductCategoryUrl}").Result;
            if (category != null && category != 0)
            {
                ProductCategoryResponce = Extancion.Client.GetAsync($"{ProductCategoryUrl}{category}").Result;
            }
            else if (search != null && search != "") 
            {
                ProductCategoryResponce = Extancion.Client.GetAsync($"{ProductCategoryUrl}{search}").Result;
            }
             
            List<T> ProductCategoryApi = await ProductCategoryResponce.Content.ReadFromJsonAsync<List<T>>();
            return ProductCategoryApi;
        }
    }
}
