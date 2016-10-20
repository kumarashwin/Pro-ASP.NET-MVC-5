using Ninject;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System.Configuration;
using MongoDB.Driver;

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
            kernel.Bind<MongoClient>().ToSelf().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MongoDbContext"].ConnectionString);
            kernel.Bind<IMongoDatabase>().ToMethod(ctx => ctx.Kernel.Get<MongoClient>().GetDatabase(ConfigurationManager.AppSettings["MongoDbName"]));
            kernel.Bind<IProductRepository>().To<MongoDbProductRepository>();

            //kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("emailSettings", new EmailSettings {
                    WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")});
        }
    }
}