using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Foundation;
using UIKit;

namespace PeopleOrNotPeopleDemo.iOS
{
    public class Bootstrapper : PeopleOrNotPeopleDemo.Bootstrapper
    {
        public static void Init()
        {
            var instance = new Bootstrapper();
        }

        protected override void Initialize()
        {
            base.Initialize();

            ContainerBuilder.RegisterType<CoreMLClassifier>().As<IClassifier>();
        }
    }
}