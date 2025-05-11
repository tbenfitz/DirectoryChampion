using Autofac;
using DirectoryOperations.Abstractions;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Abstractions;

namespace DirectoryOperations.Classes
{
    public class DirectoryOperationsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(IDetermineDirectoryOperationsAssembly)
                .GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register IFileSystem
            builder.RegisterAssemblyTypes(typeof(IFileSystem)
                .GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register IDirectoryModel
            builder.RegisterAssemblyTypes(typeof(Models.Abstractions.IDirectoryModel)
               .GetTypeInfo().Assembly)
               .AsImplementedInterfaces();
        }
    }
}
