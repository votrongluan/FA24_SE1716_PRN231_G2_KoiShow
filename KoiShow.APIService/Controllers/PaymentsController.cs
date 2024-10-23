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

        //GET: api/Payments/search? searchString = value
       [HttpGet("search")]
        public async Task<IBusinessResult> FindPaymentsByStringAsync(string searchString)
        {
            return await _paymentService.FindPaymentsByStringAsync(searchString);
        }

        // PUT: api/Payments/5/paid
        [HttpPut("{id}/paid")]
        public async Task<IBusinessResult> UpdatePaymentStatusToPaid(int id)
        {
            return await _paymentService.UpdatePaymentStatusToPaid(id);
        }

        // PUT: api/Payments/5/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IBusinessResult> CancelPaymentStatus(int id)
        {
            return await _paymentService.CancelPaymentStatus(id);
        }
    }
}
