using System.Collections.Generic;

namespace Supercon.Model
{
    public class ProductPackage
    {
        public string code { get; }
        public List<Product> productsList { get; set; }

        public Discount discount { get; set; }

        public ProductPackage(string _code)
        {
            this.code = _code;
            productsList = new List<Product>();
            this.discount = new Discount();
        }


        
    }
}