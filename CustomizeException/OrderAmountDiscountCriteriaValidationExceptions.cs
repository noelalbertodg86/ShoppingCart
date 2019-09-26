using System;

namespace CustomizeException
{
    public class OrderAmountDiscountCriteriaValidationExceptions : Exception
    {
        public OrderAmountDiscountCriteriaValidationExceptions() : base("Error in products Data")
        {
        }

        public OrderAmountDiscountCriteriaValidationExceptions(string message) : base(message)
        {
        }

        public OrderAmountDiscountCriteriaValidationExceptions(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
