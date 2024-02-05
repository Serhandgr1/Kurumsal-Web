using EntitiesLayer.ModelDTO;

namespace InstitutionalMVC.Helper
{
    public class UploadFiles
    {
        //var extension = Path.GetExtension(newProduct.ProductImage.FileName);
        public string UploadFile(IFormFile ImageName)
        {
            var extension = Path.GetExtension(ImageName.FileName);
            var newimagename = Guid.NewGuid() + extension;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/", newimagename);
            var stream = new FileStream(location, FileMode.Create);
            ImageName.CopyTo(stream);
            return newimagename;
        }
        //public Stream GenerateStreamFromString(string s, IFormFile ImageName)
        //{
        //    MemoryStream stream = new MemoryStream();
        //    StreamWriter writer = new StreamWriter(stream);
        //    writer.Write(s);
        //    writer.Flush();
        //    stream.Position = 0;
        //    ImageName.CopyTo(stream);
        //    return stream;
        //}
    }
}
