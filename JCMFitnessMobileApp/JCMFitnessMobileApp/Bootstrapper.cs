using Autofac;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCMFitnessMobileApp
{
    public static class Bootstrapper
    {

            public static void Initialize()
            {
                var baseUrl = "https://jcmfitnessapi.azurewebsites.net";
                var containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterAssemblyTypes(typeof(App).Assembly)
                    .Where(x => x.IsSubclassOf(typeof(ViewModel)));
                
                containerBuilder.RegisterType<FitnessService>().As<IFitnessService>();
                IFitApi refitInstance = RestService.For<IFitApi>(baseUrl);
                containerBuilder.RegisterInstance(refitInstance)
                    .As<IFitApi>();

                var container = containerBuilder.Build();
                Resolver.Initialize(container);

            }
    }
}
