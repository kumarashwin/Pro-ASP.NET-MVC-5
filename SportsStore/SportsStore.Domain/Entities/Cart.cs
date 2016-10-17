using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        /// <summary>
        /// Searches if the item already exists in the Cart;
        /// if yes: increases the quantity
        /// if no: adds the item
        /// </summary>
        /// <param name="product">Product to add</param>
        /// <param name="quantity">Quantity</param>
        public void AddItem(Product product, int quantity = 1)
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();

            if (line == null)
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            else
                line.Quantity += quantity;
        }

        public void RemoveLine(Product product) { lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId); }
        public void Clear() { lineCollection.Clear(); }

        public decimal ComputeTotalValue() => lineCollection.Sum(e => e.Product.Price * e.Quantity);
        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
    }
}
