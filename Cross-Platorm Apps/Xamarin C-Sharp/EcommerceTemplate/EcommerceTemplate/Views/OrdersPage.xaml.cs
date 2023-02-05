using EcommerceTemplate.ViewModels;
using Xamarin.Forms;

namespace EcommerceTemplate.Views
{
    public partial class OrdersPage : ContentPage
    {
        OrdersViewModel viewModel;

        public OrdersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new OrdersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

    }
}
