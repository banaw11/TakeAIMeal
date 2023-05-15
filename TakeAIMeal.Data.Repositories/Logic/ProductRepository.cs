using TakeAIMeal.Data.Repositories.Infrastructure;
using TakeAIMeal.Data.Repositories.Interfaces;

namespace TakeAIMeal.Data.Repositories.Logic
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ITakeAIMealDbContext dbContext) : base(dbContext)
        {
        }
    }
}
