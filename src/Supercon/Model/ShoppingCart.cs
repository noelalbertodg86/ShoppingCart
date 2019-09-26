using System.Collections.Generic;
using Supercon.Service;

namespace Supercon.Model
{

    public class ShoppingCart
    {
        //Product and quantity
        private IList<Product> products;
        private IList<ProductPackage> productPackages;
        public Customer customer { get; }
        public string cartState { get; }

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

        public void AddProductPackage(ProductPackage product)
        {
            productPackages.Add(product);
        }

        public void RemoveProductPackage(ProductPackage product)
        {
            productPackages.Remove(product);
        }

        public IList<ProductPackage> GetProductPackage()
        {
            return productPackages;
        }
    }
}
