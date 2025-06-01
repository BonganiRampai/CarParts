using CarParts.Models;
using CarParts.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarParts.Data
{
    public class CarPartRepository : RepositoryBase<CarPart>, ICarPartRepository
    {
        public CarPartRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public IEnumerable<CarPart> GetAllProductsWithCategoryDetails()
        {
            return _appDbContext.CarParts
                .Include(m => m.Category);
        }

        public IEnumerable<CarPart> GetAllProductsInCategory(string category)
        {
            Category cat = _appDbContext.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == category.ToLower());

            return _appDbContext.CarParts.Where(p => p.CategoryID == cat.CategoryID);
        }

        public IEnumerable<CarPart> GetCarPartsWithOptions(QueryOptions<CarPart> options)
        {
            IQueryable<CarPart> query = _appDbContext.Set<CarPart>();

            if (options.HasWhere)
                query = query.Where(options.Where);

            if (options.HasPaging)
            {
                query = query.Skip((options.PageNumber - 1) * options.PageSize)
                             .Take(options.PageSize);
            }

            return query.ToList();
        }
    }
}
