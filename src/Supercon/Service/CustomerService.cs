using System.Collections.Generic;
using Supercon.Model;
using CustomizeException;

namespace Supercon.Service
{
    public class CustomerService
    {
        private Customer customer = null;

        public CustomerService(string name)
        {
            this.customer = new Customer(name);
        }

        public CustomerService(Customer _customer)
        {
            this.customer = _customer;
        }

        public CustomerService()
        {
            this.customer = null;
        }

        public void CustomerDataValidation(Customer _customer)
        {
            if (string.IsNullOrEmpty(_customer.Name)){ throw new CustomerValidationExceptions("The customer name cannot be null or empty"); }

        }
    }
}