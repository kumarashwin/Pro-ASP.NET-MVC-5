using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        private IDiscountHelper discounter;
        private static int counter = 0;

        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;
            System.Diagnostics.Debug.WriteLine(string.Format($"Instance {++counter} created"));
        }

        public decimal ValueProducts(IEnumerable<Product> products) => discounter.ApplyDiscount(products.Sum(p => p.Price));
    }
}