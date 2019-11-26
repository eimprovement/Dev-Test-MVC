using Autofac;
using Autofac.Integration.Mvc;
using eimprovement.Domain.Services;
using System.Web.Mvc;

namespace eimprovement.WebApplication
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // for development since all data inside one object 
            // specify singleton to maintain data consistency
            // Change Instance type later when swap in new data source
            builder.RegisterType<MockDataSource>()
                .As<IPetStoreData>()
                .SingleInstance();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}