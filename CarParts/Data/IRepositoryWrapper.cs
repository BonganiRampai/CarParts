namespace CarParts.Data
{
    public interface IRepositoryWrapper
    {
        ICarPartRepository CarPart { get; }
        ICategoryRepository Category { get; }
        IOrderRepository Order { get; }
        void Save();
    }
}
