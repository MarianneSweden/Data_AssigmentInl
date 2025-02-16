using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository

    {
        public ProductRepository(DataContext context) : base(context)
        {
        }
    }
}
