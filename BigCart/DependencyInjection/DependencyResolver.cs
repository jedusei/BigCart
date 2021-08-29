using BigCart.Services.Navigation;
using SimpleInjector;
using System;
using System.Linq;
using System.Reflection;

namespace BigCart.DependencyInjection
{
    public static class DependencyResolver
    {
        public static Container Container { get; private set; }

        public static void Initialize()
        {
            Container ??= new Container();
        }

        public static void RegisterDependencies(Assembly assembly)
        {
            Type dependencyInterfaceType = typeof(IDependency);
            Type singletonInterfaceType = typeof(ISingletonDependency);

            var types = assembly.GetTypes()
                 .Where(t => t.IsClass && !t.IsAbstract && dependencyInterfaceType.IsAssignableFrom(t))
                 .ToArray();

            foreach (Type type in types)
            {
                bool isSingleton = singletonInterfaceType.IsAssignableFrom(type);
                Type dependencyType = type.GetInterfaces().FirstOrDefault(t => t != dependencyInterfaceType && t != singletonInterfaceType);
                dependencyType ??= type;

                if (isSingleton)
                    Container.RegisterSingleton(dependencyType, type);
                else
                    Container.Register(dependencyType, type);
            }
        }

        public static T Get<T>() where T : class
        {
            return Container.GetInstance<T>();
        }
    }
}
