using KoiShow.Common;
using KoiShow.Data;
using KoiShow.Data.DTO.PointDTO;
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

        Task<IBusinessResult> Save(PointUpdateRequestDTO point);

        Task<IBusinessResult> Delete(int id);

        Task<IBusinessResult> Create(PointCreateRequestDTO point);
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

            var result = new List<PointResponseDTO>();

            foreach (var point in points)
            {
                if (point.DeletedTime == null)
                {
                    result.Add(new PointResponseDTO
                    {
                        Id = point.Id,
                        ShapePoint = point.ShapePoint,
                        ColorPoint = point.ColorPoint,
                        PatternPoint = point.PatternPoint,
                        Comment = point.Comment,
                        PointStatus = point.PointStatus,
                        JudgeRank = point.JudgeRank,
                        JuryId = point.JuryId,
                        Penalties = point.Penalties,
                        RegisterFormId = point.RegisterFormId
                    });
                }
            }

            if (points == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Point>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
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
                var result = new PointResponseDTO
                {
                    Id = point.Id,
                    ShapePoint = point.ShapePoint,
                    ColorPoint = point.ColorPoint,
                    PatternPoint = point.PatternPoint,
                    Comment = point.Comment,
                    PointStatus = point.PointStatus,
                    JudgeRank = point.JudgeRank,
                    JuryId = point.JuryId,
                    Penalties = point.Penalties,
                    RegisterFormId = point.RegisterFormId
                };
                
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
        }

        public async Task<IBusinessResult> Save(PointUpdateRequestDTO point)
        {
            try
            {
                int result = -1;
                var pointTmp = await _unitOfWork.PointRepository.GetByIdAsync(point.Id);

                if (pointTmp != null)
                {
                    var newPoint = new Point
                    {
                        Id = point.Id,
                         ShapePoint = point.ShapePoint,
                        ColorPoint = point.ColorPoint,
                        PatternPoint = point.PatternPoint,
                        Comment = point.Comment,
                        JuryId = point.JuryId,
                        RegisterFormId = point.RegisterFormId,
                        PointStatus = point.PointStatus,
                        JudgeRank = point.JudgeRank,
                        Penalties = point.Penalties,
                    };

                    result = await _unitOfWork.PointRepository.UpdateAsync(newPoint);

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
                    point.DeletedTime = DateTime.UtcNow;
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

        public async Task<IBusinessResult> Create(PointCreateRequestDTO point)
        {
            try
            {
                var newPoint = new Point{
                    ShapePoint = point.ShapePoint,
                    ColorPoint = point.ColorPoint,
                    PatternPoint = point.PatternPoint,
                    Comment = point.Comment,
                    JuryId = point.JuryId,
                    RegisterFormId = point.RegisterFormId,
                    PointStatus = point.PointStatus,
                    JudgeRank = point.JudgeRank,
                    Penalties = point.Penalties
                };
                

                var result = await _unitOfWork.PointRepository.CreateAsync(newPoint);

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
