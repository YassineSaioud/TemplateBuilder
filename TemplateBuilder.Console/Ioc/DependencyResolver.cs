using TemplateBuilder.Core.Helpers;
using TemplateBuilder.Core.Impementations;
using TemplateBuilder.Core.Interfaces;
using TemplateBuilder.Core.Models;
using System;
using System.Collections.Generic;
using Unity;

namespace TemplateBuilder.Console.Ioc
{
    public static class DependencyResolver
    {
        static IUnityContainer Container => container.Value;

        static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static T Resolve<T>() => Container.Resolve<T>();

        public static void RegisterTypes(IUnityContainer container)
        {
            // Operation Strategy
            container.RegisterType<IFileOperations, CreateFiles>("create");
            container.RegisterType<IFileOperations, CombineFiles>("combine");
            container.RegisterType<IEnumerable<IFileOperations>, IFileOperations[]>();

            container.RegisterInstance(new EventHandler<FeedbackEventArgs>(ConsoleHelper.WriteMessage));
        }
    }
}


