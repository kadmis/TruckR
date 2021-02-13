namespace Auth.Domain.Data
{
    /// <summary>
    /// Interface that represents an entity with a given key type.
    /// </summary>
    /// <typeparam name="KeyType"></typeparam>
    public interface IEntity<KeyType>
    {
        public KeyType Id { get; }
    }
}
