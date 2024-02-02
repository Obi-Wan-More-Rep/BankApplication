using AutoMapper;
using BankApplication.Domain.DTO;
using BankApplication.Domain.Entities;

namespace BankApplication.Domain.Profiles
{
    public class AccountCreationProfile : Profile
    {
        public AccountCreationProfile()
        {
            CreateMap<AccountCreationDTO, Account>()
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.AccountTypesId, opt => opt.MapFrom(src => src.AccountTypesId));
        }
    }
}
