using AutoMapper;
using BankApplication.Domain.DTO;
using BankApplication.Domain.Entities;

namespace BankApplication.Domain.Profiles
{
    public class CreateLoanProfile : Profile
    {
        public CreateLoanProfile()
        {
            CreateMap<CreateLoanDTO, Loan>()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
