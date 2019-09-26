using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supercon.Model;

namespace Supercon.Service
{
    /// <summary>
    /// This class manage the APIs Response for any  request
    /// </summary>
    public class ResponseService
    {
        public string code;
        public string messaje;

        /// <summary>
        /// Class contructor with default values OK
        /// </summary>
        public ResponseService()
        {
            code = "OK";
            messaje = "PROCESS OK";
        }

        public virtual void SetErrorResponse(string _message = "GENERAL SYSTEM ERROR")
        {
            code = "ERROR";
            messaje = _message;
        }

        public virtual void SetOKResponse(string _message = "PROCESS OK")
        {
            code = "OK";
            messaje = _message;
        }

    }

    public class ShoppingCartResponseService: ResponseService
    {
        private List<Product> products;

        public ShoppingCartResponseService() : base()
        {
            products = new List<Product>();
        }

        public override void SetErrorResponse(string _message = "SHOPPING CART MANGER ERROR")
        {
            messaje = _message;
            code = "ERROR";
        }

        public void SetProducts(List<Product> products)
        {
            this.products = products;
        }

    }

    public class ProductComboResponseService : ResponseService
    {
        private List<Product> products;

        public ProductComboResponseService() : base()
        {
            products = new List<Product>();
        }

        public override void SetErrorResponse(string _message = "SHOPPING CART MANGER ERROR")
        {
            messaje = _message;
            code = "ERROR";
        }

        public void SetProducts(List<Product> products)
        {
            this.products = products;
        }

    }

}
