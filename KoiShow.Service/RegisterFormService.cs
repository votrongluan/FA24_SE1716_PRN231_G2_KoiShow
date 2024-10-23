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
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int registerFormId);
        Task<IBusinessResult> Save(RegisterForm registerForm);
        Task<IBusinessResult> DeleteById(int registerFormId);
    }

    public class RegisterFormService : IRegisterFormService
    {
        private readonly UnitOfWork _unitOfWork;

        public RegisterFormService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()

        {
            var registerForms = await _unitOfWork.RegisterFormRepository.GetAllAsync();

            if (registerForms == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, registerForms);
            }
        }
        public async Task<IBusinessResult> GetById(int RegisterFormId)
        {
            var registerForm = await _unitOfWork.RegisterFormRepository.GetByIdAsync(RegisterFormId);

            if (registerForm == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new RegisterForm());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, registerForm);
            }
        }

        public async Task<IBusinessResult> Save(RegisterForm registerForm)
        {
            try
            {
                int result = -1;

                var registerFormTmp = _unitOfWork.RegisterFormRepository.GetById(registerForm.Id);

                if (registerFormTmp != null)
                {
                    #region Business Rule
                    #endregion Business Rule

                    result = await _unitOfWork.RegisterFormRepository.UpdateAsync(registerForm);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.RegisterFormRepository.CreateAsync(registerForm);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, registerForm);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_CREATE_MSG);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int RegisterFormId)
        {
            try
            {
                var registerForm = await _unitOfWork.RegisterFormRepository.GetByIdAsync(RegisterFormId);

                if (registerForm == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new RegisterForm());
                }
                else
                {
                    var result = await _unitOfWork.RegisterFormRepository.RemoveAsync(registerForm);

                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, registerForm);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, registerForm);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
        public async Task<IBusinessResult> GetAllWithPayment()
        {
            #region Business Rule

            #endregion


            var payment = await _unitOfWork.RegisterFormRepository.GetAllAsync();

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


            var payment = await _unitOfWork.RegisterFormRepository.GetByIdAsync(registerFormId);

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
