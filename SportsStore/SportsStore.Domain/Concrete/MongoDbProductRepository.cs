using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using SportsStore.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace SportsStore.Domain.Concrete {
    public class MongoDbProductRepository : IProductRepository {

        private MongoDbContext context;
        private IEnumerable<Product> products;
        private bool dataChanged = true;

        public MongoDbProductRepository(MongoDbContext context) {
            this.context = context;
        }

        public IEnumerable<Product> Products {
            get {
                if (!dataChanged)
                    return this.products;
                else {
                    this.products = context.Products.Find(new BsonDocument())
                        .Project<Product>(Builders<Product>.Projection.Exclude("_id"))
                        .ToEnumerable();

                    dataChanged = false;
                    return Products;
                }
            }
        }

        public Product DeleteProduct(int productId) {
            var toDelete = Products.Where(p => p.ProductId == productId).FirstOrDefault();
            var deleteResult = context.Products.DeleteOne(p => p.ProductId == productId);
            if (deleteResult.IsAcknowledged) {
                dataChanged = true;
                return toDelete;
            } else
                return null;
        }

        public void SaveProduct(Product product) {
            if (product.ProductId == 0) {
                product.ProductId = getNextSequence("Products");
                context.Products.InsertOne(product);
            }
            else
                context.Products.ReplaceOne(p => p.ProductId == product.ProductId, product);
            dataChanged = true;
        }

        public int getNextSequence(string collectionName) {
            var counter = context.Counters.Find(new BsonDocument()).Project<Counter>(Builders<Counter>.Projection.Exclude("_id")).FirstOrDefault();
            var sequence = counter.Sequence;
            counter.Sequence++;
            context.Counters.ReplaceOne(c => c.CollectionName == collectionName, counter);
            return sequence;
        }
    }
}
