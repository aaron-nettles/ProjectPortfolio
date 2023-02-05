using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using EcommerceTemplate.Services;
using EcommerceTemplate.Resources;
using EcommerceTemplate.Views;

namespace EcommerceTemplate.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        IService service => DependencyService.Get<IService>();

        public ObservableCollection<CartItemViewModel> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command RemoveItemCommand { get; }
        public Command DeleteItemCommand { get; }
        public Command DeleteAllCommand { get; }
        public Command CheckoutCommand { get; }

        public double Total
        {
            get
            {
                double total = 0.0;

                foreach (var item in Items)
                    total += item.Total;

                return total;
            }
        }

        public CartViewModel()
        {
            Title = AppResources.ShoppingCart;

            Items = new ObservableCollection<CartItemViewModel>();
            Items.CollectionChanged += (_, __) => OnPropertyChanged(nameof(Total));

            LoadItemsCommand = new Command(async () => await LoadItems());
            AddItemCommand = new Command<CartItemViewModel>(OnAddItemTapped);
            RemoveItemCommand = new Command<CartItemViewModel>(OnRemoveItemTapped);
            DeleteItemCommand = new Command<CartItemViewModel>(OnDeleteItemTapped);
            DeleteAllCommand = new Command(OnDeleteAllTapped);
            CheckoutCommand = new Command(async () =>
                await Shell.Current.GoToAsync($"{nameof(CheckoutAddressPage)}"));
        }

        async Task LoadItems()
        {
            IsBusy = true;

            Items.Clear();
            var items = await service.GetCartItemsAsync();
            foreach (var item in items)
            {
                Items.Add(new CartItemViewModel(item));
            }

            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        
        private async void OnAddItemTapped(CartItemViewModel item)
        {
            item.Quantity += 1;

            var line = await service.GetCartItemAsync(item.Id);
            line.Quantity = item.Quantity;
            await service.UpdateCartItemAsync(line);

            OnPropertyChanged(nameof(Total));
        }

        private async void OnRemoveItemTapped(CartItemViewModel item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity -= 1;
                (await service.GetCartItemAsync(item.Id)).Quantity = item.Quantity;

                OnPropertyChanged(nameof(Total));
            }
            else
                OnDeleteItemTapped(item);
        }

        private async void OnDeleteItemTapped(CartItemViewModel item)
        {
            Items.Remove(item);
            await service.DeleteCartItemAsync(item.Id);
        }

        private async void OnDeleteAllTapped()
        {
            var yesNo = await Shell.Current.DisplayAlert(AppResources.Question,
                            AppResources.DoYouWantDeleteAllItems, AppResources.Yes, AppResources.No);

            if (yesNo == false) return;

            Items.Clear();
            await service.DeleteAllCartItemsAsync();
        }

    }
}
