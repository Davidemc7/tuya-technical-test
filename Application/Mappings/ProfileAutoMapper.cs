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
        }

        private void ConfigureCustomer()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
