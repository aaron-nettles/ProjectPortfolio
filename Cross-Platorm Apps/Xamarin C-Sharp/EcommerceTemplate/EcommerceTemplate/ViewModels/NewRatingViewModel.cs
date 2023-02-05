using EcommerceTemplate.Models;
using System;
using Xamarin.Forms;
using EcommerceTemplate.Services;
using EcommerceTemplate.Resources;

namespace EcommerceTemplate.ViewModels
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public class NewRatingViewModel : BaseViewModel
    {
        IService service => DependencyService.Get<IService>();

        public Command StarCommand { get; }
        public Command SubmitCommand { get; }

        private string productId;
        public string ProductId
        {
            get => productId;
            set
            {
                productId = value;
                LoadProduct(value);
            }
        }

        private string productImage;
        public string ProductImage
        {
            get => productImage;
            set => SetProperty(ref productImage, value);
        }

        private string productName;
        public string ProductName
        {
            get => productName;
            set => SetProperty(ref productName, value);
        }

        private string productDescription;
        public string ProductDescription
        {
            get => productDescription;
            set => SetProperty(ref productDescription, value);
        }

        private float starCount;
        public float StarCount
        {
            get => starCount;
            set => SetProperty(ref starCount, value);
        }

        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public NewRatingViewModel()
        {
            Title = AppResources.NewRating;

            StarCommand = new Command<String>(OnStarTapped);
            SubmitCommand = new Command(OnSubmitTapped);

            StarCount = 5;
        }

        public async void LoadProduct(string id)
        {
            var item = await service.GetProductAsync(id);
            ProductName = item.Name;
            ProductImage = item.FirstImage;
            ProductDescription = item.Description.Replace("\n", " ");
        }

        private void OnStarTapped(String star)
        {
            StarCount = int.Parse(star);
        }

        private async void OnSubmitTapped()
        {
            Rating newItem = new Rating
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = ProductId,
                CustomerId = Globals.LoggedCustomerId,
                Star = (byte)starCount,
                Text = text,
                DateGmt = DateTime.UtcNow
            };

            await service.AddRatingAsync(newItem);
            await Shell.Current.GoToAsync("..");
        }

    }
}
