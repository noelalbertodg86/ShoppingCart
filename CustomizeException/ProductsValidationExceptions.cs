using System;

namespace CustomizeException
{
    public class CustomerValidationExceptions : Exception
    {
        public CustomerValidationExceptions() : base("Error in customer Data")
        {
        }

        public CustomerValidationExceptions(string message) : base(message)
        {
        }

        public CustomerValidationExceptions(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
