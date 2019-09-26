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

    public class DiscountController : Controller
    {
        private ProductService productService;
        private ProductPackageService productComboService;
        private DiscountService discountService;
        private ResponseService responseTemplate;
        private ITraceLogs traceLogs = new EventViewerTraceLogs("ShoppingCart");

        public DiscountController()
        {
            productService = new ProductService();
            productComboService = new ProductPackageService();
            discountService = new DiscountService();
        }

        [HttpPost("discount/addDiscount")]
        public ResponseService AddDiscount(Discount discount)
        {
            try
            {
                discountService.DiscountDataValidation(discount);
                discountService.CreateDiscount(discount);
                // Return ok value using the generic return class
                responseTemplate.SetOKResponse("Discount created successfully");
                return responseTemplate;
            }
            catch (DiscountValidationExceptions e)
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

        [HttpPost("discount/deleteDiscount")]
        public ResponseService DeleteDiscount(Discount discount)
        {
            try
            {
                discountService.DiscountDataValidation(discount);
                discountService.DeleteDiscount(discount);
                // Return ok value using the generic return class
                responseTemplate.SetOKResponse("Discount deleted successfully");
                return responseTemplate;
            }
            catch (DiscountValidationExceptions e)
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


        [HttpPost("discount/setDiscountToProduct")]
        public ResponseService SetDiscountToProduct(Product product, Discount discount)
        {
            try
            {
                this.productService.ProductDataValidation(product);
                this.discountService.DiscountDataValidation(discount);

                this.productService.SetDiscount(product, discount);
                // Return ok value using the generic return class
                responseTemplate.SetOKResponse("Discount add successfully to product.");
                return responseTemplate;
            }
            catch (DiscountValidationExceptions e)
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

        [HttpPost("discount/setDiscountToCombo")]
        public ResponseService SetDiscountToCombo(ProductPackage combo, Discount discount)
        {
            try
            {
                this.productComboService.ComboDataValidation(combo);
                this.discountService.DiscountDataValidation(discount);

                this.productComboService.SetComboDiscount(combo, discount);
                // Return ok value using the generic return class
                responseTemplate.SetOKResponse("Discount add successfully to combo.");
                return responseTemplate;
            }
            catch (DiscountValidationExceptions e)
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
                responseTemplate.SetErrorResponse(_message: "GENERAL ERROR REMOVING PRODUCT FROM COMBO");
                return responseTemplate;
            }
        }
    }
}
