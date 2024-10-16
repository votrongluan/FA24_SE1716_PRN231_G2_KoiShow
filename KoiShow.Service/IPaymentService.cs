//using KoiShow.Service.Base;
//using KoiShow.Data;
//using KoiShow.Data.Models;
//using KoiShow.Common;
//using KoiShow.Common.DTO.DtoResponse;


//namespace KoiShow.Service
//{
//    public interface IPaymentService
//    {
//        //Task<IBusinessResult> GetAll();
//        //Task<IBusinessResult> GetById(int paymentNp);
//        Task<IBusinessResult> GetAll();
//        Task<IBusinessResult> GetById(int paymentId);
//        Task<IBusinessResult> Save(Payment payment);
//        Task<IBusinessResult> DeleteById(int paymentNo);


//    }

//    public class PaymentService : IPaymentService
//    {
//        private readonly UnitOfWork _unitOfWork;

//        public PaymentService()
//        {
//            _unitOfWork ??= new UnitOfWork();
//        }


//        //public async Task<IBusinessResult> GetAll()
//        //{
//        //    #region Business Rule

//        //    #endregion


//        //    var payment = await _unitOfWork.PPaymentRepository.GetAllAsync();

//        //    if (payment == null)
//        //    {
//        //        return new BusinessResult(constant.WARNING_NO_DATA_CODE, constant.WARNING_NO_DATA_MSG, new List<Payment>());
//        //    }
//        //    else
//        //    {
//        //        return new BusinessResult(constant.WARNING_NO_DATA_CODE, constant.WARNING_NO_DATA_MSG, payment);
//        //    }
//        //}

//        //public async Task<IBusinessResult> GetById(int paymentNo)
//        //{
//        //    #region Business Rule

//        //    #endregion


//        //    var payment = await _unitOfWork.PPaymentRepository.GetByIdAsync(paymentNo);

//        //    if (payment == null)
//        //    {
//        //        return new BusinessResult(constant.WARNING_NO_DATA_CODE, constant.WARNING_NO_DATA_MSG, new List<Payment>());
//        //    }
//        //    else
//        //    {
//        //        return new BusinessResult(constant.WARNING_NO_DATA_CODE, constant.WARNING_NO_DATA_MSG, payment);
//        //    }
//        //}

//        public async Task<IBusinessResult> GetAll()
//        {
//            var payments = await _unitOfWork.PPaymentRepository.GetAllPaymentsAsync();

//            if (payments == null || payments.Count == 0)
//            {
//                return new BusinessResult(constant.WARNING_NO_DATA_CODE, constant.WARNING_NO_DATA_MSG, new List<PaymentDtoResponse>());
//            }
//            else
//            {
//                return new BusinessResult(constant.SUCCESS_READ_CODE, constant.SUCCESS_READ_MSG, payments);
//            }
//        }

//        public async Task<IBusinessResult> GetById(int paymentId)
//        {
//            var payment = await _unitOfWork.PPaymentRepository.GetPaymentByIdAsync(paymentId);

//            if (payment == null)
//            {
//                return new BusinessResult(constant.WARNING_NO_DATA_CODE, constant.WARNING_NO_DATA_MSG, new PaymentDtoResponse());
//            }
//            else
//            {
//                return new BusinessResult(constant.SUCCESS_READ_CODE, constant.SUCCESS_READ_MSG, payment);
//            }
//        }

//        public async Task<IBusinessResult> Save(Payment payment)
//        {
//            #region Business Rule
//            #endregion
//            try
//            {
//                int result = -1;
//                var findPayment = await _unitOfWork.PPaymentRepository.GetByIdAsync(payment.PaymentId);

//                if (findPayment == null)
//                {
//                     result = await _unitOfWork.PPaymentRepository.UpdateAsync(payment);
//                     if (result > 0)
//                        {
//                          return new BusinessResult(constant.SUCCESS_UPDATE_CODE, constant.SUCCESS_UPDATE_MSG, new List<Payment>());
//                        }
//                     else
//                        {
//                          return new BusinessResult(constant.FAIL_UPDATE_CODE, constant.FAIL_UPDATE_MSG, result);
//                        }
//                }
//                else
//                {
//                    result = await _unitOfWork.PPaymentRepository.CreateAsync(payment);
//                    if (result > 0)
//                    {
//                        return new BusinessResult(constant.SUCCESS_CREATE_CODE, constant.SUCCESS_CREATE_MSG, new List<Payment>());
//                    }
//                    else
//                    {
//                        return new BusinessResult(constant.FAIL_UPDATE_CODE, constant.FAIL_UPDATE_MSG, result);
//                    }
//                }
//            }catch (Exception ex)
//            {
//                return new BusinessResult(constant.ERROR_EXCEPTION, ex.Message);
//            }

//        }

//        public async Task<IBusinessResult> DeleteById(int paymentNo)
//        {
//            #region Business Rule

//            #endregion

//            try

//            {
//                var payment = await _unitOfWork.PPaymentRepository.GetByIdAsync(paymentNo);
//                if (payment == null)
//                {
//                        return new BusinessResult(constant.WARNING_NO_DATA_CODE, constant.WARNING_NO_DATA_MSG, new Payment());
//                }
//                else 
//                {
//                    var result = await _unitOfWork.PPaymentRepository.RemoveAsync(payment);

//                    if(result == true)
//                    {
//                        return new BusinessResult(constant.SUCCESS_DELETE_CODE, constant.SUCCESS_DELETE_MSG, new Payment());
//                    }
//                    else
//                    {
//                        return new BusinessResult(constant.FAIL_DELETE_CODE, constant.FAIL_DELETE_MSG, new Payment());
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(constant.ERROR_EXCEPTION, ex.Message);
//            }

//        }

//    }
//}

using KoiShow.Service.Base;
using KoiShow.Data;
using KoiShow.Data.Models;
using KoiShow.Common;
using KoiShow.Common.DTO.DtoResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiShow.Service
{
    public interface IPaymentService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int paymentId);
        Task<IBusinessResult> Save(Payment payment);
        Task<IBusinessResult> DeleteById(int paymentId);
    }

    public class PaymentService : IPaymentService
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var payments = await _unitOfWork.PPaymentRepository.GetAllPaymentsAsync();

            if (payments == null || payments.Count == 0)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<PaymentDtoResponse>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, payments);
            }
        }

        public async Task<IBusinessResult> GetById(int paymentId)
        {
            var payment = await _unitOfWork.PPaymentRepository.GetPaymentByIdAsync(paymentId);

            if (payment == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new PaymentDtoResponse());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, payment);
            }
        }

        public async Task<IBusinessResult> Save(Payment payment)
        {
            try
            {
                var existingPayment = await _unitOfWork.PPaymentRepository.GetByIdAsync(payment.Id);

                if (existingPayment == null) // Create new payment if it doesn't exist
                {
                    var result = await _unitOfWork.PPaymentRepository.CreateAsync(payment);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, new PaymentDtoResponse());
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, result);
                    }
                }
                else // Update existing payment
                {
                    var result = await _unitOfWork.PPaymentRepository.UpdateAsync(payment);
                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, new PaymentDtoResponse());
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, result);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> DeleteById(int paymentId)
        {
            try
            {
                var payment = await _unitOfWork.PPaymentRepository.GetByIdAsync(paymentId);
                if (payment == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new PaymentDtoResponse());
                }
                else
                {
                    var result = await _unitOfWork.PPaymentRepository.RemoveAsync(payment);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, new PaymentDtoResponse());
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, new PaymentDtoResponse());
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
