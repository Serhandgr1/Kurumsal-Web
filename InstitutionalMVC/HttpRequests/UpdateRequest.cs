using EntitiesLayer.ModelDTO;
using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class UpdateRequest<T>
    {
        public async Task UpdateRequestGeneric(string url , T entity) 
        {
            string urlUpdate = Extancion.Client.BaseAddress + url;
            var data = await Extancion.Client.PutAsJsonAsync(urlUpdate, entity);
        }
    }
}
