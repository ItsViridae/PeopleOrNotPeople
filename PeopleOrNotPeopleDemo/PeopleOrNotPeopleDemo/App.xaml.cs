using PeopleOrNotPeopleDemo.ViewModels;
using PeopleOrNotPeopleDemo.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeopleOrNotPeopleDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainView = Resolver.Resolve<MainView>();
            var navigationPage = new NavigationPage(mainView);

            ViewModel.Navigation = navigationPage.Navigation;

            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
