using KoiShow.Common;
using KoiShow.Data;
using KoiShow.Data.Models;
using KoiShow.Service.Base;

namespace KoiShow.Service;

public interface IContestService
{
    Task<IBusinessResult> GetAllAsync();
    Task<IBusinessResult> GetByIdAsync(int contestId);
    Task<IBusinessResult> SaveAsync(Contest contest);
    Task<IBusinessResult> DeleteByIdAsync(int contestId);
}

public class ContestService: IContestService
{
    private readonly UnitOfWork _unitOfWork;

    public ContestService()
    {
        _unitOfWork ??= new UnitOfWork();
    }

    public async Task<IBusinessResult> GetAllAsync()
    {
        #region Business rule

        #endregion

        try
        {
            var contests = await _unitOfWork.ContestRepository.GetAllAsync();

            if (contests == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Contest>());
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, contests);
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    public Task<IBusinessResult> GetByIdAsync(int contestId)
    {
        throw new NotImplementedException();
    }

    public Task<IBusinessResult> SaveAsync(Contest contest)
    {
        throw new NotImplementedException();
    }

    public Task<IBusinessResult> DeleteByIdAsync(int contestId)
    {
        throw new NotImplementedException();
    }
}