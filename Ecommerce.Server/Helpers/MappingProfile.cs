using AutoMapper;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;

namespace Ecommerce.Server.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Mapping of Product
        CreateMap<Product, ProductDTO>()            
            .ForMember(dto => dto.IdProduct, opt => opt.MapFrom(src => src.IdProduct));

        //Mapping of Category
        CreateMap<Category, CategoryDTO>();           

        //Mapping order
        CreateMap<Order, OrderDTO>()         
            .ForMember(dto => dto.Products, opt => opt.MapFrom(src => src.OrderProducts.Select(op => new {op.IdProduct, op.Quantity})));

        //Mapping order to orderProduct
        CreateMap<OrderProduct, OrderProductDTO>()
            .ForMember(dto => dto.IdProduct, opt => opt.MapFrom(src => src.IdProduct))
            .ForMember(dto => dto.Quantity, opt => opt.MapFrom(src => src.Quantity));

        //Mapping User

        CreateMap<User, UserDTO>()            
            .ForMember(dto => dto.IdOrders, opt => opt.MapFrom(src => src.Orders.Select(o => o.IdOrder)));
    }
}