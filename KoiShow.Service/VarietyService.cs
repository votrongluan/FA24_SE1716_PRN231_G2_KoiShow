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
    public interface IVarietyService
    {
        Task<IBusinessResult> GetAll();
    }

    public class VarietyService
    {
        private readonly UnitOfWork _unitOfWork;

        public VarietyService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            #region Business Rule

            #endregion


            var varieties = await _unitOfWork.VarietyRepository.GetAllAsync();

            if (varieties == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Animal>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, varieties);
            }
        }
    }
}
