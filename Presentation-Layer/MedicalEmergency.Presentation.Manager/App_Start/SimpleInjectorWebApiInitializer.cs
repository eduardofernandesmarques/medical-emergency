using SimpleInjector;
using MedicalEmergency.Infrastructure.CrossCutting.IoC;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.Web;
using System.Reflection;
using System.Web.Mvc;

[assembly: WebActivator.PostApplicationStartMethod(typeof(MedicalEmergency.Presentation.Manager.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace MedicalEmergency.Presentation.Manager.App_Start
{
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            Bootstrapper.RegisterServices(container);
        }
    }
}