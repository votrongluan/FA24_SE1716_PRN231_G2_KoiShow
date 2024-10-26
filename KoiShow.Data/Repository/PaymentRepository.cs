    using KoiShow.Data.Base;
    using KoiShow.Data.Models;
    using KoiShow.Common.DTO.DtoResponse;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using KoiShow.Common.DTO.DTORequest;

    namespace KoiShow.Data.Repository
    {
        public class PaymentRepository : GenericRepository<Payment>
        {
            public PaymentRepository() { }

            public PaymentRepository(FA24_SE1716_PRN231_G2_KoiShowContext context) => _context = context;

            public async Task<List<PaymentDtoResponse>> GetAllPaymentsAsync()
            {
                var payments = await _context.Payments.ToListAsync();

                return payments.Select(MapToDtoResponse).ToList();
            }

            public IQueryable<PaymentDtoResponse> GetAllPayments()
            {
                return _context.Payments
                    .AsEnumerable()  // This moves the operation to in-memory to allow mapping
                    .Select(payment => MapToDtoResponse(payment))
                    .AsQueryable();
            }

            public async Task<PaymentDtoResponse> CreatePaymentAsync(PaymentDto paymentRequestDto)
            {
                // Map PaymentRequestDTO to Payment entity
                var payment = new Payment
                {
                    RegisterFormId = paymentRequestDto.RegisterFormId,
                    TransactionId = paymentRequestDto.TransactionId,
                    PaymentAmount = paymentRequestDto.PaymentAmount,
                    PaymentDate = paymentRequestDto.PaymentDate,
                    PaymentStatus = "Pending", 
                    Description = paymentRequestDto.Description,
                    Currency = paymentRequestDto.Currency
                };

                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();

                return MapToDtoResponse(payment);
            }

            // Find Payment By ID
            public async Task<PaymentDtoResponse> FindPaymentByIdAsync(int paymentId)
            {
                var payment = await _context.Payments.FindAsync(paymentId);
                return payment != null ? MapToDtoResponse(payment) : null;
            }

            // Find Payment By Any Field (Search by string)
            public async Task<List<PaymentDtoResponse>> FindPaymentsByStringAsync(string searchString)
            {
                // Trim and convert the search string to lowercase
                string searchLower = searchString.Trim().ToLower();

                // Perform the search
                var payments = await _context.Payments
                    .Where(p =>
                        (p.TransactionId != null && p.TransactionId.ToLower().Contains(searchLower)) ||
                        (p.Description != null && p.Description.ToLower().Contains(searchLower)) ||
                        (p.PaymentStatus != null && p.PaymentStatus.ToLower().Contains(searchLower)) ||
                        (p.PaymentAmount.ToString().Contains(searchLower)))  // Convert PaymentAmount to string for search
                    .ToListAsync();

                return payments.Select(MapToDtoResponse).ToList();
            }

        public async Task<List<PaymentDtoResponse>> FindPaymentsByCriteriaAsync(string transactionId, string description, string paymentStatus)
        {
            var query = _context.Payments.AsQueryable();

            if (!string.IsNullOrEmpty(transactionId))
            {
                query = query.Where(p => p.TransactionId != null && p.TransactionId.Contains(transactionId));
            }

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(p => p.Description != null && p.Description.Contains(description));
            }

            if (!string.IsNullOrEmpty(paymentStatus))
            {
                query = query.Where(p => p.PaymentStatus != null && p.PaymentStatus.Contains(paymentStatus));
            }

            var payments = await query.ToListAsync();
            return payments.Select(MapToDtoResponse).ToList();
        }


        public IQueryable<PaymentDtoResponse> FindPaymentsByString(string searchString)
            {
                return _context.Payments
                    .Where(p =>
                        (p.TransactionId != null && p.TransactionId.Contains(searchString)) ||
                        (p.Description != null && p.Description.Contains(searchString)) ||
                        (p.PaymentStatus != null && p.PaymentStatus.Contains(searchString)))
                    .AsEnumerable()  // This moves the operation to in-memory
                    .Select(payment => MapToDtoResponse(payment))
                    .AsQueryable();
            }

            // Update Payment Status to Paid
            public async Task<PaymentDtoResponse> UpdatePaymentStatusToPaidAsync(int paymentId)
            {
                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
                if (payment != null)
                {
                    payment.PaymentStatus = "Paid";
                    _context.Payments.Update(payment);
                    await _context.SaveChangesAsync();
                    return MapToDtoResponse(payment);
                }

                return null;
            }

            // Cancel Payment Status
            public async Task<PaymentDtoResponse> CancelPaymentStatusAsync(int paymentId)
            {
                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
                if (payment != null)
                {
                    payment.PaymentStatus = "Cancelled";
                    _context.Payments.Update(payment);
                    await _context.SaveChangesAsync();
                    return MapToDtoResponse(payment);
                }

                return null;
            }

            // Helper to map Payment to PaymentDtoResponse
            private PaymentDtoResponse MapToDtoResponse(Payment payment)
            {
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
