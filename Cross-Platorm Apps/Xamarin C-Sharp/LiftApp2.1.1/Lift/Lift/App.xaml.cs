using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Lift.Services;
using Lift.Views;

namespace Lift
{
    public partial class App : Application
    {

        public App ()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart ()
        {
            MainPage = new NavigationPage(new AppShell());
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

