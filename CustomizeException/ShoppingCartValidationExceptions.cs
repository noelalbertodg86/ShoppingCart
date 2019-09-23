using System;

namespace CustomizeException
{
    public class ShoppingCartValidationExceptions : Exception
    {
        public ShoppingCartValidationExceptions() : base("Error in shopping cart Data")
        {
        }

        public ShoppingCartValidationExceptions(string message) : base(message)
        {
        }

        public ShoppingCartValidationExceptions(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
