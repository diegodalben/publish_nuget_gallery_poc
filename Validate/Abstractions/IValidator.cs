namespace Validate.Abstractions
{
    public interface IValidator<in T> where T : class
    {
        bool IsValid(T value);
    }
}