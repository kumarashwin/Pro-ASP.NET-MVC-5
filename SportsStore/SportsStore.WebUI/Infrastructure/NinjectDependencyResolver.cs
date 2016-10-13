using Ninject;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using SportsStore.Domain.Abstract;
using Moq;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam) {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);
        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);

        private void AddBindings()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns(new List<Product> {
                    new Product { Name = "Football", Price = 25 },
                    new Product { Name = "Surfboard", Price = 179 },
                    new Product { Name = "Running shoes", Price = 95}});

            kernel.Bind<IProductRepository>().ToConstant(mock.Object);

        }
    }
}