using InstitutionalMVC.Helper;

namespace InstitutionalMVC.HttpRequests
{
    public class DeleteRequest
    {
        public async Task<string> DeleteRequestGeneric(string url ,int id ) 
        {
            string urlDelete = Extancion.Client.BaseAddress + url;
            var data = Extancion.Client.DeleteAsync($"{urlDelete}?id={id}").Result;
            return "Başarılı";
        }
    }
}
