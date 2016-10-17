using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class MongoDbProductRepository : IProductRepository
    {
        private MongoDbContext context;

        public MongoDbProductRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
    }
}
