using System.Collections.Generic;

namespace Supercon.Model
{
    public class ProductPackage
    {
        string code;
        List<Product> productsList;

        public ProductPackage(string _code)
        {
            this.code = _code;
            productsList = new List<Product>();
        }

        public List<Product> ProductsList { get; set; }

        public string Code { get; set; }

        
    }
}