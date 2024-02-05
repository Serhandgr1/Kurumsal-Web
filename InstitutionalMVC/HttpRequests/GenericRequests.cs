using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class GenericRequests<T>
    {
        public async Task<List<T>> GetHttpRequest(string url, string? search = "", int? category = 0)
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
        public async Task<string> PostRequestGeneric(string Url, T entity)
        {
            string url = Extancion.Client.BaseAddress + Url;
            var data = await Extancion.Client.PostAsJsonAsync(url, entity);
            return "Başarılı";
        }
        public async Task<string> UpdateRequestGeneric(string url, T entity)
        {
            string urlUpdate = Extancion.Client.BaseAddress + url;
            var data = await Extancion.Client.PutAsJsonAsync(urlUpdate, entity);
            return "UpdateBaşarılı";
        }
        public  async Task<T> GetByIdGeneric(string url , int id)
        {
            string urlReuest = Extancion.Client.BaseAddress + url;
            HttpResponseMessage responce = Extancion.Client.GetAsync($"{urlReuest}?id={id}").Result;
            T dto = await responce.Content.ReadFromJsonAsync<T>();
            return dto;
        }
    }
}
