namespace Auth.Domain.Data
{
    /// <summary>
    /// Interface that represents value object of a given type.
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public interface IValueObject<Type>
    {
        public Type Value { get; }
    }
}
