using AutoMapper;
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
            CreateMap<Product, ProductModel>().
                ForMember(des => des.CategoryName, otp => otp.MapFrom(x => x.Category.CategoryName))
                .ReverseMap();
            CreateMap<CreateProductModel, Product>();
            
            CreateMap<Category, CategoryModel>();
        }
    }
}
