using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            ConfigureCustomer();
            ConfigureOrder();
        }

        private void ConfigureCustomer()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }

        private void ConfigureOrder()
        {
            CreateMap<Order, OrderDTO>()
                .AfterMap((t, dto) =>
                {
                    dto.Customer = new CustomerDTO
                    {
                        CustomerId = t.Customer.CustomerId,
                        GivenName = t.Customer.GivenName,
                        FamilyName = t.Customer.FamilyName,
                        Email = t.Customer.Email,
                        Phone = t.Customer.Phone,
                        Address = t.Customer.Address
                    };
                });
            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.Customer, opt => opt.Ignore());
        }
    }
}
