using Autofac;
using LeagueFriend.Mvvm_View;
using LeagueFriend.Mvvm_ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LeagueFriend.DependencyInjection
{
    public static class ContainerConfig
    {
        private static IContainer container;
        public static IContainer Configure()
        {
            
            if(container == null)
            {
                var builder = new ContainerBuilder();
                builder.RegisterAssemblyTypes(Assembly.Load(nameof(EFLibrary)))
                    .Where(o => o.Namespace.Contains("Models") || o.Namespace.Contains("DataAcces"))
                    .As(x => x.GetInterfaces()
                    .FirstOrDefault(s => s.Name == "I" + x.Name)).InstancePerDependency();
                //builder.RegisterAssemblyTypes(Assembly.Load(nameof(Api)))
                //    .Where(o => o.Namespace.Contains("Processor"))
                //    .As(x => x.GetInterfaces()
                //    .FirstOrDefault(s => s.Name == "I" + x.Name));
                builder.RegisterAssemblyTypes(Assembly.Load(nameof(Api)))
                    .Where(o => o.Namespace.Contains("Processor"))
                    .AsImplementedInterfaces();
                //builder.RegisterAssemblyTypes(Assembly.Load(nameof(LeagueFriend)))
                //    .Where(o => o.Namespace.Contains("Mvvm_ViewModel"))
                //    .As(x => x.GetInterfaces().FirstOrDefault(s => s.Name == "I" + x.Name));
                builder.RegisterAssemblyTypes(Assembly.Load(nameof(LeagueFriend)))
                 .Where(o => o.Namespace.Contains("Mvvm_ViewModel"))
                 .AsImplementedInterfaces();
                builder.RegisterAssemblyTypes(Assembly.Load(nameof(LeagueFriend)))
                   .Where(o => o.Namespace.Contains("Mvvm_View")).AsSelf();

                builder.RegisterType<MainWindow>().AsSelf();
                container = builder.Build();
            }
            return container;

        }

    }
}
