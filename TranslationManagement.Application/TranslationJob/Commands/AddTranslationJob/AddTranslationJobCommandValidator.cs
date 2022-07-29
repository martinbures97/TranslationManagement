namespace TranslationManagement.Application.TranslationJob.Commands.AddTranslationJob;

public class AddTranslationJobCommandValidator : AbstractValidator<AddTranslationJobCommand>
{
    public AddTranslationJobCommandValidator()
    {
        RuleFor(c => c.CustomerName)
            .NotEmpty()
            .MaximumLength(TranslationJobEntity.CustomerNameMaxLength);
        
        RuleFor(c => c.OriginalContent)
            .NotEmpty()
            .MaximumLength(TranslationJobEntity.OriginalContentMaxLength);
    }
}