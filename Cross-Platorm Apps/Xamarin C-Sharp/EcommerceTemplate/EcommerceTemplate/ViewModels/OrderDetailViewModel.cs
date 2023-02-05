using System;
using System.Collections.ObjectModel;
using EcommerceTemplate.Models;
using Xamarin.Forms;
using EcommerceTemplate.Services;

namespace EcommerceTemplate.ViewModels
{
    [QueryProperty(nameof(OrderId), nameof(OrderId))]
    public class OrderDetailViewModel : BaseViewModel
    {
        IService service => DependencyService.Get<IService>();

        public ObservableCollection<OrderItem> LineItems { get; }

        private string orderId;
        public string OrderId
        {
            get => orderId;
            set
            {
                SetProperty(ref orderId, value);
                LoadOrder(value);
            }
        }

        private OrderStatus status;
        public OrderStatus Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        private DateTime dateGmt;
        public DateTime DateGmt
        {
            get => dateGmt;
            set => SetProperty(ref dateGmt, value);
        }

        private Address billingAddress;
        public Address BillingAddress
        {
            get => billingAddress;
            set => SetProperty(ref billingAddress, value);
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
            set => SetProperty(ref total, value);
        }

        private double shipping;
        public double Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        private double grandTotal;
        public double GrandTotal
        {
            get => grandTotal;
            set => SetProperty(ref grandTotal, value);
        }

        public OrderDetailViewModel()
        {
            LineItems = new ObservableCollection<OrderItem>();
        }

        public async void LoadOrder(string id)
        {
            var order = await service.GetOrderAsync(id);
            var lineItems = await service.GetOrderItemsAsync(id);

            Status = order.Status;
            DateGmt = order.DateGmt;
            Total = order.Total;
            GrandTotal = order.GrandTotal;
            BillingAddress = order.BillingAddress;
            ShippingAddress = order.ShippingAddress;

            foreach (var item in lineItems)
                LineItems.Add(item);
        }

    }
}
