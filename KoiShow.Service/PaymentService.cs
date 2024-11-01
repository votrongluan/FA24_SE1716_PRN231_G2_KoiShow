using KoiShow.Service.Base;
using KoiShow.Data;
using KoiShow.Data.Models;
using KoiShow.Common;
using KoiShow.Common.DTO.DtoResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoiShow.Common.DTO.DTORequest;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using KoiShow.Common.Librarie;

namespace KoiShow.Service
{

    public interface IPaymentService
    {
        Task<IBusinessResult> CreatePayment(PaymentDto paymentDto);
        Task<IBusinessResult> GetAllPaymentsAsync();
        Task<IBusinessResult> FindPaymentById(int paymentId);
        Task<IBusinessResult> FindPaymentsByStringAsync(string searchString);
        Task<IBusinessResult> UpdatePaymentStatusToPaid(int paymentId);
        Task<IBusinessResult> CancelPaymentStatus(int paymentId);
        Task<IBusinessResult> GeneratePaymentUrl(int paymentId, HttpContext context);
    }

    public class PaymentService : IPaymentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IConfiguration configuration)
        {
            _unitOfWork = new UnitOfWork();
            _configuration = configuration;
        }

        public async Task<IBusinessResult> CreatePayment(PaymentDto paymentDto)
        {
            try
            {
                var result = await _unitOfWork.CreatePaymentAsync(paymentDto);
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GeneratePaymentUrl(int paymentId, HttpContext context)
        {
            try
            {
                // Retrieve payment details
                var payment = await _unitOfWork.FindPaymentByIdAsync(paymentId);
                if (payment == null)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, "Payment not found");
                }

                // Multiply payment amount by 100 (VNPay requires amounts in minor units)
                var paymentAmount = ((int)payment.PaymentAmount * 100).ToString();

                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
                var tick = DateTime.Now.Ticks.ToString();

                var pay = new VnPayLibrary();
                var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

                // Add VNPay request data
                pay.AddRequestData("vnp_Version", _configuration["VNPay:Version"]);
                pay.AddRequestData("vnp_Command", _configuration["VNPay:Command"]);
                pay.AddRequestData("vnp_TmnCode", _configuration["VNPay:TmnCode"]);
                pay.AddRequestData("vnp_Amount", paymentAmount);  // Amount in minor units
                pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", _configuration["VNPay:CurrCode"]);
                pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
                pay.AddRequestData("vnp_Locale", _configuration["VNPay:Locale"]);
                pay.AddRequestData("vnp_OrderInfo", $"{payment.Id} Payment for service {payment.PaymentAmount}");
                pay.AddRequestData("vnp_OrderType", "billpayment");  // Make sure you set this field correctly
                pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
                pay.AddRequestData("vnp_TxnRef", tick);  // Unique transaction reference

                // Generate the payment URL
                var paymentUrl = pay.CreateRequestUrl(_configuration["VNPay:BaseUrl"], _configuration["VNPay:HashSecret"]);

                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, new
                {
                    Payment = payment,
                    PaymentUrl = paymentUrl
                });
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllPaymentsAsync()
        {
            try
            {
                var payments = await _unitOfWork.GetAllPaymentsAsync();
                if (payments != null && payments.Count > 0)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, payments);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        // Find Payment by ID
        public async Task<IBusinessResult> FindPaymentById(int paymentId)
        {
            try
            {
                var payment = await _unitOfWork.FindPaymentByIdAsync(paymentId);
                if (payment != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, payment);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> FindPaymentsByCriteriaAsync(string transactionId, string description, string paymentStatus)
        {
            try
            {
                var payments = await _unitOfWork.FindPaymentsByCriteriaAsync(transactionId, description, paymentStatus);
                if (payments.Count > 0)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, payments);
                }
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        //Find Payments by Any Field
        public async Task<IBusinessResult> FindPaymentsByStringAsync(string searchString)
        {
            try
            {
                var payments = await _unitOfWork.FindPaymentsByStringAsync(searchString);
                if (payments.Count > 0)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, payments);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        // Update Payment Status to Paid
        public async Task<IBusinessResult> UpdatePaymentStatusToPaid(int paymentId)
        {
            try
            {
                var result = await _unitOfWork.UpdatePaymentStatusToPaidAsync(paymentId);
                if (result != null)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, result);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        // Cancel Payment Status
        public async Task<IBusinessResult> CancelPaymentStatus(int paymentId)
        {
            try
            {
                var result = await _unitOfWork.CancelPaymentStatusAsync(paymentId);
                if (result != null)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, result);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}

