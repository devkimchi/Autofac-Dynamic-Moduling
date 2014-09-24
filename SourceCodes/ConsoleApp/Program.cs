using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Core;
using ComponentA;
using ComponentB;

namespace ConsoleApp
{
    public class Program
    {
        private static IContainer _container;
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            RegisterModules(builder);

            _container = builder.Build();

            ProcessRequest();
        }

        /// <summary>
        /// Registers modules dynamically scanning .dll files.
        /// </summary>
        /// <param name="builder"><c>ContainerBuilder</c> instance.</param>
        private static void RegisterModules(ContainerBuilder builder)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (String.IsNullOrWhiteSpace(path))
            {
                return;
            }

            //  Gets all compiled assemblies.
            //  This is particularly useful when extending applications functionality from 3rd parties,
            //  if there are interfaces within the modules.
            var assemblies = Directory.GetFiles(path, "Module*.dll", SearchOption.TopDirectoryOnly)
                                      .Select(Assembly.LoadFrom);

            foreach (var assembly in assemblies)
            {
                //  Gets the all modules from each assembly to be registered.
                //  Make sure that each module **MUST** have a parameterless constructor.
                var modules = assembly.GetTypes()
                                      .Where(p => typeof (IModule).IsAssignableFrom(p)
                                                  && !p.IsAbstract)
                                      .Select(p => (IModule) Activator.CreateInstance(p));

                //  Regsiters each module.
                foreach (var module in modules)
                {
                    builder.RegisterModule(module);
                }
            }
        }

        private static void ProcessRequest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                scope.Resolve<IComponentA>().Process();
                scope.Resolve<IComponentB>().Process();
            }
        }
    }
}
