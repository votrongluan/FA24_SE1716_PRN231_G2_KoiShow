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
        Task<IBusinessResult> CreatePaymentAndGenerateUrl(PaymentDto paymentDto, HttpContext context);
        Task<IBusinessResult> GetAllPaymentsAsync();
        Task<IBusinessResult> FindPaymentById(int paymentId);
        Task<IBusinessResult> FindPaymentsByStringAsync(string searchString);
        Task<IBusinessResult> UpdatePaymentStatusToPaid(int paymentId);
        Task<IBusinessResult> CancelPaymentStatus(int paymentId);
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

        public async Task<IBusinessResult> CreatePaymentAndGenerateUrl(PaymentDto paymentDto, HttpContext context)
        {
            try
            {
                // Step 1: Create the payment in the database first
                var paymentCreationResult = await _unitOfWork.CreatePaymentAsync(paymentDto);
                if (paymentCreationResult == null)
                {
                    return new BusinessResult(Const.ERROR_EXCEPTION, "Failed to create payment");
                }

                // Step 2: Generate the payment URL using the VNPay library
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
                var tick = DateTime.Now.Ticks.ToString();
                var pay = new VnPayLibrary();
                var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

                pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
                pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
                pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
                pay.AddRequestData("vnp_Amount", ((int)paymentDto.PaymentAmount * 100).ToString());
                pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
                pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
                pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
                pay.AddRequestData("vnp_OrderInfo", $"{paymentDto.PaymentId} {paymentDto.Description} {paymentDto.PaymentAmount}");
                pay.AddRequestData("vnp_OrderType", paymentDto.OrderType);
                pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
                pay.AddRequestData("vnp_TxnRef", tick);

                // Generate the payment URL
                var paymentUrl = pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

                // Step 3: Return both the payment and the URL to the caller
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, new
                {
                    Payment = paymentCreationResult,
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

