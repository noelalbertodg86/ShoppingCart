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
using TraceLogs;

namespace Supercon.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingCart")]
    public class ShoppingCartController : Controller
    {
        private ProductService productService;
        private CustomerService customerService;
        private Customer customer;
        private IList<Product> products;
        private ITraceLogs traceLogs = new EventViewerTraceLogs("ShoppingCart");
        private readonly ShoppingCartService shoppingCartService;

        public ShoppingCartController()
        {
            productService = new ProductService();
            customerService = new CustomerService(customer);
            products = new List<Product>();
            shoppingCartService = new ShoppingCartService(customer, products);
        }

        [HttpPost("shopingCart/checkout")]
        public ResponseService Checkout(Customer customer)
        {
            var response = new ShoppingCartResponseService();
            try
            {
                customerService.CustomerDataValidation(customer);   
                this.customer = customer;
                shoppingCartService.Checkout();

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
                traceLogs.SaveErrorLogs(e);
                response.SetErrorResponse();
                return response;
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
                traceLogs.SaveErrorLogs(e);
                response.SetErrorResponse("Error adding product to the shopping cart");
                return response;
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
                traceLogs.SaveErrorLogs(e);
                response.SetErrorResponse("Error removing product from shopping cart");
                return response;
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
                traceLogs.SaveErrorLogs(e);
                response.SetErrorResponse("Error getting product from shopping cart");
                return response;
            }
        }



    }
}