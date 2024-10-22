using KoiShow.Service.Base;
using KoiShow.Data;
using KoiShow.Data.Models;
using KoiShow.Common;
using KoiShow.Common.DTO.DtoResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoiShow.Common.DTO.DTORequest;

namespace KoiShow.Service
{

    public interface IPaymentService
    {
        Task<IBusinessResult> GetAllPaymentsAsync();
        Task<IBusinessResult> CreatePayment(PaymentDto paymentDto);
        Task<IBusinessResult> FindPaymentById(int paymentId);
        Task<IBusinessResult> FindPaymentsByStringAsync(string searchString);
        Task<IBusinessResult> UpdatePaymentStatusToPaid(int paymentId);
        Task<IBusinessResult> CancelPaymentStatus(int paymentId);
    }

    public class PaymentService : IPaymentService
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentService()
        {
            _unitOfWork = new UnitOfWork();
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

        // Create Payment using PaymentDto
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

