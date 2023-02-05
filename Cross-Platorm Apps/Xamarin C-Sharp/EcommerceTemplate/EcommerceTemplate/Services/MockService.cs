using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceTemplate.DataStores;
using EcommerceTemplate.Models;
using Xamarin.Forms;

namespace EcommerceTemplate.Services
{
    /// <summary>
    /// Mock service that works with mock data stores for testing.
    /// </summary>
    public class MockService : IService
    {
        ICustomerDataStore dataCustomer = DependencyService.Get<ICustomerDataStore>();
        IAddressDataStore dataAddress => DependencyService.Get<IAddressDataStore>();
        ICartItemDataStore dataCartItem => DependencyService.Get<ICartItemDataStore>();
        IProductDataStore dataProduct => DependencyService.Get<IProductDataStore>();
        ICategoryDataStore dataCategory => DependencyService.Get<ICategoryDataStore>();
        IOrderDataStore dataOrder => DependencyService.Get<IOrderDataStore>();
        IOrderItemDataStore dataOrderItem => DependencyService.Get<IOrderItemDataStore>();
        IRatingDataStore dataRating => DependencyService.Get<IRatingDataStore>();
        IFavoriteDataStore dataFavorite => DependencyService.Get<IFavoriteDataStore>();
        IBannerDataStore dataBanner => DependencyService.Get<IBannerDataStore>();
        IVariantDataStore dataVariant => DependencyService.Get<IVariantDataStore>();

        // Methods for Customer entity

        public async Task<Customer> GetCustomerAsync(string id)
        {
            return await dataCustomer.GetAsync(id);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            return await dataCustomer.UpdateAsync(customer);
        }

        // Methods for Address entity

        public async Task<Address> GetAddressAsync(string id)
        {
            return await dataAddress.GetAsync(id);
        }

        public async Task<IEnumerable<Address>> GetAddressesAsync(string customerId)
        {
            return (await dataAddress.GetByAsync(i => i.CustomerId == customerId))
                        .OrderBy(i => i.Title);
        }

        public async Task<bool> DeleteAddressAsync(string id)
        {
            return await dataAddress.DeleteAsync(id);
        }

        public async Task<Address> AddAddressAsync(Address address)
        {
            return await dataAddress.AddAsync(address);
        }

        public async Task<bool> UpdateAddressAsync(Address address)
        {
            return await dataAddress.UpdateAsync(address);
        }

        // Methods for CartItem entity

        public async Task<CartItem> GetCartItemAsync(string id)
        {
            return await dataCartItem.GetAsync(id);
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            var oldItem = dataCartItem.GetByAsync(i => i.ProductId == cartItem.ProductId &&
                                                        i.VariantId == cartItem.VariantId &&
                                                        i.UnitPrice == cartItem.UnitPrice)
                            .Result.FirstOrDefault();

            if (oldItem == null)
                return await dataCartItem.AddAsync(cartItem);
            else
            {
                oldItem.Quantity += cartItem.Quantity;
                await dataCartItem.UpdateAsync(oldItem);
                return oldItem;
            }
        }

        public async Task<bool> UpdateCartItemAsync(CartItem cartItem)
        {
            return await dataCartItem.UpdateAsync(cartItem);
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            var result = (await dataCartItem.GetAllAsync())
                            .Select(i => {
                                var p = dataProduct.GetAsync(i.ProductId).Result;
                                i.ProductName = p.Name;
                                i.ProductDescription = p.Description;
                                i.ProductImage = p.Images[0];
                                i.VariantString = dataVariant.GetAsync(i.VariantId).Result?.ToString();
                                return i;
                            });

            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteCartItemAsync(string id)
        {
            return await dataCartItem.DeleteAsync(id);
        }

        public async Task<bool> DeleteAllCartItemsAsync()
        {
            return await dataCartItem.DeleteAllAsync();
        }

        public async Task<bool> IsInCartAsync(string productId)
        {
            var result = (await dataCartItem.GetAllAsync())
                            .Any(i => i.ProductId == productId);

            return await Task.FromResult(result);
        }

        public async Task<int> GetQuantityInCartAsync(string productId)
        {
            var result = (await dataCartItem.GetAllAsync())
                            .Where(i => i.ProductId == productId).Sum(s => s.Quantity);

            return await Task.FromResult(result);
        }

        // Methods for Category entity

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string name)
        {
            var result = await dataCategory.GetByAsync(i => name == null || i.Name.Contains(name));

            return await Task.FromResult(result);
        }

        // Methods for Order entity

        public async Task<Order> GetOrderAsync(string id)
        {
            return await dataOrder.GetAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string customerId)
        {
            var result = (await dataOrder.GetByAsync(o => o.CustomerId == customerId)).ToList()
                            .Select(o => {
                                o.Total = 0;
                                dataOrderItem.GetByAsync(l =>
                                    l.OrderId == o.Id).Result.ToList().ForEach(li => o.Total += li.Total);
                                return o;
                            });

            return await Task.FromResult(result);
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            return await dataOrder.AddAsync(order);
        }

        // Methods for OrderItem entity

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(string orederId)
        {
            var result = (await dataOrderItem.GetByAsync(s => s.OrderId == orederId)).ToList()
                                .Select(i =>
                                {
                                    var p = dataProduct.GetAsync(i.ProductId).Result;
                                    i.ProductName = p.Name;
                                    i.ProductDescription = p.Description;
                                    i.ProductImage = p.Images[0];
                                    return i;
                                });

            return await Task.FromResult(result);
        }

        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
        {
            return await dataOrderItem.AddAsync(orderItem);
        }

        // Methods for Product entity

        public async Task<Product> GetProductAsync(string id)
        {
            return await dataProduct.GetAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string[] categoriIds = null, string[] tags = null,
                                                    string[] productIds = null, string[] colors = null,
                                                    string[] materials = null, string key = null,
                                                    bool onlyFavorite = false, bool onlyNew = false,
                                                    bool onlyFeatured = false, bool onlyPopular = false,
                                                    SortBy sortBy = SortBy.Unsorted)
        {
            var result = (await dataProduct.GetByAsync(p => (categoriIds == null || p.CategoryIds.Any(c => categoriIds.Contains(c))) &&
                                           (tags == null || p.Tags.Any(t => tags.Contains(t))) &&
                                           (productIds == null || productIds.Contains(p.Id)) &&
                                           (colors == null || colors.Any(c => c == p.Color)) &&
                                           (materials == null || materials.Any(m => m == p.Material)) &&
                                           (key == null || p.Name.ToLower().Contains(key.ToLower())) &&
                                           (onlyNew == false || p.IsNew == true) &&
                                           (onlyFeatured == false || p.IsFeatured == true) &&
                                           (onlyPopular == false || p.IsPopular == true))).ToList()
                                .Select(i =>
                                {
                                    i.RatingCount = dataRating.GetByAsync(r => r.ProductId == i.Id).Result.Count();
                                    i.AverageRating = i.RatingCount > 0 ?
                                        (float)dataRating.GetByAsync(r =>
                                            r.ProductId == i.Id).Result.Sum(r => r.Star) / i.RatingCount : 0;
                                    i.IsFavorite = IsFavoriteAsync(Globals.LoggedCustomerId, i.Id).Result;
                                    i.QtyInCart = GetQuantityInCartAsync(i.Id).Result;
                                    i.IsSimple = IsSimpleItemAsync(i.Id).Result;
                                    return i;
                                }).Where(p => (onlyFavorite == false || p.IsFavorite == true));

            switch (sortBy)
            {
                case SortBy.Name_AZ: result = result.OrderBy(i => i.Name); break;
                case SortBy.Name_ZA: result = result.OrderByDescending(i => i.Name); break;
                case SortBy.Price_High: result = result.OrderByDescending(i => i.Price); break;
                case SortBy.Price_Low: result = result.OrderBy(i => i.Price); break;
                case SortBy.Highest_Rate: result = result.OrderByDescending(i => i.AverageRating); break;
                case SortBy.Rate_Count: result = result.OrderByDescending(i => i.RatingCount); break;
            };

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<string>> GetAllTagsAsync()
        {
            return (await dataProduct.GetAllAsync()).SelectMany(i => i.Tags).Distinct().OrderBy(t => t);
        }

        public async Task<IEnumerable<string>> GetAllColorsAsync()
        {
            return (await dataProduct.GetAllAsync()).Select(i => i.Color).Distinct().OrderBy(c => c);
        }

        public async Task<IEnumerable<string>> GetAllMaterialsAsync()
        {
            return (await dataProduct.GetAllAsync()).Select(i => i.Material).Distinct().OrderBy(m => m);
        }

        // Methods for Rating entity

        public async Task<Rating> AddRatingAsync(Rating rating)
        {
            return await dataRating.AddAsync(rating);
        }

        public async Task<IEnumerable<Rating>> GetRatingsAsync(string productId)
        {
            return (await dataRating.GetByAsync(r => r.ProductId == productId)).ToList()
                            .Select(i => {
                                i.CustomerFullName = dataCustomer.GetAsync(i.CustomerId).Result.FullName;
                                return i;
                            }).OrderBy(t => t.DateGmt);
        }

        // Methods for Banner entity

        public async Task<IEnumerable<Banner>> GetBannersAsync()
        {
            return await dataBanner.GetAllAsync();
        }

        // Methods for Favorite entity

        public async Task<Favorite> AddFavoriteAsync(Favorite favorite)
        {
            return await dataFavorite.AddAsync(favorite);
        }

        public async Task<bool> DeleteFavoriteAsync(string id)
        {
            return await dataFavorite.DeleteAsync(id);
        }

        public async Task<bool> IsFavoriteAsync(string customerId, string productId)
        {
            return await dataFavorite.IsExistAsync(f => f.CustomerId == customerId && f.ProductId == productId);
        }

        public async Task<Favorite> GetFavoriteAsync(string customerId, string productId)
        {
            return (await dataFavorite.GetByAsync(f => f.CustomerId == customerId && f.ProductId == productId))
                        .FirstOrDefault();
        }

        // Methods for Variant entity

        public async Task<IEnumerable<Variant>> GetVariantsAsync(string name, string productId)
        {
            return await dataVariant.GetByAsync(i => i.Name == name && i.ProductId == productId);
        }

        public async Task<bool> IsSimpleItemAsync(string productId)
        {
            return await dataVariant.IsExistAsync(i => i.ProductId == productId);
        }

    }
}
