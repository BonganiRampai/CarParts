using System.Linq.Expressions;

namespace CarParts.Data.DataAccess
{
    public class QueryOptions<T>
    {
        // public properties for filtering, and paging
        public Expression<Func<T, bool>> Where { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // read-only properties
        public bool HasWhere => Where != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;

    }
}