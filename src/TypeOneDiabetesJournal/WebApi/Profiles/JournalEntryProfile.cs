using AutoMapper;
using Core.Models;
using DataLayer.Data;
using WebApi.Features.JournalSearch;

namespace WebApi.Profiles
{
    public class JournalEntryProfile : Profile
    {
        public JournalEntryProfile()
        {
            this.CreateMap<Journalentry, JournalEntrySummary>()
                .IncludeMembers(src => src.JournalEntryTags, src => src.JournalEntryNutritionalInfo)
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Title))
                .ForMember(dest => dest.CarbCount, src => src.MapFrom(x => x.JournalEntryNutritionalInfo));

            this.CreateMap<Journalentrynutritionalinfo, JournalEntrySummary>()
                .IncludeMembers(src => src.Nutritionalinfo)
                .ForMember(dest => dest.CarbCount, obj => obj.MapFrom(src => src.Nutritionalinfo));

            this.CreateMap<Nutritionalinfo, JournalEntrySummary>()
                .ForMember(dest => dest.CarbCount, obj => obj.MapFrom(src => src.Carbohydrates));

            this.CreateMap<Journalentrytag, string>()
                .IncludeMembers(src => src.Tag)
                .ForMember(dest => dest, obj => obj.MapFrom(src => src.Tag));

            this.CreateMap<Tag, string>()
                .ForMember(dest => dest, obj => obj.MapFrom(src => src.Description));
        }
    }
}
