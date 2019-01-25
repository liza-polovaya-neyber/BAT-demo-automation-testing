using BATDemoSalesForce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BATDemoSalesForce.Services.RestClient;
using BATDemoSalesForce.Services.SalesForceAuthentication;
using BATDemoTests.Validators;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace BATDemoSalesForce.Repos
{
    public class ContainerConfig
    {
        public static UnityContainer Create()
        {
            var container = new UnityContainer();

            container.RegisterType<IConfigurationService, ConfigurationService>("salesForceConfiguration",
                new ContainerControlledLifetimeManager(), new InjectionConstructor("salesforce"));

            container.RegisterType<ISalesForceAuthenticationService, SalesForceAuthenticationService>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IConfigurationService>("salesForceConfiguration")));


            container.RegisterType<ISalesForceService, SalesForceService>(new TransientLifetimeManager());
            container.RegisterType<IProfileValidator, ProfileValidator>(new TransientLifetimeManager());


            container.RegisterType<IProfileRepository, ProfileRepository>(new TransientLifetimeManager());
            container.RegisterType<ISalesForceRestService, SalesForceRestService>(new TransientLifetimeManager());

            return container;
        }
    }
}
