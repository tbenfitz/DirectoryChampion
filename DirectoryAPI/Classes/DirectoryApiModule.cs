using Autofac;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectoryAPI.Abstractions;

namespace DirectoryAPI.Classes
{
    public class DirectoryApiModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(IDetermineDirectoryApiAssembly).GetTypeInfo()
                .Assembly).AsImplementedInterfaces();
        }
    }
}
