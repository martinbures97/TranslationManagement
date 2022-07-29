namespace TranslationManagement.Application.TranslationJob.Commands.DeleteTranslationJob;

public class DeleteTranslationJobCommandValidator : AbstractValidator<DeleteTranslationJobCommand>
{
    public DeleteTranslationJobCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
    }
}