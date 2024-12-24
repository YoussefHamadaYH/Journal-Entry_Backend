using AutoMapper;
using JournyTask.DTOs;
using JournyTask.Models;

namespace JournyTask.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Mapping for AccountsChart and AccountsChartDTO
            CreateMap<AccountsChart, AccountsChartDTO>()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ArabicName, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.EnglishName, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.AllowEntry, opt => opt.MapFrom(src => src.AllowEntry))
                .ReverseMap();

            // Mapping for JournalDetail and JournalDetailDTO
            CreateMap<JournalDetail, JournalDetailDTO>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.NameEn))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.Number))
                .ReverseMap();

            // Mapping for JournalHeader and JournalHeaderDTO
            CreateMap<JournalHeader, JournalHeaderDTO>()
                .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => src.EntryDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.JournalDetails, opt => opt.MapFrom(src => src.JournalDetails))
                .ReverseMap()
                .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => src.EntryDate.Date)); // Explicit type conversion

            CreateMap<JournalHeader, JournalHeaderDTO>().ReverseMap();
            CreateMap<JournalDetail, JournalDetailDTO>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.NameEn))
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.Number));

        }
    }
}
