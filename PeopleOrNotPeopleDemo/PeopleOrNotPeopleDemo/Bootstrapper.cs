using Autofac;
using Xamarin.Forms;
using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using PeopleOrNotPeopleDemo.ViewModels;

namespace PeopleOrNotPeopleDemo
{
    public class Bootstrapper
    {
        protected ContainerBuilder ContainerBuilder { get; private set; }

        public Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize()
        {
            ContainerBuilder = new ContainerBuilder();

            var currentAssembly = Assembly.GetExecutingAssembly();

            foreach(var type in currentAssembly.DefinedTypes.Where(x => x.IsSubclassOf(typeof(Page))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            foreach(var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(ViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

        }
        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();

            Resolver.Initalize(container);
        }
    }
}
