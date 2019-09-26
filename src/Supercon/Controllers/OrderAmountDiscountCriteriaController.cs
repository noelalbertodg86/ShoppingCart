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

    public class OrderAmountDiscountCriteriaController : Controller
    {
        private OrderAmountDiscountCriteriaService orderAmount;
        private ResponseService responseTemplate;
        private ITraceLogs traceLogs = new EventViewerTraceLogs("ShoppingCart");

        public OrderAmountDiscountCriteriaController()
        {
            orderAmount = new OrderAmountDiscountCriteriaService();
        }

        [HttpPost("discount/addOrderAmountDiscountCriteria")]
        public ResponseService AddOrderAmountDiscountCriteria(OrderAmountDiscountCriteria orderAmount)
        {
            try
            {
                this.orderAmount.OrderAmountDiscountCriteriaDataValidation(orderAmount);
                this.orderAmount.AddAmountCriteria(orderAmount);
                // Return ok value using the generic return class
                responseTemplate.SetOKResponse("Discount order by amount  created successfully");
                return responseTemplate;
            }
            catch (OrderAmountDiscountCriteriaValidationExceptions e)
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
