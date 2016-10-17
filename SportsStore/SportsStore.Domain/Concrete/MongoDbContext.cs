using MongoDB.Bson;
using MongoDB.Driver;
using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Domain.Concrete
{
    public class MongoDbContext
    {
        private IMongoDatabase _db;

        public MongoDbContext(IMongoDatabase db)
        {
            this._db = db;
        }

        public IEnumerable<Product> Products {
            get
            {
                return _db.GetCollection<Product>("Products")
                    .Find(new BsonDocument())
                    .Project<Product>(Builders<Product>.Projection.Exclude("_id"))
                    .ToEnumerable();              
            }
        }
    }
}
