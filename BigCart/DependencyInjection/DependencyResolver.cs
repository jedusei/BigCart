using SimpleInjector;
using System;
using System.Linq;
using System.Reflection;

namespace BigCart.DependencyInjection
{
    public static class DependencyResolver
    {
        public static Container Container { get; private set; }

        private static void Initialize()
        {
            Container = new Container();
            Container.Options.EnableAutoVerification = false;
        }

        private static void EnsureInitialized()
        {
            if (Container == null)
                Initialize();
        }

        public static void RegisterDependencies(Assembly assembly)
        {
            EnsureInitialized();

            Type dependencyInterfaceType = typeof(IDependency);
            Type singletonInterfaceType = typeof(ISingletonDependency);

            var types = assembly.GetTypes()
                 .Where(t => t.IsClass && !t.IsAbstract && dependencyInterfaceType.IsAssignableFrom(t))
                 .ToArray();

            foreach (Type type in types)
            {
                bool isSingleton = singletonInterfaceType.IsAssignableFrom(type);
                Type dependencyType = type.GetInterfaces()
                    .Except(type.BaseType.GetInterfaces())
                    .FirstOrDefault(t => t != dependencyInterfaceType && t != singletonInterfaceType);

                if (dependencyType == null)
                {
                    if (isSingleton)
                        Container.RegisterSingleton(type);
                    else
                        Container.Register(type);
                }
                else
                {
                    if (isSingleton)
                        Container.RegisterSingleton(dependencyType, type);
                    else
                        Container.Register(dependencyType, type);
                }
            }
        }

        public static T Get<T>() where T : class
        {
            EnsureInitialized();
            return Container.GetInstance<T>();
        }
    }
}
