using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supercon.Model
{
    public class OrderAmountDiscountCriteria
    {
        public string uniqueCode { set; get; }
        public double baseAmount{ set; get; }
        public Discount discount { set; get; }

        public OrderAmountDiscountCriteria()
        {
            baseAmount = 0;
            discount = new Discount();
        }
    }
}
