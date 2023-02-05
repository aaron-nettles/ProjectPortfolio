using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using EcommerceTemplate.Services;
using EcommerceTemplate.Resources;
using EcommerceTemplate.Views;

namespace EcommerceTemplate.ViewModels
{
    public class CheckoutAddressViewModel : BaseViewModel
    {
        IService service => DependencyService.Get<IService>();

        public ObservableCollection<AddressViewModel> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command<AddressViewModel> AddressCommand { get; }
        public Command PaymentCommand { get; }
        public Command AddCommand { get; }
        public Command EditCommand { get; }

        private double total;
        public double Total
        {
            get => total;
        }

        private double shipping = 5.0;
        public double Shipping
        {
            get => shipping;
        }

        public double GrandTotal
        {
            get => total + shipping;
        }

        private string selectedAddresId;

        public CheckoutAddressViewModel()
        {
            Title = AppResources.ShippingAddress;

            Items = new ObservableCollection<AddressViewModel>();

            LoadItemsCommand = new Command(async () => await LoadItems());
            AddressCommand = new Command<AddressViewModel>(OnAddressTapped);
            PaymentCommand = new Command(async () =>
                await Shell.Current.GoToAsync($"{nameof(CheckoutPaymentPage)}" +
                                              $"?{nameof(CheckoutPaymentViewModel.AddressId)}={selectedAddresId}"));
            AddCommand = new Command(OnAddTapped);
            EditCommand = new Command(OnEditTapped);

            var cartLines = service.GetCartItemsAsync().Result;

            foreach (var item in cartLines)
                total += item.Total;
        }

        async Task LoadItems()
        {
            IsBusy = true;

            Items.Clear();
            var items = await service.GetAddressesAsync(Globals.LoggedCustomerId);

            foreach (var item in items)
                Items.Add(new AddressViewModel(item));

            if (Items.Count > 0)
            {
                Items[0].IsSelected = true;
                selectedAddresId = Items[0].Address.Id;
            }

            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        void OnAddressTapped(AddressViewModel item)
        {
            if (item == null) return;

            foreach (var a in Items)
                a.IsSelected = a.Address.Id == item.Address.Id;

            selectedAddresId = item.Address.Id;
        }

        private async void OnAddTapped()
        {
            await Shell.Current.GoToAsync($"{nameof(AddressDetailPage)}");
        }

        private async void OnEditTapped()
        {
            if (string.IsNullOrEmpty(selectedAddresId)) return;

            await Shell.Current.GoToAsync($"{nameof(AddressDetailPage)}" +
                                          $"?{nameof(AddressDetailViewModel.AddressId)}={selectedAddresId}");
        }

    }
}
