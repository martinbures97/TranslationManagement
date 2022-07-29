namespace TranslationManagement.Application.TranslationJob.Commands.UpdateTranslationJobCommand;

public class UpdateTranslationJobCommandValidator : AbstractValidator<UpdateTranslationJobCommand>
{
    public UpdateTranslationJobCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();

        RuleFor(c => c.OriginalContent)
            .NotEmpty()
            .MaximumLength(TranslationJobEntity.OriginalContentMaxLength);
    }
}