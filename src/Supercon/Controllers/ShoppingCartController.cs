using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Supercon.Service;
using Supercon.Model;
using CustomizeException;

namespace Supercon.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingCart")]
    public class ShoppingCartController : Controller
    {

        private readonly ShoppingCartService shoppingCartService = new ShoppingCartService();
        private readonly ProductService productService = new ProductService();
        private Customer customer = null;


        [HttpPost("shopingCart/checkout")]
        public ResponseService Checkout(Customer customer)
        {
            var response = new ShoppingCartResponseService();
            try
            {
                IList<Product> products = new List<Product>();
                this.customer = customer;
                shoppingCartService.Checkout(this.customer, products);

                // Return ok value using the generic return class
                response.SetOKResponse("Shopping cart started successfully");
                return response;
            }
            catch (CustomerValidationExceptions e)
            {
                response.SetErrorResponse(e.Message);
                return response;
            }
            catch (Exception e)
            {
                response.SetErrorResponse();
                return response;
                // implementar clase de Logs y clases especificas de manejo de excepciones

            }
        }

        [HttpPost("shopingCart/addProduct")]
        public ResponseService AddProduct(Product product)
        {
            var response = new ShoppingCartResponseService();
            try
            {
                productService.ProductDataValidation(product);
                shoppingCartService.AddProduct(product);
                // Return ok value using the generic return class
                response.SetOKResponse("Product add successfully");
                return response;
            }
            catch (ProductsValidationExceptions e)
            {
                response.SetErrorResponse(e.Message);
                return response;
            }
            catch (Exception e)
            {
                response.SetErrorResponse("Error adding product to the shopping cart");
                return response;
                // implementar clase de Logs y clases especificas de manejo de excepciones

            }
        }

        [HttpPost("shopingCart/removeProduct")]
        public ResponseService RemoveProduct(Product product)
        {
            var response = new ShoppingCartResponseService();
            try
            {
                productService.ProductDataValidation(product);
                shoppingCartService.RemoveProduct(product);
                // Return ok value using the generic return class
                response.SetOKResponse("Product remove successfully");
                return response;
            }
            catch (ProductsValidationExceptions e)
            {
                response.SetErrorResponse(e.Message);
                return response;
            }
            catch (Exception e)
            {
                response.SetErrorResponse("Error removing product from shopping cart");
                return response;
                // implementar clase de Logs y clases especificas de manejo de excepciones

            }
        }

        [HttpGet("shopingCart/getProducts")]
        public ResponseService GetProducts()
        {
            var response = new ShoppingCartResponseService();
            try
            {
                // Return ok value using the generic return class
                response.SetProducts(shoppingCartService.GetProducts());
                return response;
            }
            catch (Exception e)
            {
                response.SetErrorResponse("Error getting product from shopping cart");
                return response;
                // implementar clase de Logs y clases especificas de manejo de excepciones

            }
        }



    }
}