using KoiShow.Data.Base;
using KoiShow.Data.Models;
using KoiShow.Common.DTO.DtoResponse;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiShow.Data.Repository
{
    public class PaymentRepository : GenericRepository<Payment>
    {
        public PaymentRepository() { }

        public PaymentRepository(FA24_SE1716_PRN231_G2_KoiShowContext context) => _context = context;

        // GetAll that returns PaymentDtoResponse
        public async Task<List<PaymentDtoResponse>> GetAllPaymentsAsync()
        {
            var payments = await _context.Payments.ToListAsync();

            // Map Payment to PaymentDtoResponse
            return payments.Select(payment => new PaymentDtoResponse
            {
                Id = payment.Id,
                RegisterFormId = payment.RegisterFormId,
                TransactionId = payment.TransactionId,
                PaymentAmount = payment.PaymentAmount,
                PaymentDate = payment.PaymentDate,
                PaymentStatus = payment.PaymentStatus,
                Description = payment.Description,
                Vatrate = payment.Vatrate,
                ActualCost = payment.ActualCost,
                DiscountAmount = payment.DiscountAmount,
                FinalAmount = payment.FinalAmount,
                Currency = payment.Currency,
                CreatedBy = payment.CreatedBy,
                CreatedTime = payment.CreatedTime,
                LastUpdatedBy = payment.LastUpdatedBy,
                LastUpdatedTime = payment.LastUpdatedTime
            }).ToList();
        }

        // GetById that returns PaymentDtoResponse
        public async Task<PaymentDtoResponse> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return null;

            // Map Payment to PaymentDtoResponse
            return new PaymentDtoResponse
            {
                Id = payment.Id,
                RegisterFormId = payment.RegisterFormId,
                TransactionId = payment.TransactionId,
                PaymentAmount = payment.PaymentAmount,
                PaymentDate = payment.PaymentDate,
                PaymentStatus = payment.PaymentStatus,
                Description = payment.Description,
                Vatrate = payment.Vatrate,
                ActualCost = payment.ActualCost,
                DiscountAmount = payment.DiscountAmount,
                FinalAmount = payment.FinalAmount,
                Currency = payment.Currency,
                CreatedBy = payment.CreatedBy,
                CreatedTime = payment.CreatedTime,
                LastUpdatedBy = payment.LastUpdatedBy,
                LastUpdatedTime = payment.LastUpdatedTime
            };
        }
    }
}
