using System;
using System.Collections.Generic;
using Lift.ViewModels;
using Lift.Views;
using Xamarin.Forms;

namespace Lift
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(AddItemPage), typeof(AddItemPage));
            Routing.RegisterRoute(nameof(VideoListPage), typeof(VideoListPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LessonPage), typeof(LessonPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

