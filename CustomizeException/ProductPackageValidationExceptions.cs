using System;

namespace CustomizeException
{
    public class ProductComboValidationExceptions : Exception
    {
        public ProductComboValidationExceptions() : base("Error in customer Data")
        {
        }

        public ProductComboValidationExceptions(string message) : base(message)
        {
        }

        public ProductComboValidationExceptions(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
