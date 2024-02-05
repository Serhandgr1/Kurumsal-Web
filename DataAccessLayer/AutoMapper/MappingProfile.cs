using AutoMapper;
using EntitiesLayer.Entities;
using EntitiesLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
namespace DataAccessLayer.AutoMapper
{
    public class MappingProfile:Profile
    {
      
        public MappingProfile()
        {
            //ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CreateMap<ProductDTO, NewProductClient>().ForMember(des=>des.ProductImage,opt=>opt.Ignore()).ForMember(des => des.ProductImage2, opt => opt.Ignore()).ForMember(des => des.ProductImage3, opt => opt.Ignore());
            CreateMap<NewProductClient, ProductDTO>().ForMember(des => des.ProductImage, opt => opt.Ignore()).ForMember(des => des.ProductImage2, opt => opt.Ignore()).ForMember(des => des.ProductImage3, opt => opt.Ignore());
            CreateMap<NewAboutClient, AboutDTO>().ForMember(des => des.Image, opt => opt.Ignore()).ForMember(des => des.Image2, opt => opt.Ignore()).ForMember(des => des.Image3, opt => opt.Ignore());
            CreateMap<AboutDTO, NewAboutClient>().ForMember(des => des.Image, opt => opt.Ignore()).ForMember(des => des.Image2, opt => opt.Ignore()).ForMember(des => des.Image3, opt => opt.Ignore());
            CreateMap<ProjectDTO, NewProjeClient>().ForMember(des => des.ProjectImage, opt => opt.Ignore());
            CreateMap<NewProjeClient, ProjectDTO>().ForMember(des => des.ProjectImage, opt => opt.Ignore());
            CreateMap<ReferangeDTO, NewReferanceClient>().ForMember(des => des.ReferangeImage, opt => opt.Ignore());
            CreateMap<NewReferanceClient, ReferangeDTO>().ForMember(des => des.ReferangeImage, opt => opt.Ignore());
            CreateMap<UserEntites, UserDTO>();
            CreateMap<AboutEntites, AboutDTO>();
            CreateMap<AboutDTO, AboutEntites>();
            CreateMap<ContactAdminDTO , ContactAdminEntities>();
            CreateMap<ContactAdminEntities, ContactAdminDTO>();
            CreateMap<CategoryDTO, CategoryEntities>();
            CreateMap<CategoryEntities, CategoryDTO>();
            CreateMap<ContactEntities, ContactDTO>();
            CreateMap<ContactDTO, ContactEntities>();
            CreateMap<ReferanceEntities, ReferangeDTO>();
            CreateMap<ReferangeDTO, ReferanceEntities>();
            CreateMap<ProductEntities, ProductDTO>();
            CreateMap<ProductDTO, ProductEntities>();
            CreateMap<UserDTO, UserEntites>();
            CreateMap<ProjectDTO, ProjectEntities>();
            CreateMap<ProjectEntities, ProjectDTO>();
            CreateMap<PreferenceDTO, PreferenceEntiites>();
            CreateMap<PreferenceEntiites, PreferenceDTO>();
            CreateMap<ServicesDTO, ServicesEntities>();
            CreateMap<ServicesEntities, ServicesDTO>();
            CreateMap<CommentDTO, CommentEntities>();
            CreateMap<CommentEntities, CommentDTO>();
            CreateMap<UserForRegistrationDTO, UserEntites>();
            CreateMap<ContactAdminEntities, ContactAdminDTO>();
            CreateMap<ContactAdminDTO, ContactAdminEntities>();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly) 
        {
            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(i=>i.IsGenericType && i.GetGenericTypeDefinition()==typeof(IMapFrom<>))).ToList();
            foreach (var type in types) 
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }

    }
}
