using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supercon.Model;
using CustomizeException;

namespace Supercon.Service
{
    public class OrderAmountDiscountCriteriaService
    {
        List<OrderAmountDiscountCriteria> orderAmountDiscountCriterias = new List<OrderAmountDiscountCriteria>();

        public OrderAmountDiscountCriteriaService()
        {
            this.orderAmountDiscountCriterias = new List<OrderAmountDiscountCriteria>();
        }

        public void AddAmountCriteria(OrderAmountDiscountCriteria orderAmountDiscountCriteria)
        {
            this.orderAmountDiscountCriterias.Add(orderAmountDiscountCriteria);
        }

        public void DeleteAmountCriteria(OrderAmountDiscountCriteria orderAmountDiscountCriteria)
        {
            this.orderAmountDiscountCriterias.Remove(orderAmountDiscountCriteria);
        }

        public List<OrderAmountDiscountCriteria> GetAllOrderAmountDiscountCriteria()
        {
            return this.orderAmountDiscountCriterias.Where(p=>p.discount != null).ToList();
        }

        public OrderAmountDiscountCriteria GetOrderAmountDiscountCriteria(string uniqueCode)
        {
            return this.orderAmountDiscountCriterias.Where(p => p.uniqueCode == uniqueCode).FirstOrDefault();
        }

        public void OrderAmountDiscountCriteriaDataValidation(OrderAmountDiscountCriteria orderAmountDiscountCriteria)
        {
            if (string.IsNullOrEmpty(orderAmountDiscountCriteria.uniqueCode)){ throw new OrderAmountDiscountCriteriaValidationExceptions("The Unique Code cannot be null or empty"); }
            if (orderAmountDiscountCriteria.baseAmount <= 0){ throw new OrderAmountDiscountCriteriaValidationExceptions("The Amount Base cannot be zero [0]."); }
            if (this.orderAmountDiscountCriterias.Where(p => p.uniqueCode == orderAmountDiscountCriteria.uniqueCode).ToList().Count > 0)
            { throw new OrderAmountDiscountCriteriaValidationExceptions("The entry Unique Code already exists "); }

        }

        public OrderAmountDiscountCriteria GetOrderAmountDiscountByTotalAmount(double orderAmount)
        {
            OrderAmountDiscountCriteria discountCriteria = null;
            double valueTemp = orderAmount;
            foreach (OrderAmountDiscountCriteria criteria in GetAllOrderAmountDiscountCriteria())
            {
                if(criteria.baseAmount <= orderAmount)
                {
                    double difference = (orderAmount - criteria.baseAmount);
                    if (difference < valueTemp)
                    {
                        valueTemp = difference;
                        discountCriteria = criteria;
                    }
                }
            }
            return discountCriteria;
        }

    }
}
