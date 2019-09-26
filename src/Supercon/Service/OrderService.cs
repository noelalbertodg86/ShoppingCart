using System.Collections.Generic;
using Supercon.Model;

namespace Supercon.Service
{
    public class OrderService
    {
        private Order order;

        public virtual void ShowConfirmation(Customer customer, IList<Product> products, IList<ProductPackage> productsPackage, double totalPrice, int loyaltyPointsEarned)
        {
            double orderDiscount = new OrderDiscountValueManager(totalPrice).CalculateDiscount();
            //show confirmation
            //do some calculations and formatting on the shopping cart data and ask user for confirmation
            //after confirmation redirect to place order

        }

        public ShoppingCart PlaceOrder(Customer customer, IList<Product> products, IList<ProductPackage> productsPackage)
        {
            //place order
            return new Order(customer, products);
        }

    }


}