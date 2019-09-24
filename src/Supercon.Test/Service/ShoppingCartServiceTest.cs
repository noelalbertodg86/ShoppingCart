using System;
using System.Collections.Generic;
using Supercon.Service;
using Xunit;
using Supercon.Model;
using System.Linq;

namespace Supercon.Test.Service
{
    public class ShoppingCartServiceTest
    {
        private readonly ShoppingCartService shoppingCartService;
        private readonly Product product1;
        private readonly Product product2;

        public ShoppingCartServiceTest()
        {
            IList<Product> productsList = new List<Product>();
            shoppingCartService = new ShoppingCartService(new Customer("Noel"), productsList);


            product1 = new Product(10.50, "PROD_01", "Table");
            product2 = new Product(5.50, "PROD_02", "Chair");
        }

        [Fact]
        public void AddProductToShoppingCart()
        {
            shoppingCartService.AddProduct(product1);
            shoppingCartService.AddProduct(product2);

            Assert.Equal(2, shoppingCartService.GetProducts().Count);
            Assert.Equal("PROD_01", shoppingCartService.GetProducts()[0].ProductCode);
            Assert.Equal("PROD_02", shoppingCartService.GetProducts()[1].ProductCode);
        }

        [Fact]
        public void RemoveProductFromShoppingCart()
        {
            shoppingCartService.AddProduct(product1);
            shoppingCartService.AddProduct(product2);

            shoppingCartService.RemoveProduct(product1);

            Assert.Equal(1, shoppingCartService.GetProducts().Count);
            Assert.Equal("PROD_02", shoppingCartService.GetProducts()[0].ProductCode);
        }

        [Fact]
        public void GetProductFromShoppingCart()
        {
            shoppingCartService.AddProduct(product1);
            shoppingCartService.AddProduct(product2);

            Assert.Equal(2, shoppingCartService.GetProducts().Count);

            Assert.Equal("PROD_01", shoppingCartService.GetProducts()[0].ProductCode);
            Assert.Equal("Table", shoppingCartService.GetProducts()[0].Name);
            Assert.Equal(10.50, shoppingCartService.GetProducts()[0].Price);

            Assert.Equal("PROD_02", shoppingCartService.GetProducts()[1].ProductCode);
            Assert.Equal("Chair", shoppingCartService.GetProducts()[1].Name);
            Assert.Equal(5.50, shoppingCartService.GetProducts()[1].Price);
        }




    }
}
