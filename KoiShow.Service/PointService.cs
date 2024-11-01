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
        Task<IBusinessResult> Save(Point point);
        Task<IBusinessResult> DeleteById(int id);

    }

    public class PointService : IPointService
    {
        private readonly UnitOfWork _unitOfWork;

        public PointService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            var points = await _unitOfWork.PointRepository
                .GetAllWithIncludeAsync(
                    a => a.Jury,
                    a => a.RegisterForm,
                    a => a.RegisterForm.Animal,
                    a => a.RegisterForm.Contest
                );

            if (points == null || !points.Any())
            {
                return new BusinessResult(
                    Const.WARNING_NO_DATA_CODE,
                    Const.WARNING_NO_DATA_MSG,
                    new List<PointResponseDTO>()
                );
            }

            var pointDtos = points.Select(p => new PointResponseDTO
            {
                Id = p.Id,
                AnimalName = p.RegisterForm?.Animal?.AnimalName,
                ContestName = p.RegisterForm?.Contest?.ContestName,
                ShapePoint = p.ShapePoint,
                ColorPoint = p.ColorPoint,
                PatternPoint = p.PatternPoint,
                Comment = p.Comment,
                JuryId = p.Jury?.Id,
                RegisterFormId = p.RegisterForm?.Id,
                PointStatus = p.PointStatus,
                JudgeRank = p.JudgeRank,
                Penalties = p.Penalties,
                TotalScore = p.TotalScore,
                JuryName = p.Jury?.FullName ?? string.Empty
            }).ToList();

            return new BusinessResult(
                Const.SUCCESS_CREATE_CODE,
                Const.SUCCESS_CREATE_MSG,
                pointDtos
            );
        }


        public async Task<IBusinessResult> GetById(int id)
        {
            #region Business Rule

            #endregion


            var animals = (await _unitOfWork.PointRepository.GetAllWithIncludeAsync(a => a.RegisterForm, a => a.Jury, a => a.RegisterForm.Animal)).Where(e => e.Id == id).FirstOrDefault();

            if (animals == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Animal>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, animals);
            }
        }

        public async Task<IBusinessResult> Save(Point point)
        {
            try
            {
                int result = -1;
                var findPoint = await _unitOfWork.PointRepository.GetByIdAsync(point.Id);

                if (findPoint != null)
                {
                    result = await _unitOfWork.PointRepository.UpdateAsync(point);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, findPoint);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, new List<Point>());
                    }
                }

                result = await _unitOfWork.PointRepository.CreateAsync(point);

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
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            #region Business Rule

            #endregion
            try
            {
                var points = await _unitOfWork.PointRepository.GetByIdAsync(id);

                if (points == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Point>());
                }
                else
                {
                    var result = await _unitOfWork.PointRepository.RemoveAsync(points);

                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, points);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, points);
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
