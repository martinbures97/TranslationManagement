using TranslationManagement.Application.TranslationJob.Commands.AddTranslationJob;
using TranslationManagement.Application.TranslationJob.Commands.UpdateTranslationJobCommand;
using TranslationManagement.Application.TranslationJob.Queries;
using TranslationManagement.Application.Translator.Commands.AddTranslator;
using TranslationManagement.Application.Translator.Commands.UpdateTranslator;
using TranslationManagement.Application.Translator.Queries;

namespace TranslationManagement.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddTranslatorCommand, TranslatorEntity>();
        CreateMap<UpdateTranslatorCommand, TranslatorEntity>();
        CreateMap<TranslatorEntity, TranslatorDto>()
            .ForMember(d => d.JobIds, s => 
                s.MapFrom(s => s.Jobs.Select(j => j.Id)));

        CreateMap<AddTranslationJobCommand, TranslationJobEntity>();
        CreateMap<UpdateTranslationJobCommand, TranslationJobEntity>();
        CreateMap<TranslationJobEntity, TranslationJobDto>();
    }
}