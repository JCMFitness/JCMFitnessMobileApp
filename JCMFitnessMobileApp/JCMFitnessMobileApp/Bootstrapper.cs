using Autofac;
using JCMFitnessMobileApp.Services;
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
                var baseUrl = "https://newsapi.org";
                var containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterType<MainShell>();
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
