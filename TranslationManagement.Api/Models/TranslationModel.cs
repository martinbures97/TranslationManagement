namespace TranslationManagement.Api.Models;

public class TranslationModel
{
    private string _content;
    private string _customer;
    
    public TranslationModel(string? customer, string content) : this(content)
    {
        Customer = customer;
    }
    
    public TranslationModel(string content) : this()
    {
        Content = content;
    }

    public TranslationModel()
    {
        
    }

    public string? Customer
    {
        get => _customer;
        set => _customer = RemoveNewLines(value);
    }

    public string Content
    {
        get => _content;
        set => _content = RemoveNewLines(value);
    }

    private string RemoveNewLines(string? value)
    {
        return string.IsNullOrEmpty(value) 
            ? string.Empty 
            : value.Replace("\n", "").Replace("\t", "");
    }
}