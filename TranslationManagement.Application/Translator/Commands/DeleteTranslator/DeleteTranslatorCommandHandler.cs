namespace TranslationManagement.Application.Translator.Commands.DeleteTranslator;

public class DeleteTranslatorCommandHandler : AbstractValidator<DeleteTranslatorCommand>
{
    public DeleteTranslatorCommandHandler()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
    }
}