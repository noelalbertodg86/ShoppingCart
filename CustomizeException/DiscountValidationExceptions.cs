using System;

namespace CustomizeException
{
    public class DiscountValidationExceptions : Exception
    {
        public DiscountValidationExceptions() : base("Error in customer Data")
        {
        }

        public DiscountValidationExceptions(string message) : base(message)
        {
        }

        public DiscountValidationExceptions(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
