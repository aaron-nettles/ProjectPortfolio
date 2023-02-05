using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Internals;
using EcommerceTemplate.Services;

namespace EcommerceTemplate.ViewModels
{
    [QueryProperty(nameof(Title), nameof(Title))]
    [QueryProperty(nameof(ProductIds), nameof(ProductIds))]
    [QueryProperty(nameof(CategoryIds), nameof(CategoryIds))]
    [QueryProperty(nameof(TagNames), nameof(TagNames))]
    [QueryProperty(nameof(ColorNames), nameof(ColorNames))]
    [QueryProperty(nameof(MaterialNames), nameof(MaterialNames))]
    [QueryProperty(nameof(OnlyFavorite), nameof(OnlyFavorite))]
    [QueryProperty(nameof(OnlyNew), nameof(OnlyNew))]
    [QueryProperty(nameof(OnlyFeatured), nameof(OnlyFeatured))]
    [QueryProperty(nameof(OnlyPopular), nameof(OnlyPopular))]
    public class FilterViewModel : BaseViewModel
    {
        IService service => DependencyService.Get<IService>();

        public ObservableCollection<CategoryViewModel> Categories { get; }
        public ObservableCollection<StringViewModel> Tags { get; }
        public ObservableCollection<StringViewModel> Colors { get; }
        public ObservableCollection<StringViewModel> Materials { get; }

        public Command<CategoryViewModel> CategoryCommand { get; }
        public Command<StringViewModel> TagCommand { get; }
        public Command<StringViewModel> ColorCommand { get; }
        public Command<StringViewModel> MaterialCommand { get; }
        public Command ClearAllCommand { get; }
        public Command CancelCommand { get; }
        public Command ApplyCommand { get; }

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
            set
            {
                categoryIds = value;
                Categories.ForEach(i => i.IsSelected = value?.Split(',').Any(id => id == i.Id) ?? false);
            }
        }

        private string tagNames;
        public string TagNames
        {
            get => tagNames;
            set
            {
                tagNames = value;
                Tags.ForEach(i => i.IsSelected = value?.Split(',').Any(name => name == i.Name) ?? false);
            }
        }

        private string colorNames;
        public string ColorNames
        {
            get => colorNames;
            set
            {
                colorNames = value;
                Colors.ForEach(i => i.IsSelected = value?.Split(',').Any(id => id == i.Name) ?? false);
            }
        }

        private string materialNames;
        public string MaterialNames
        {
            get => materialNames;
            set
            {
                materialNames = value;
                Materials.ForEach(i => i.IsSelected = value?.Split(',').Any(id => id == i.Name) ?? false);
            }
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

        public FilterViewModel()
        {
            Categories = new ObservableCollection<CategoryViewModel>();
            Tags = new ObservableCollection<StringViewModel>();
            Colors = new ObservableCollection<StringViewModel>();
            Materials = new ObservableCollection<StringViewModel>();

            var categories = service.GetCategoriesAsync(null).Result;

            foreach (var item in categories)
                Categories.Add(new CategoryViewModel(item));

            var tags =  service.GetAllTagsAsync().Result;

            foreach (var item in tags)
                Tags.Add(new StringViewModel(item));

            var colors = service.GetAllColorsAsync().Result;

            foreach (var item in colors)
                Colors.Add(new StringViewModel(item));

            var materials = service.GetAllMaterialsAsync().Result;

            foreach (var item in materials)
                Materials.Add(new StringViewModel(item));

            CategoryCommand = new Command<CategoryViewModel>(OnCategoryTapped);
            TagCommand = new Command<StringViewModel>(OnTagTapped);
            ColorCommand = new Command<StringViewModel>(OnColorTapped);
            MaterialCommand = new Command<StringViewModel>(OnMaterialTapped);

            ClearAllCommand = new Command(OnClearAllTapped);
            CancelCommand = new Command(OnCancelTapped);
            ApplyCommand = new Command(OnApplyTapped);
        }

        private string removeId(string target, string id)
        {
            if (string.IsNullOrEmpty(target)) return id;

            var ids = target.Split(',').ToList();
            ids.Remove(id);
            return String.Join(",", ids);
        }

        private string addId(string target, string id)
        {
            if (string.IsNullOrEmpty(target)) return id;

            var ids = target.Split(',').ToList();
            ids.Add(id);
            return String.Join(",", ids);
        }

        void OnCategoryTapped(CategoryViewModel item)
        {
            if (item == null) return;

            if (item.IsSelected)
                CategoryIds = removeId(CategoryIds, item.Id);
            else
                CategoryIds = addId(CategoryIds, item.Id);
        }

        void OnTagTapped(StringViewModel item)
        {
            if (item == null) return;

            if (item.IsSelected)
                TagNames = removeId(TagNames, item.Name);
            else
                TagNames = addId(TagNames, item.Name);
        }

        void OnColorTapped(StringViewModel item)
        {
            if (item == null) return;

            if (item.IsSelected)
                ColorNames = removeId(ColorNames, item.Name);
            else
                ColorNames = addId(ColorNames, item.Name);
        }

        void OnMaterialTapped(StringViewModel item)
        {
            if (item == null) return;

            if (item.IsSelected)
                MaterialNames = removeId(MaterialNames, item.Name);
            else
                MaterialNames = addId(MaterialNames, item.Name);
        }

        void OnClearAllTapped()
        {
            CategoryIds = null;
            TagNames = null;
            ColorNames = null;
            MaterialNames = null;
        }

        async void OnApplyTapped()
        {
            await Shell.Current.GoToAsync($"..?{nameof(ProductsViewModel.CategoryIds)}={CategoryIds}" +
                                            $"&{nameof(ProductsViewModel.Tags)}={TagNames}" +
                                            $"&{nameof(ProductsViewModel.ColorIds)}={ColorNames}" +
                                            $"&{nameof(ProductsViewModel.MaterialIds)}={MaterialNames}" +
                                            $"&{nameof(ProductsViewModel.OnlyFavorite)}={OnlyFavorite}" +
                                            $"&{nameof(ProductsViewModel.OnlyFeatured)}={OnlyFeatured}" +
                                            $"&{nameof(ProductsViewModel.OnlyNew)}={OnlyNew}" +
                                            $"&{nameof(ProductsViewModel.OnlyPopular)}={OnlyPopular}" +
                                            $"&{nameof(ProductsViewModel.ProductIds)}={ProductIds}" +
                                            $"&{nameof(ProductsViewModel.Title)}={Title}")
                                .ContinueWith((o) => MessagingCenter.Send<FilterViewModel>(this, "apply"));
        }

        async void OnCancelTapped()
        {
            await Shell.Current.GoToAsync("..")
                        .ContinueWith((o) => MessagingCenter.Send<FilterViewModel>(this, "cancel"));
        }
    }
}
