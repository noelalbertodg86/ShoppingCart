using System.Collections.Generic;
using Supercon.Model;

namespace Supercon.Service
{
    public class ShoppingCartService
    {
        private ShoppingCart cart;

        public void Checkout(Customer customer, IList<Product> products)
        {
            this.cart = new ShoppingCart(customer, products, "OPEN");
            cart.Checkout();
        }

        public void AddProduct(Product product)
        {
            this.cart.AddProduct(product);
        }

        public void RemoveProduct(Product product)
        {
            this.cart.RemoveProduct(product);
        }

        public List<Product> GetProducts()
        {
            return (List<Product>)this.cart.GetProducts();
        }
    }
}