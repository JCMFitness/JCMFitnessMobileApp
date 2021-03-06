using System;
using System.Collections.Generic;
using Autofac;
using System.Text;

namespace JCMFitnessMobileApp
{

        public static class Resolver
        {
            private static IContainer container;

            public static void Initialize(IContainer container)
            {
                Resolver.container = container;
            }

            public static T Resolve<T>()
            {
                return container.Resolve<T>();
            }
        }
    
}
