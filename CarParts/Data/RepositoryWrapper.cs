namespace CarParts.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _appDbContext;
        private ICarPartRepository _carPart;
        private ICategoryRepository _category;
        private IOrderRepository _order;

        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public ICarPartRepository CarPart
        {
            get
            {
                if (_carPart == null)
                {
                    _carPart = new CarPartRepository(_appDbContext);
                }
                return _carPart;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_appDbContext);
                }
                return _category;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrderRepository(_appDbContext);
                }
                return _order;
            }
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
