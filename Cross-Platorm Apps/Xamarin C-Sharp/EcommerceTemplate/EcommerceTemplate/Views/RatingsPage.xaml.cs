using EcommerceTemplate.ViewModels;
using Xamarin.Forms;

namespace EcommerceTemplate.Views
{
    public partial class RatingsPage : ContentPage
    {
        RatingsViewModel viewModel;

        public RatingsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RatingsViewModel(); 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

    }
}
