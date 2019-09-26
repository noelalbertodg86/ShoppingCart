using System.Collections.Generic;
using Supercon.Model;

namespace Supercon.Service
{
    public class ShoppingCartService
    {
        private ShoppingCart cart;
        private OrderService orderService = new OrderService();

        public ShoppingCartService(Customer customer, IList<Product> products)
        {
            this.cart = new ShoppingCart(customer, products, "OPEN");
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

        public void AddProductPackage(ProductPackage product)
        {
            this.cart.AddProductPackage(product);
        }

        public void RemoveProduct(ProductPackage product)
        {
            this.cart.RemoveProductPackage(product);
        }

        public List<ProductPackage> GetProductsPackage()
        {
            return (List<ProductPackage>)this.cart.GetProductPackage();
        }

        public void SetOrderService(OrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Checkout: Calculates total price and total loyalty points earned by the customer.
        /// </summary>
        public void CheckoutV2()
        {
            //total value of shoping cart
            double totalPrice = GetProductsTotalPrice() + GetProductsPackageTotalPrice();

            // save the loyalty points earned
            int loyaltyPointsEarned = GetLoyaltyPoints();
            this.cart.customer.loyaltyPoints = loyaltyPointsEarned;

            // call the shopping confirmation
            orderService.ShowConfirmation(this.cart.customer, this.GetProducts(), GetProductsPackage(),totalPrice, loyaltyPointsEarned);
        }

        /// <summary>
        /// Loyalty points are earned more when the product is not under any offer.
        /// Customer earns 1 point on every $5 purchase.
        /// Customer earns 1 point on every $10 spent on a product with 10% discount.
        /// Customer earns 1 point on every $15 spent on a product with 15% discount.
        /// </summary>
        /// <returns></returns>
        public int GetLoyaltyPoints()
        {
            int loyaltyPointsEarned = 0;
            foreach (Product product in GetProducts())
            {
                if (product.discount.value == 10 && product.discount.isPercentDiscount)
                {
                    loyaltyPointsEarned += (int)(product.Price / 10);
                }
                else if (product.discount.value == 15 && product.discount.isPercentDiscount)
                {
                    loyaltyPointsEarned += (int)(product.Price / 15);
                }
                else
                {
                    loyaltyPointsEarned += (int)(product.Price / 5);
                }
            }
            return loyaltyPointsEarned;
        }

        /// <summary>
        /// Calculate the value of all products in the shopping cart and subtract the discount if necessary
        /// </summary>
        /// <returns></returns>
        public double GetProductsTotalPrice()
        {
            double totalPrice = 0;
            foreach (Product product in GetProducts())
            {
                totalPrice += product.Price;
            }

            double discount = new ProductDiscountValueManager(GetProducts()).CalculateDiscount();
            return totalPrice - discount;
        }

        /// <summary>
        /// Calculate the value of all products package in the shopping cart and subtract the discount if necessary
        /// </summary>
        /// <returns></returns>
        public double GetProductsPackageTotalPrice()
        {
            double totalPrice = 0;
            
            foreach (ProductPackage package in GetProductsPackage())
            {
                foreach (Product product in package.productsList)
                {
                    totalPrice += product.Price;
                }
                
            }
            double discount = new ProductPackageDiscountValueManager(GetProductsPackage()).CalculateDiscount();
            return totalPrice-discount;
        }


        /// <summary>
        /// Checkout: Calculates total price and total loyalty points earned by the customer.
        /// Products with product code starting with DIS_10 have a 10% discount applied.
        /// Products with product code starting with DIS_15 have a 15% discount applied.
        /// Loyalty points are earned more when the product is not under any offer.
        /// Customer earns 1 point on every $5 purchase.
        /// Customer earns 1 point on every $10 spent on a product with 10% discount.
        /// Customer earns 1 point on every $15 spent on a product with 15% discount.
        /// </summary>
        [System.Obsolete("Checkout method is deprecated, please use CheckoutV2 instead.")]
        public void Checkout()
        {
            double totalPrice = 0;
            int loyaltyPointsEarned = 0;

            foreach (Product product in GetProducts())
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

            orderService.ShowConfirmation(this.cart.customer, this.GetProducts(), GetProductsPackage(), totalPrice, loyaltyPointsEarned);
        }
    }
}