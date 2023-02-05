using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceTemplate.DataStores;
using EcommerceTemplate.Models;

namespace EcommerceTemplate.Services
{
    /// <summary>
    /// Interface with common tasks
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>Customer object or null</returns>
        Task<Customer> GetCustomerAsync(string id);

        /// <summary>
        /// Update the customer
        /// </summary>
        /// <param name="customer">Customer object</param>
        /// <returns>True, if successful</returns>
        Task<bool> UpdateCustomerAsync(Customer customer);

        // Address
        /// <summary>
        /// Get address by id
        /// </summary>
        /// <param name="id">Address id</param>
        /// <returns>Address object or null</returns>
        Task<Address> GetAddressAsync(string id);

        /// <summary>
        /// Get list of addresses by customer id
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <returns>List of address of customer</returns>
        Task<IEnumerable<Address>> GetAddressesAsync(string customerId);

        /// <summary>
        /// Delete the address
        /// </summary>
        /// <param name="id">Address Id</param>
        /// <returns>True, if successful</returns>
        Task<bool> DeleteAddressAsync(string id);

        /// <summary>
        /// Add the address
        /// </summary>
        /// <param name="address">Address object</param>
        /// <returns>Added address</returns>
        Task<Address> AddAddressAsync(Address address);

        /// <summary>
        /// Update the address
        /// </summary>
        /// <param name="address">Address object</param>
        /// <returns>True, if successful</returns>
        Task<bool> UpdateAddressAsync(Address address);

        /// <summary>
        /// Get cart item by id
        /// </summary>
        /// <param name="id">CartItem id</param>
        /// <returns>CartItem object or null</returns>
        Task<CartItem> GetCartItemAsync(string id);

        /// <summary>
        /// Add the cart item
        /// </summary>
        /// <param name="cartItem">CartItem object</param>
        /// <returns>Added cart item</returns>
        Task<CartItem> AddCartItemAsync(CartItem cartItem);

        /// <summary>
        /// Update the CartItem
        /// </summary>
        /// <param name="cartItem">CartItem object</param>
        /// <returns>True, if successful</returns>
        Task<bool> UpdateCartItemAsync(CartItem cartItem);

        /// <summary>
        /// Get all items in the cart
        /// </summary>
        /// <returns>All CartItems in the cart</returns>
        Task<IEnumerable<CartItem>> GetCartItemsAsync();

        /// <summary>
        /// Delete cart by id
        /// </summary>
        /// <param name="id">CartItem id</param>
        /// <returns>True, if successful</returns>
        Task<bool> DeleteCartItemAsync(string id);

        /// <summary>
        /// Delete all items in the cart
        /// </summary>
        /// <returns>True, if successful</returns>
        Task<bool> DeleteAllCartItemsAsync();

        /// <summary>
        /// Determine that the product is in the cart.
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns>True, if the product has been added to the cart</returns>
        Task<bool> IsInCartAsync(string productId);

        /// <summary>
        /// Determine how many items are in the basket.
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns>Number of the items in the cart</returns>
        Task<int> GetQuantityInCartAsync(string productId);

        /// <summary>
        /// Get categories by name
        /// </summary>
        /// <param name="name">Keyword for category name</param>
        /// <returns>List of categories filtered by name</returns>
        Task<IEnumerable<Category>> GetCategoriesAsync(string name);

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Order object or null</returns>
        Task<Order> GetOrderAsync(string id);

        /// <summary>
        /// Get orders by customer id
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <returns>List of orders of customer</returns>
        Task<IEnumerable<Order>> GetOrdersAsync(string customerId);

        /// <summary>
        /// Add the order object
        /// </summary>
        /// <param name="order">Order object</param>
        /// <returns>Added order object</returns>
        Task<Order> AddOrderAsync(Order order);

        /// <summary>
        /// Get items in the order
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <returns>Items in the order</returns>
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(string orederId);

        /// <summary>
        /// Add order item
        /// </summary>
        /// <param name="orderItem">OrderItem object</param>
        /// <returns>Added order item</returns>
        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);

        /// <summary>
        /// Get the product by id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>Product object or null</returns>
        Task<Product> GetProductAsync(string id);

        /// <summary>
        /// Get products by parameters
        /// </summary>
        /// <param name="categoriIds">Array of the category ids. Default is null.</param>
        /// <param name="tags">Array of the tag names. Default is null.</param>
        /// <param name="productIds">Array of the product ids. Default is null.</param>
        /// <param name="colors">Array of the colors. Default is null.</param>
        /// <param name="materials">Array of the materials. Default is null.</param>
        /// <param name="key">Keyword for product name. Default is null.</param>
        /// <param name="onlyFavorite">Get only favorited items. Default is false.</param>
        /// <param name="onlyNew">Get only new items. Default is false.</param>
        /// <param name="onlyFeatured">Get only featured items. Default is false.</param>
        /// <param name="onlyPopular">Get only popular items. Default is false.</param>
        /// <param name="sortBy">SortBy enumaration. Default is unsorted.</param>
        /// <returns>List of product filtered by parameters</returns>
        Task<IEnumerable<Product>> GetProductsAsync(string[] categoriIds = null, string[] tags = null,
                                              string[] productIds = null, string[] colors = null,
                                              string[] materials = null, string key = null,
                                              bool onlyFavorite = false, bool onlyNew = false,
                                              bool onlyFeatured = false, bool onlyPopular = false,
                                              SortBy sortBy = SortBy.Unsorted);

        /// <summary>
        /// Get all tags in the product data store by distinct
        /// </summary>
        /// <returns>All tag names</returns>
        Task<IEnumerable<string>> GetAllTagsAsync();

        /// <summary>
        /// Get all colors in the product data store by distinct
        /// </summary>
        /// <returns>All color names</returns>
        Task<IEnumerable<string>> GetAllColorsAsync();

        /// <summary>
        /// Get all materials in the product data store by distinct
        /// </summary>
        /// <returns>All material names</returns>
        Task<IEnumerable<string>> GetAllMaterialsAsync();

        /// <summary>
        /// Get list of ratings by product id
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns>List of rating objects filtered by product id</returns>
        Task<IEnumerable<Rating>> GetRatingsAsync(string productId);

        /// <summary>
        /// Add the rating
        /// </summary>
        /// <param name="rating">Rating object</param>
        /// <returns>Added rating object</returns>
        Task<Rating> AddRatingAsync(Rating rating);

        /// <summary>
        /// Get list of all banners
        /// </summary>
        /// <returns>List of banner objects</returns>
        Task<IEnumerable<Banner>> GetBannersAsync();

        /// <summary>
        /// Add the favorite
        /// </summary>
        /// <param name="favorite">Favorite object</param>
        /// <returns>Added favorite object</returns>
        Task<Favorite> AddFavoriteAsync(Favorite favorite);

        /// <summary>
        /// Delete the Favorite
        /// </summary>
        /// <param name="id">Favorite id</param>
        /// <returns>True, if successful</returns>
        Task<bool> DeleteFavoriteAsync(string id);

        /// <summary>
        /// Determine the product is favorite 
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <param name="productId">Product id</param>
        /// <returns>True, if product has been addes to favorites for customer</returns>
        Task<bool> IsFavoriteAsync(string customerId, string productId);

        /// <summary>
        /// Get the Favorite object by parameters
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <param name="productId">Product id</param>
        /// <returns>Favorite object or null</returns>
        Task<Favorite> GetFavoriteAsync(string customerId, string productId);

        /// <summary>
        /// Get list of variants by parameters
        /// </summary>
        /// <param name="name">Name of variant</param>
        /// <param name="productId">Product id</param>
        /// <returns>List of variant objects</returns>
        Task<IEnumerable<Variant>> GetVariantsAsync(string name, string productId);

        /// <summary>
        /// Determine if the product has variants
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns>True, if product has no variants</returns>
        Task<bool> IsSimpleItemAsync(string productId);

    }
}
