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
    public interface IPointService
    {
        Task<IBusinessResult> GetAll();

        Task<IBusinessResult> GetById(int id);

        Task<IBusinessResult> Save(Point point);

        Task<IBusinessResult> Delete(int id);

        Task<IBusinessResult> Create(Point point);
    }
    public class PointService : IPointService
    {
        private readonly UnitOfWork _unitOfWork;

        public PointService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var points = await _unitOfWork.PointRepository.GetAllAsync();

            if (points == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Point>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, points);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            var point = await _unitOfWork.PointRepository.GetByIdAsync(id);

            if (point == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Point>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, point);
            }
        }

        public async Task<IBusinessResult> Save(Point point)
        {
            try
            {
                int result = -1;
                var pointTmp = await _unitOfWork.PointRepository.GetByIdAsync(point.Id);

                if (pointTmp != null)
                {
                    result = await _unitOfWork.PointRepository.UpdateAsync(point);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, point);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, new List<Point>());
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Point>());
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Delete(int id)
        {
            try
            {

                var point = await _unitOfWork.PointRepository.GetByIdAsync(id);

                if (point != null)
                {
                    var result = await _unitOfWork.PointRepository.RemoveAsync(point);

                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, new List<Point>());
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, new List<Point>());
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Point>());
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Create(Point point)
        {
            try
            {
                var result = await _unitOfWork.PointRepository.CreateAsync(point);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, point);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, new List<Point>());
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
