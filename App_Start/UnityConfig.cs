using System.Web.Http;
using Unity;
using Unity.WebApi;
using BusinessServices;

namespace WEBAPIProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IProductServices, ProductServices>();
            container.RegisterType<IUserServices, UserServices>();
            container.RegisterType<ITokenServices, TokenServices>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}