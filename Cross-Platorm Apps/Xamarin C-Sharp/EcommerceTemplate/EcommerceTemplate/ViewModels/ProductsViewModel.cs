using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EcommerceTemplate.Helpers;
using EcommerceTemplate.DataStores;
using Xamarin.Forms;
using EcommerceTemplate.Services;
using EcommerceTemplate.Resources;
using EcommerceTemplate.Views;

namespace EcommerceTemplate.ViewModels
{
    [QueryProperty(nameof(SortOption), nameof(SortOption))]
    [QueryProperty(nameof(Title), nameof(Title))]
    [QueryProperty(nameof(MaterialIds), nameof(MaterialIds))]
    [QueryProperty(nameof(ColorIds), nameof(ColorIds))]
    [QueryProperty(nameof(ProductIds), nameof(ProductIds))]
    [QueryProperty(nameof(CategoryIds), nameof(CategoryIds))]
    [QueryProperty(nameof(Tags), nameof(Tags))]
    [QueryProperty(nameof(OnlyFavorite), nameof(OnlyFavorite))]
    [QueryProperty(nameof(OnlyNew), nameof(OnlyNew))]
    [QueryProperty(nameof(OnlyFeatured), nameof(OnlyFeatured))]
    [QueryProperty(nameof(OnlyPopular), nameof(OnlyPopular))]
    public class ProductsViewModel : BaseViewModel
    {
        IService service => DependencyService.Get<IService>();

        public ObservableCollection<ProductViewModel> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command FilterCommand { get; }
        public Command SortCommand { get; }

        private string materialIds;
        public string MaterialIds
        {
            get => materialIds;
            set => materialIds = value;
        }

        private string colorIds;
        public string ColorIds
        {
            get => colorIds;
            set => colorIds = value;
        }

        private string productIds;
        public string ProductIds
        {
            get => productIds;
            set => productIds = value;
        }

        private string categoryIds;
        public string CategoryIds
        {
            get => categoryIds;
            set => categoryIds = value;
        }

        private string tags;
        public string Tags
        {
            get => tags;
            set => tags = value;
        }

        private bool onlyFavorite;
        public bool OnlyFavorite
        {
            get => onlyFavorite;
            set => onlyFavorite = value;
        }

        private bool onlyNew;
        public bool OnlyNew
        {
            get => onlyNew;
            set => onlyNew = value;
        }

        private bool onlyFeatured;
        public bool OnlyFeatured
        {
            get => onlyFeatured;
            set => onlyFeatured = value;
        }

        private bool onlyPopular;
        public bool OnlyPopular
        {
            get => onlyPopular;
            set => onlyPopular = value;
        }

        private string sortOption;
        public string SortOption
        {
            get => sortOption;
            set
            {
                sortOption = value;
                sortBy = (SortBy)Enum.Parse(typeof(SortBy), value);
            }
        }

        private SortBy sortBy;

        public ProductsViewModel()
        {
            Title = AppResources.Products;
            Items = new ObservableCollection<ProductViewModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            FilterCommand = new Command(OnFilterTapped);
            SortCommand = new Command(OnSortTapped);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            Items.Clear();
            var items = await service.GetProductsAsync(categoriIds: string.IsNullOrEmpty(CategoryIds) ? null : CategoryIds.Split(','),
                                tags: string.IsNullOrEmpty(Tags) ? null : Tags.Split(','),
                                productIds: string.IsNullOrEmpty(ProductIds) ? null : ProductIds.Split(','),
                                colors: string.IsNullOrEmpty(ColorIds) ? null : ColorIds.Split(','),
                                materials: string.IsNullOrEmpty(MaterialIds) ? null : MaterialIds.Split(','),
                                onlyFavorite: OnlyFavorite,
                                onlyNew: OnlyNew,
                                onlyFeatured: OnlyFeatured,
                                onlyPopular: OnlyPopular,
                                sortBy: sortBy);

            foreach (var item in items)
                Items.Add(new ProductViewModel(item));

            IsBusy = false;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async void OnSortTapped()
        {
            var action = await Shell.Current.DisplayActionSheet(AppResources.SortBy,
                                AppResources.Cancel, null,
                                SortBy.Unsorted.FriendlyName(),
                                SortBy.Name_AZ.FriendlyName(),
                                SortBy.Name_ZA.FriendlyName(),
                                SortBy.Price_Low.FriendlyName(),
                                SortBy.Price_High.FriendlyName(),
                                SortBy.Highest_Rate.FriendlyName(),
                                SortBy.Rate_Count.FriendlyName());

            if (action == "Cancel") return;

            sortBy = ExtensionMethods.SortByFromFriendlyName(action);
            IsBusy = true;
        }

        async void OnFilterTapped()
        {
            MessagingCenter.Subscribe<FilterViewModel>(this, "apply", (sender) =>
            {
                MessagingCenter.Unsubscribe<FilterViewModel>(this, "apply");
                MessagingCenter.Unsubscribe<FilterViewModel>(this, "cancel");
                IsBusy = true;
            });

            MessagingCenter.Subscribe<FilterViewModel>(this, "cancel", (sender) =>
            {
                MessagingCenter.Unsubscribe<FilterViewModel>(this, "apply");
                MessagingCenter.Unsubscribe<FilterViewModel>(this, "cancel");
            });

            await Shell.Current.GoToAsync($"{nameof(FilterPage)}" +
                                          $"?{nameof(FilterViewModel.CategoryIds)}={CategoryIds}" +
                                          $"&{nameof(FilterViewModel.TagNames)}={Tags}" +
                                          $"&{nameof(FilterViewModel.ColorNames)}={ColorIds}" +
                                          $"&{nameof(FilterViewModel.MaterialNames)}={MaterialIds}" +
                                          $"&{nameof(FilterViewModel.OnlyFavorite)}={OnlyFavorite}" +
                                          $"&{nameof(FilterViewModel.OnlyFeatured)}={OnlyFeatured}" +
                                          $"&{nameof(FilterViewModel.OnlyNew)}={OnlyNew}" +
                                          $"&{nameof(FilterViewModel.OnlyPopular)}={OnlyPopular}" +
                                          $"&{nameof(FilterViewModel.ProductIds)}={ProductIds}" +
                                          $"&{nameof(FilterViewModel.Title)}={Title}");
        }
        
    }
}
