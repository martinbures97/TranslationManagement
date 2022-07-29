using AutoMapper;
using TranslationManagement.Api.Models;
using TranslationManagement.Application.TranslationJob.Commands.AddTranslationJob;

namespace TranslationManagement.Api.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TranslationModel, AddTranslationJobCommand>()
            .ForMember(d => d.OriginalContent,
                s =>
                    s.MapFrom(s => s.Content))
            .ForMember(d => d.CustomerName,
                s =>
                    s.MapFrom(s => s.Customer));
    }
}