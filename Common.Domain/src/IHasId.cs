namespace Jopalesha.Common.Domain
{
    public interface IHasId<out TKey>
    {
        TKey Id { get; }
    }
}
