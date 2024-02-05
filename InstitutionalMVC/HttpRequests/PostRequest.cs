using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class PostRequest<T>
    {
        public async Task PostRequestGeneric(string Url, T entity) 
        {
            string url = Extancion.Client.BaseAddress + Url;
            var data = await Extancion.Client.PostAsJsonAsync(url, entity);
        }
    }
}
