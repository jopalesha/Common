namespace Jopalesha.Common.Domain
{
    public interface IKey<out T>
    {
        T Value { get; }
    }
}