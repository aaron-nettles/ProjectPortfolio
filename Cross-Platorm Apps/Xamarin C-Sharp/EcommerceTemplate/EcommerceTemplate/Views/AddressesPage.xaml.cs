using EcommerceTemplate.ViewModels;
using Xamarin.Forms;

namespace EcommerceTemplate.Views
{
    public partial class AddressesPage : ContentPage
    {
        AddressesViewModel viewModel;

        public AddressesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AddressesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}
