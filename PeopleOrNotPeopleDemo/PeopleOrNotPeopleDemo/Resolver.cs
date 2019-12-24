using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleOrNotPeopleDemo
{
    public class Resolver
    {
        private static IContainer container;

        public static void Initalize(IContainer container)
        {
            Resolver.container = container;
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
