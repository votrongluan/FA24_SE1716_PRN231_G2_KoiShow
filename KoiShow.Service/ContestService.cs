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
    public interface IContestService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int ContestId);
        Task<IBusinessResult> Save(Contest contest);
        Task<IBusinessResult> DeleteById(int ContestId);
    }
    public class ContestService : IContestService
    {
        private readonly UnitOfWork _unitOfWork;
        public ContestService() 
        {
            _unitOfWork ??= new UnitOfWork();
        }
        
        public async Task<IBusinessResult> GetAll()
        {
            var contests = await _unitOfWork.ContestRepository.GetAllAsync();

            if (contests == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Contest>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, contests);
            }
        }

        public async Task<IBusinessResult> GetById(int ContestId)
        {
            var contest = await _unitOfWork.ContestRepository.GetByIdAsync(ContestId);

            if(contest == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Contest());
            }
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, contest);
            }
        }

        public async Task<IBusinessResult> Save(Contest contest)
        {
            try
            {
                int result = -1;

                var contestTmp = _unitOfWork.ContestRepository.GetById(contest.Id);

                if (contestTmp != null)
                {
                    result = await _unitOfWork.ContestRepository.UpdateAsync(contest);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, contest);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.ContestRepository.CreateAsync(contest);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, contest);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, contest);
                    }
                }
            }
            catch (Exception ex) 
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int ContestId)
        {
            try
            {
                var contest = await _unitOfWork.ContestRepository.GetByIdAsync(ContestId);

                if (contest == null) 
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Contest());
                }
                else
                {
                    var result = await _unitOfWork.ContestRepository.RemoveAsync(contest);

                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, contest);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, contest);
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
