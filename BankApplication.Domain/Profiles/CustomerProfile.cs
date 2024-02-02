using AutoMapper;
using BankApplication.Domain.DTO;
using BankApplication.Domain.Entities;

namespace BankApplication.Domain.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Givenname, opt => opt.MapFrom(src => src.Givenname))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Streetaddress, opt => opt.MapFrom(src => src.Streetaddress))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Zipcode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Telephonecountrycode, opt => opt.MapFrom(src => src.Telephonecountrycode))
                .ForMember(dest => dest.Telephonenumber, opt => opt.MapFrom(src => src.Telephonenumber))
                .ForMember(dest => dest.Emailaddress, opt => opt.MapFrom(src => src.Emailaddress))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
