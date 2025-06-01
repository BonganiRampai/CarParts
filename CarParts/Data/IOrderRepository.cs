using CarParts.Models;

namespace CarParts.Data
{
    public interface IOrderRepository:IRepositoryBase<Order>
    {
        IEnumerable<Order> GetUserOrdersWithCategoryDetails(string Name);
    }
}
