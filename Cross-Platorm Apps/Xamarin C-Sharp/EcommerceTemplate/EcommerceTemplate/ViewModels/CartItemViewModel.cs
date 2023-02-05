using System;
using EcommerceTemplate.Models;

namespace EcommerceTemplate.ViewModels
{
    public class CartItemViewModel : BaseViewModel
    {
        private string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string productId;
        public string ProductId
        {
            get => productId;
            set => SetProperty(ref productId, value);
        }

        private string productName;
        public string ProductName
        {
            get => productName;
            set => SetProperty(ref productName, value);
        }

        public string variantString;
        public string VariantString
        {
            get => variantString;
            set => SetProperty(ref variantString, value);
        }

        private string productDescription;
        public string ProductDescription
        {
            get => productDescription;
            set => SetProperty(ref productDescription, value);
        }

        private string productImage;
        public string ProductImage
        {
            get => productImage;
            set => SetProperty(ref productImage, value);
        }

        private float unitPrice;
        public float UnitPrice
        {
            get => unitPrice;
            set => SetProperty(ref unitPrice, value);
        }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set
            {
                SetProperty(ref quantity, value);
                OnPropertyChanged(nameof(Total));
            }
        }

        public double Total
        {
            get { return Math.Round(UnitPrice * Quantity, 2); }
        }

        public CartItemViewModel(CartItem item)
        {
            Id = item.Id;
            ProductId = item.ProductId;
            ProductName = item.ProductName;
            VariantString = item.VariantString;
            ProductDescription = item.ProductDescription;
            ProductImage = item.ProductImage;
            UnitPrice = item.UnitPrice;
            Quantity = item.Quantity;
        }

    }
}
