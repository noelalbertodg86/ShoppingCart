using System.Collections.Generic;
using Supercon.Model;
using System.Linq;
using CustomizeException;

namespace Supercon.Service
{
    public class DiscountService
    {
        private List<Discount> discounts;

        public DiscountService()
        {
            discounts = new List<Discount>();
        }

        public void CreateDiscount(Discount discount)
        {
            discounts.Add(discount);
        }

        public void CreateDiscount(string code, double value, bool isByPercent)
        {
            discounts.Add(new Discount(value, isByPercent, code));
        }


        public void DeleteDiscount(Discount discount)
        {
            discounts.Remove(discount);
        }
        public void DeleteDiscount(string code)
        {
            Discount todelete = discounts.Where(p => p.code == code).FirstOrDefault();
            discounts.Remove(todelete);
        }

        public void DiscountDataValidation(Discount discount)
        {
            if (string.IsNullOrEmpty(discount.code)) { throw new DiscountValidationExceptions("The discount code cannot be null or empty"); }
            if (discount.value <= 0) { throw new DiscountValidationExceptions("The discount value must be greater than zero(0)"); }
        }



    }
    public interface IDiscountValueManager
    {
        double CalculateDiscount();
    }

    public class ProductDiscountValueManager: IDiscountValueManager
    {
        private List<Product> products = new List<Product>();

        public ProductDiscountValueManager(List<Product> _products)
        {
            this.products = _products;
        }

        public double CalculateDiscount()
        {
            double discount = 0;
            foreach (Product p in this.products)
            {
                Discount productDiscount = p.discount;
                if (productDiscount != null)
                {
                    discount += (productDiscount.isPercentDiscount ? p.Price * (productDiscount.value / 100) : productDiscount.value);
                }
            }
            return discount;
        }
    }


    public class ProductPackageDiscountValueManager : IDiscountValueManager
    {
        private List<ProductPackage> productPackage = new List<ProductPackage>();

        public ProductPackageDiscountValueManager(List<ProductPackage> _productsPackage)
        {
            this.productPackage = _productsPackage;
        }

        public double CalculateDiscount()
        {
            double discount = 0;
            foreach (ProductPackage pk in this.productPackage)
            {
                Discount productPackageDiscount = pk.discount;
                if (productPackageDiscount != null)
                {
                    double packageAmount = 0;
                    foreach (Product p in pk.productsList)
                    {
                        packageAmount += p.Price;
                    }
                    discount += (productPackageDiscount.isPercentDiscount ? packageAmount * (productPackageDiscount.value / 100) : productPackageDiscount.value);
                }
            }
            return discount;
        }
    }

    public class OrderDiscountValueManager : IDiscountValueManager
    {
        private double totalOrderAmount;
        OrderAmountDiscountCriteriaService criteria;

        public OrderDiscountValueManager(double _totalOrderAmount)
        {
            this.totalOrderAmount = _totalOrderAmount;
            this.criteria = new OrderAmountDiscountCriteriaService();
        }



        public double CalculateDiscount()
        {
            double orderDiscount = 0;
            OrderAmountDiscountCriteria criteria = this.criteria.GetOrderAmountDiscountByTotalAmount(this.totalOrderAmount);
            if(criteria != null)
            {
                orderDiscount = criteria.discount.isPercentDiscount ? totalOrderAmount * (criteria.discount.value / 100) : criteria.discount.value;
            }
            return orderDiscount;
        }
    }

}