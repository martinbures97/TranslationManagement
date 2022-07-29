namespace TranslationManagement.Application.Translator.Commands.UpdateTranslator;

public class UpdateTranslatorCommandValidator : AbstractValidator<UpdateTranslatorCommand>
{
    public UpdateTranslatorCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(TranslatorEntity.NameMaxLength);

        RuleFor(c => c.HourlyRate)
            .GreaterThanOrEqualTo(1);

        RuleFor(c => c.CreditCardNumber)
            .NotEmpty()
            .MaximumLength(TranslatorEntity.CreditCardNumberMaxLength)
            .CreditCard();
    }
}