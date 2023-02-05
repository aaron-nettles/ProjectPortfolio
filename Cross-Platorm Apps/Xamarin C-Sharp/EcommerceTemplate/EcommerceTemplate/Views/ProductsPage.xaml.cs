using EcommerceTemplate.ViewModels;
using Xamarin.Forms;

namespace EcommerceTemplate.Views
{
    public partial class ProductsPage : ContentPage
    {
        ProductsViewModel viewModel;

        public ProductsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ProductsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

    }
}
