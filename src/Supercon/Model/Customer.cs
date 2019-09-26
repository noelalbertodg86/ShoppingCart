namespace Supercon.Model
{
    public class Customer
    {
        public string name { get; }
        public int loyaltyPoints { get; set; }

        public Customer(string name)
        {
            this.name = name;
        }

    }
}
