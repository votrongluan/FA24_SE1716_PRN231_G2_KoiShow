using KoiShow.Data;
using KoiShow.Service.Base;
using KoiShow.Common;
using KoiShow.Data.Models;

namespace KoiShow.Service;

public interface IContestResultService
{
    Task<IBusinessResult> GetAllAsync();
    Task<IBusinessResult> GetByIdAsync(int contestResultId);
    Task<IBusinessResult> SaveAsync(ContestResult contestResult);
    Task<IBusinessResult> DeleteByIdAsync(int contestResultId);
    Task<IBusinessResult> GetPointsForContestResult(int contestId);
}

public class ContestResultService : IContestResultService
{
    private readonly UnitOfWork _unitOfWork;

    public ContestResultService()
    {
        _unitOfWork ??= new UnitOfWork();
    }

    public async Task<IBusinessResult> GetAllAsync()
    {
        #region Business rule

        #endregion

        try
        {
            var contestResults = await _unitOfWork.ContestResultRepository.GetAllWithIncludeAsync(e => e.Contest);

            if (contestResults == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<ContestResult>());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, contestResults);
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    public async Task<IBusinessResult> GetByIdAsync(int contestResultId)
    {
        #region Business rule

        #endregion

        try
        {
            var contestResult = await _unitOfWork.ContestResultRepository.GetByIdAsync(contestResultId);

            if (contestResult == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new ContestResult());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, contestResult);
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    public async Task<IBusinessResult> SaveAsync(ContestResult contestResult)
    {
        try
        {
            int result = -1;

            var contestResultTmp = await _unitOfWork.ContestResultRepository.GetByIdAsync(contestResult.Id);

            if (contestResultTmp != null)
            {
                #region Business Rule for update
                #endregion

                result = await _unitOfWork.ContestResultRepository.UpdateAsync(contestResult);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, contestResult);
                }

                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, contestResult);
            }

            #region Business Rule for create
            #endregion

            result = await _unitOfWork.ContestResultRepository.CreateAsync(contestResult);

            if (result > 0)
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, contestResult);
            }

            return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, contestResult);
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    public async Task<IBusinessResult> DeleteByIdAsync(int contestResultId)
    {
        #region Business Rule 
        #endregion

        try
        {
            var contestResult = await _unitOfWork.ContestResultRepository.GetByIdAsync(contestResultId);

            if (contestResult != null)
            {
                #region Business Rule for update
                #endregion

                var result = await _unitOfWork.ContestResultRepository.RemoveAsync(contestResult);

                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, contestResult);
                }

                return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, contestResult);
            }

            return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, contestResult);
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    public Task<IBusinessResult> GetPointsForContestResult(int contestId)
    {
        throw new NotImplementedException();
    }
}