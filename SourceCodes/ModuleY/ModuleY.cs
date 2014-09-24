using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using ComponentB;

namespace ModuleY
{
    public class ModuleY : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ComponentB.ComponentB>().As<IComponentB>();
        }
    }
}
