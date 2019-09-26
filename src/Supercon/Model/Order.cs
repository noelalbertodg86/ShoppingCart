using System.Collections.Generic;
using Supercon.Model;

namespace Supercon.Model
{
    public class Order: ShoppingCart
    {
        private Discount discount;
        public Order(Customer customer, IList<Product> products):base(customer, products, "ORDER_PLACED")
        {
            discount = new Discount();
        }
    }
}
