using System;
using Xamarin.Forms;

namespace EcommerceTemplate.Views
{
    public partial class CheckoutCompletedPage : ContentPage
    {
        public CheckoutCompletedPage()
        {
            InitializeComponent();
        }

        async void OnContinueTapped(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("//tabbar/cart");
        }

    }
}
