using Autofac;
using Models.Abstractions;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Classes
{
    /// <summary>
    /// Use by Autofac for IoC
    /// </summary>
    public class DirectoryOperationsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(IDetermineDirectoryOperationsAssembly)
                .GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
        }
    }
}
