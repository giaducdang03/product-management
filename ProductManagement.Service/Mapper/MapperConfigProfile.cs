using AutoMapper;
using ProductManagement.Repository.Commons;
using ProductManagement.Repository.Models;
using ProductManagement.Service.BussinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.Mapper
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.CategoryName, otp => otp.MapFrom(x => x.Category.CategoryName))
                .ForMember(dest => dest.Status, otp => otp.MapFrom(x => x.Status.ToString()));
            CreateMap<CreateProductModel, Product>();
            
            CreateMap<Category, CategoryModel>();
        }
    }
}
