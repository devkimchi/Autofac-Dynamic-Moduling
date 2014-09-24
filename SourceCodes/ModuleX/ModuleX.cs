using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using ComponentA;

namespace ModuleX
{
    public class ModuleX : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ComponentA.ComponentA>().As<IComponentA>();
        }
    }
}
