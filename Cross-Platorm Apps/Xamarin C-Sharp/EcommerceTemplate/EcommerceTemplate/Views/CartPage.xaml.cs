using EcommerceTemplate.ViewModels;
using Xamarin.Forms;

namespace EcommerceTemplate.Views
{
    public partial class CartPage : ContentPage
    {
        CartViewModel viewModel;

        public CartPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CartViewModel();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

    }
}
