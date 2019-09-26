using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supercon.Model;
using Supercon.Service;
using CustomizeException;
using TraceLogs;

namespace Supercon.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductCombo")]
    public class ProductPackageController : Controller
    {
        private ProductPackageService productComboService;
        private ResponseService responseTemplate = new ResponseService();
        private ITraceLogs traceLogs = new EventViewerTraceLogs("ShoppingCart");
        private ProductService productService;

        public ProductPackageController()
        {
            this.productComboService = new ProductPackageService();
            this.productService = new ProductService();
        }

        [HttpPost("productCombo/createCombo")]
        public ResponseService CreateCombo(ProductPackage combo)
        {
            try
            {
                this.productComboService.ComboDataValidation(combo);
                this.productComboService.CreateCombo(combo);
                // Return ok value using the generic return class
                responseTemplate.SetOKResponse("Product combo create successfully");
                return responseTemplate;
            }
            catch (ProductComboValidationExceptions e)
            {
                responseTemplate.SetErrorResponse(e.Message);
                return responseTemplate;
            }
            catch (Exception e)
            {
                traceLogs.SaveErrorLogs(e);
                responseTemplate.SetErrorResponse(_message: "GENERAL ERROR CREATING COMBO");
                return responseTemplate;
            }
        }

        [HttpPost("productCombo/addProduct")]
        public ResponseService AddProductToCombo(ProductPackage combo, Product product)
        {
            try
            {
                productService.ProductDataValidation(product);
                this.productComboService.ComboDataValidation(combo);
                this.productComboService.AddProductToCombo(combo,product);
                // Return ok value using the generic return class
                responseTemplate.SetOKResponse("Product add to the combo successfully");
                return responseTemplate;
            }
            catch (ProductsValidationExceptions e)
            {
                responseTemplate.SetErrorResponse(e.Message);
                return responseTemplate;
            }
            catch (ProductComboValidationExceptions e)
            {
                responseTemplate.SetErrorResponse(e.Message);
                return responseTemplate;
            }
            catch (Exception e)
            {
                traceLogs.SaveErrorLogs(e);
                responseTemplate.SetErrorResponse(_message: "GENERAL ERROR ADDING PRODUCT TO COMBO");
                return responseTemplate;
            }
        }

        [HttpPost("productCombo/removeProduct")]
        public ResponseService RemoveProduct(ProductPackage combo, Product product)
        {
            try
            {
                this.productComboService.ComboDataValidation(combo);
                productService.ProductDataValidation(product);
                this.productComboService.RemoveProductFromCombo(combo,product);
                // Return ok value using the generic return class
                responseTemplate.SetOKResponse("Product remove from combo successfully");
                return responseTemplate;
            }
            catch (ProductComboValidationExceptions e)
            {
                responseTemplate.SetErrorResponse(e.Message);
                return responseTemplate;
            }
            catch (ProductsValidationExceptions e)
            {
                responseTemplate.SetErrorResponse(e.Message);
                return responseTemplate;
            }
            catch (Exception e)
            {
                traceLogs.SaveErrorLogs(e);
                responseTemplate.SetErrorResponse(_message: "GENERAL ERROR REMOVING PRODUCT FROM COMBO");
                return responseTemplate;
            }
        }


        [HttpGet("productCombo/getProductsFromCombo")]
        public ResponseService GetProductsFromCombo(ProductPackage combo)
        {
            var responseproductsInCombo = (ProductComboResponseService)responseTemplate;
            try
            {
                this.productComboService.ComboDataValidation(combo);
                responseproductsInCombo.SetProducts(this.productComboService.GetProductsFromCombo(combo));
                // Return ok value using the generic return class
                responseproductsInCombo.SetOKResponse("Product getting successfully from combo ");
                return responseTemplate;
            }
            catch (ProductComboValidationExceptions e)
            {
                responseTemplate.SetErrorResponse(e.Message);
                return responseTemplate;
            }
            catch (Exception e)
            {
                traceLogs.SaveErrorLogs(e);
                responseTemplate.SetErrorResponse(_message: "GENERAL ERROR GETTING PRODUCT FROM COMBO");
                return responseTemplate;
            }
        }

    }
}