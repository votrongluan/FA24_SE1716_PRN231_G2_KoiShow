using KoiShow.Common;
using KoiShow.Data;
using KoiShow.Data.Models;
using KoiShow.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShow.Service
{
    public interface IAccountService
    {
        Task<IBusinessResult> GetByUserNameAndPassword(string userName, string password);
    }

    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public AccountService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetByUserNameAndPassword(string userName, string password)
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
            var account = accounts.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            if (account == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Account());
            } else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, account);
            }
        }
    }
}
