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

        public void SetOrderService(OrderService orderService)
        {
            this.orderService = orderService;
        }

        private OrderService orderService = new OrderService();

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


        /*
            Checkout: Calculates total price and total loyalty points earned by the customer.
            Products with product code starting with DIS_10 have a 10% discount applied.
            Products with product code starting with DIS_15 have a 15% discount applied.

            Loyalty points are earned more when the product is not under any offer.
                Customer earns 1 point on every $5 purchase.
                Customer earns 1 point on every $10 spent on a product with 10% discount.
                Customer earns 1 point on every $15 spent on a product with 15% discount.
        */

        public void Checkout()
        {
            double totalPrice = 0;

            int loyaltyPointsEarned = 0;
            foreach (Product product in products)
            {
                double discount = 0;
                if (product.ProductCode.StartsWith("DIS_10", System.StringComparison.OrdinalIgnoreCase))
                {
                    discount = (product.Price * 0.1);
                    loyaltyPointsEarned += (int)(product.Price / 10);
                }
                else if (product.ProductCode.StartsWith("DIS_15", System.StringComparison.OrdinalIgnoreCase))
                {
                    discount = (product.Price * 0.15);
                    loyaltyPointsEarned += (int)(product.Price / 15);
                }
                else
                {
                    loyaltyPointsEarned += (int)(product.Price / 5);
                }

                totalPrice += product.Price - discount;
            }

            orderService.ShowConfirmation(customer, products, totalPrice, loyaltyPointsEarned);
        }

    }
}
