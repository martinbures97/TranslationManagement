namespace TranslationManagement.Application.Translator.Commands.AddTranslator;

public class AddTranslatorCommandValidator : AbstractValidator<AddTranslatorCommand>
{
    public AddTranslatorCommandValidator()
    {
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