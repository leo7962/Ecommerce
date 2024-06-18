using AutoMapper;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;

namespace Ecommerce.Server.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ForMember(dto => dto.IdCategories, opt => opt.MapFrom(src => src.CategoryProducts.Select(cp => cp.IdCategory)));
            CreateMap<ProductDTO, Product>();
            CreateMap<Category, CategoryDTO>().ForMember(dto => dto.IdProducts, opt => opt.MapFrom(src => src.categoryProducts.Select(cp => cp.IdProduct)));
            CreateMap<CategoryDTO, Category>();
            CreateMap<Customer, CustomerDTO>().ForMember(dto => dto.IdOrders, opt => opt.MapFrom(src => src.Orders.Select(o => o.Id).ToList()));
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>().ForMember(dto => dto.DetailOrders, opt => opt.MapFrom(src => src.detailOrders));
            CreateMap<DetailOrder, DetailOrderDTO>();
            CreateMap<Product, CategoryDTO>().ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id)).ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
