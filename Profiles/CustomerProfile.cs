using AutoMapper;
using LibApp.Dtos;
using LibApp.Models;
using LibApp.ViewModels;

namespace LibApp.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<RegisterViewModel, Customer>();
        }
    }
}