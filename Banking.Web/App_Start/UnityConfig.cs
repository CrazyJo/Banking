using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using Banking.Data.Infrastructure;
using Banking.Service;

namespace Banking.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            InitializeContainer(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void InitializeContainer(UnityContainer container)
        {
            container.RegisterType<IUoWFactory<IBankingUoW>, BankingUoWFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<ISignInManager, SignInManager>(new HierarchicalLifetimeManager());
            container.RegisterType<IBankingService, BankingService>(new HierarchicalLifetimeManager());
        }
    }
}