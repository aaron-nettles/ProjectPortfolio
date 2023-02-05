using System;
using Xamarin.Forms;
using EcommerceTemplate.Models;
using System.Collections.Generic;
using EcommerceTemplate.Services;
using EcommerceTemplate.Views;
using EcommerceTemplate.Resources;

namespace EcommerceTemplate.ViewModels
{
    [QueryProperty(nameof(AddressId), nameof(AddressId))]
    public class CheckoutPaymentViewModel : BaseViewModel
    {
        IService service => DependencyService.Get<IService>();

        public Command CompleteCommand { get; }

        private string addressId;
        public string AddressId
        {
            get => addressId;
            set
            {
                addressId = value;
                LoadAddress(value);
            }
        }

        private string cardOwner;
        public string CardOwner
        {
            get => cardOwner;
            set => SetProperty(ref cardOwner, value);
        }

        private string cardNumber;
        public string CardNumber
        {
            get => cardNumber;
            set => SetProperty(ref cardNumber, value);
        }

        private string month;
        public string Month
        {
            get => month;
            set => SetProperty(ref month, value);
        }

        private string year;
        public string Year
        {
            get => year;
            set => SetProperty(ref year, value);
        }

        private string cvc;
        public string Cvc
        {
            get => cvc;
            set => SetProperty(ref cvc, value);
        }

        private Address shippingAddress;
        public Address ShippingAddress
        {
            get => shippingAddress;
            set => SetProperty(ref shippingAddress, value);
        }

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

        private IEnumerable<CartItem> cartLines { get; }

        public CheckoutPaymentViewModel()
        {
            Title = AppResources.Payment;

            CompleteCommand = new Command(OnCompleteTapped);

            cartLines = service.GetCartItemsAsync().Result;

            foreach (var item in cartLines) 
                total += item.Total;
        }

        async void LoadAddress(string id)
        {
            var a = await service.GetAddressAsync(id);
            ShippingAddress = a;
        }

        async void OnCompleteTapped()
        {
            var order = await service.AddOrderAsync(new Order
            {
                Id = Guid.NewGuid().ToString(),
                CustomerId = Globals.LoggedCustomerId,
                DateGmt = DateTime.Now,
                BillingAddress = service.GetAddressAsync(addressId).Result,
                ShippingAddress = service.GetAddressAsync(addressId).Result,
                Shipping = shipping,
                Status = OrderStatus.Processing
            });

            foreach(var item in cartLines)
                await service.AddOrderItemAsync(new OrderItem
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                });

            await service.DeleteAllCartItemsAsync();

            await Shell.Current.GoToAsync($"{nameof(CheckoutCompletedPage)}");
        }
    }
}
