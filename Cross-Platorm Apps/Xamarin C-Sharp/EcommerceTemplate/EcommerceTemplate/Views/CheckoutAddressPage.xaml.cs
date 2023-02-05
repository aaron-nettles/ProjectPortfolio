using EcommerceTemplate.ViewModels;
using Xamarin.Forms;

namespace EcommerceTemplate.Views
{
    public partial class CheckoutAddressPage : ContentPage
    {
        CheckoutAddressViewModel viewModel;

        public CheckoutAddressPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CheckoutAddressViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

    }
}
