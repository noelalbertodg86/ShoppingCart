using System;

namespace Supercon.CustomizeException
{
    public class ProductsValidationExceptions: Exception
    {
        public ProductsValidationExceptions() : base("Error in products Data")
        {
        }

        public ProductsValidationExceptions(string message) : base(message)
        {
        }

        public ProductsValidationExceptions(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
