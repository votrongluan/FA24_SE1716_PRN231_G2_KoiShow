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

namespace KoiShow.API.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        //private readonly FA24_SE1716_PRN231_G2_KOISHOWContext _context;
        //public PaymentsController(FA24_SE1716_PRN231_G2_KOISHOWContext context)
                //{
                //    _context = context;
                //}

        private readonly PaymentService _paymentService;
        public PaymentsController(PaymentService paymentService) => _paymentService = paymentService;

        // GET: api/Payments
        [HttpGet]
        public async Task<IBusinessResult> GetPayments()
        {
            return await _paymentService.GetAll();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetPaymentById(int id)
        {
            return await _paymentService.GetById(id);
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutPayment(int id, Payment payment)
        {
            return await _paymentService.Save(payment);
        }

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostPayment(Payment payment)
        {
            return await _paymentService.Save(payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeletePayment(int id)
        {
            return await _paymentService.DeleteById(id);
        }

        private async Task<bool> PaymentExists(int id)
        {
            var result = await _paymentService.GetById(id);
            return result != null && result.Status == 200 && result.Data != null;
        }
    }
}
