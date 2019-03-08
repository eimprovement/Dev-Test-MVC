using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Autofac;
using Autofac.Integration.Mvc;

using eimprovement.WebApplication.Client;
using eimprovement.WebApplication.Services;

namespace eimprovement.WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SetupDependencyResolver();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void SetupDependencyResolver()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            PetStoreApiClient petStoreClient = CreatePetStoreClient();
            builder.RegisterInstance(petStoreClient).As<IPetStoreApiClient>();

            builder.RegisterType<PetStoreService>().As<IPetStoreService>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private PetStoreApiClient CreatePetStoreClient()
        {
            string baseAddres = GetAppSetting("PetStoreClientBaseAddress");
            string apiKey = GetAppSetting("PetStoreClientApiKey");

            return new PetStoreApiClient(new Uri(baseAddres), apiKey);
        }

        private string GetAppSetting(string name) {
            var value = ConfigurationManager.AppSettings[name];
            if (value == null) throw new InvalidOperationException($"App setting={name} not present in app configuration");
            return value;
        }
    }
}
