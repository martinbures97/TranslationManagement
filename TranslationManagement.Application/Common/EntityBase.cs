namespace TranslationManagement.Application.Common;

public abstract class EntityBase<TKey> : IEntity<TKey>
{
    public virtual TKey Id { get; set; }
    
    public bool IsDeleted { get; set; }
}