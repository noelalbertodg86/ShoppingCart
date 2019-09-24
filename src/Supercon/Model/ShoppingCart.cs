using System.Collections.Generic;
using Supercon.Service;

namespace Supercon.Model
{

    public class ShoppingCart
    {
        //Product and quantity
        private IList<Product> products;
        private Customer customer;
        private string cartState;

        public ShoppingCart(Customer customer, IList<Product> products, string cartState)
        {
            this.customer = customer;
            this.products = products;
            this.cartState = cartState;
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

        public IList<Product> GetProducts()
        {
            return products;
        }

        public Customer Customer { get; set; }
        public IList<Product> Products { get; set; }

    }
}
