using MongoDB.Driver;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete {
    public class MongoDbContext
    {
        private IMongoDatabase _db;

        public MongoDbContext(IMongoDatabase db)
        {
            this._db = db;
        }

        public IMongoCollection<Product> Products {
            get
            {
                return _db.GetCollection<Product>("Products");          
            }
        }

        public IMongoCollection<Counter> Counters {
            get {
                return _db.GetCollection<Counter>("Counters");
            }
        }
    }
}
