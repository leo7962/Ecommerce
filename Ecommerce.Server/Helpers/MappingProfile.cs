using AutoMapper;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;

namespace Ecommerce.Server.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ForMember(dto => dto.Categories, opt => opt.MapFrom(src => src.CategoryProducts.Select(cp => cp.Category)));
            CreateMap<ProductDTO, Product>();
            CreateMap<Category, CategoryDTO>().ForMember(dto => dto.Products, opt => opt.MapFrom(src => src.categoryProducts.Select(cp => cp.Product)));
            CreateMap<CategoryDTO, Category>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
            CreateMap<CategoryProduct, CategoryProductDTO>().ForMember(dto => dto.IdProduct, opt => opt.MapFrom(src => src.IdProduct))
                .ForMember(dto => dto.IdCategory, opt => opt.MapFrom(src => src.IdCategory));
        }
    }
}
