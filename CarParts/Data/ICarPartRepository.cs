using CarParts.Models;
using CarParts.Data.DataAccess;

namespace CarParts.Data
{
    public interface ICarPartRepository : IRepositoryBase<CarPart>
    {
        IEnumerable<CarPart> GetAllProductsInCategory(string category);
        IEnumerable<CarPart> GetAllProductsWithCategoryDetails();
        IEnumerable<CarPart> GetCarPartsWithOptions(QueryOptions<CarPart> options);


    }
}
