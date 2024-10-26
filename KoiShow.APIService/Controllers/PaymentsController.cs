using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiShow.Data.Models;
using KoiShow.Service;
using KoiShow.Service.Base;
using KoiShow.Common.DTO.DTORequest;
using Microsoft.AspNetCore.Authorization;
using KoiShow.Common.DTO.DtoResponse;
using KoiShow.Common;

namespace KoiShow.API.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        public PaymentsController(PaymentService paymentService) => _paymentService = paymentService;

        // POST: api/Payments/create
        [HttpPost("create")]
        public async Task<IBusinessResult> CreatePayment([FromBody] PaymentDto paymentDto)
        {
            return await _paymentService.CreatePayment(paymentDto);
        }

        //GET: api/Payments
        [HttpGet]
        public async Task<IBusinessResult> GetAllPaymentsAsync()
        {
            return await _paymentService.GetAllPaymentsAsync();
        }


        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> FindPaymentById(int id)
        {
            return await _paymentService.FindPaymentById(id);
        }

        //GET: api/Payments/search
        [HttpGet("search")]
        public async Task<IBusinessResult> FindPaymentsByStringAsync(string transactionId = null, string description = null, string paymentStatus = null)
        {
            return await _paymentService.FindPaymentsByCriteriaAsync(transactionId, description, paymentStatus);
        }

        // PUT: api/Payments/5/paid
        [HttpPut("{id}/paid")]
        public async Task<IBusinessResult> UpdatePaymentStatusToPaid(int id)
        {
            // Step 1: Update the payment status to "Paid"
            var updateResult = await _paymentService.UpdatePaymentStatusToPaid(id);

            // Check if the update was successful
            if (updateResult.Status == Const.SUCCESS_UPDATE_CODE)
            {
                // Step 2: If the update was successful, generate the payment URL
                var httpContext = HttpContext; // Get the current HttpContext
                var urlGenerationResult = await _paymentService.GeneratePaymentUrl(id, httpContext);

                // Check if URL generation was successful
                if (urlGenerationResult.Status == Const.SUCCESS_CREATE_CODE)
                {
                    // Return both the payment update result and the generated payment URL
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, "Payment status updated and URL generated successfully", new
                    {
                        PaymentUrl = urlGenerationResult
                    });
                }
                else
                {
                    // Return success for payment status update but failure for URL generation
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, "Payment status updated, but failed to generate payment URL", new
                    {
                        Error = urlGenerationResult.Message
                    });
                }
            }
            else
            {
                // Return failure if the payment update failed
                return updateResult;
            }
        }

        // PUT: api/Payments/5/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IBusinessResult> CancelPaymentStatus(int id)
        {
            return await _paymentService.CancelPaymentStatus(id);
        }
    }
}
