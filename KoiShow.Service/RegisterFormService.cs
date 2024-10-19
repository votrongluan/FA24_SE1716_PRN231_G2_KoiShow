using KoiShow.Service.Base;
using KoiShow.Data;
using KoiShow.Data.Models;
using KoiShow.Common;
using KoiShow.Common.DTO.DtoResponse;

namespace KoiShow.Service
{
    public interface IRegisterFormService
    {
        Task<IBusinessResult> GetAllWithPayment();
        Task<IBusinessResult> GetByIdWithPayment(int registerFormId);
    }

    public class RegisterFormService : IRegisterFormService
    {
        private readonly UnitOfWork _unitOfWork;

        public RegisterFormService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAllWithPayment()
        {
            #region Business Rule

            #endregion


            var payment = await _unitOfWork.RRegisterFormRepository.GetAllAsync();

            if (payment == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<RegisterForm>());
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, payment);
            }
        }

        public async Task<IBusinessResult> GetByIdWithPayment(int registerFormId)
        {
            #region Business Rule

            #endregion


            var payment = await _unitOfWork.RRegisterFormRepository.GetByIdAsync(registerFormId);

            if (payment == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new PaymentDtoResponse());
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, payment);
            }
        }

    }
}
