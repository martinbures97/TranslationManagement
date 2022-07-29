namespace TranslationManagement.Application.Common;

public interface IEntity<out TKey>
{
    TKey Id { get; }
}