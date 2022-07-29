namespace TranslationManagement.Application.Common.Exceptions;

public class NotFoundException : ApplicationLayerException
{
    public NotFoundException(string entityName) : base($"{entityName} not found.")
    {
        
    }
}