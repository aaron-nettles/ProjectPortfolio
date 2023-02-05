using System.Collections.Generic;
using EcommerceTemplate.Models;

namespace EcommerceTemplate.DataStores.MockDataStore
{
    /// <summary>
    /// Mock data store with fake entities to test.
    /// </summary>
    public class OrderItemDataStore : BaseDataStore<OrderItem>, IOrderItemDataStore
    {
        protected override IList<OrderItem> items { get; }

        public OrderItemDataStore()
        {
            items = new List<OrderItem>
            {
                new OrderItem { Id = "ol001", OrderId = "or001", ProductId = "p001", UnitPrice = 18f, Quantity = 1 },

                new OrderItem { Id = "ol002", OrderId = "or001", ProductId = "p002", UnitPrice = 55f, Quantity = 2 },

                new OrderItem { Id = "ol003", OrderId = "or002", ProductId = "p003", UnitPrice = 16f, Quantity = 3 },

                new OrderItem { Id = "ol004", OrderId = "or003", ProductId = "p005", UnitPrice = 35f, Quantity = 2 },

                new OrderItem { Id = "ol005", OrderId = "or004", ProductId = "p005", UnitPrice = 30f, Quantity = 1 }
            };
        }
    }
}
