using CarParts.Models;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Data
{
    public class OrderRepository: RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public IEnumerable<Order> GetUserOrdersWithCategoryDetails(string Name)
        {
            return _appDbContext.Orders.
                Where(o=>o.CustomerName == Name).
                Include(c=>c.Category);
        }
    }
}
