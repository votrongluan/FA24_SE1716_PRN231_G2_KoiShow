using KoiShow.Common;
using KoiShow.Data.Models;
using KoiShow.Data;
using KoiShow.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShow.Service
{
    public interface IAnimalService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(Animal animal);
        Task<IBusinessResult> DeleteById(int id);

    }

    public class AnimalService : IAnimalService
    {
        private readonly UnitOfWork _unitOfWork;

        public AnimalService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            #region Business Rule

            #endregion


            var animals = await _unitOfWork.AnimalRepository.GetAllWithIncludeAsync(a => a.Owner, a => a.Variety);

            if (animals == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Animal>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, animals);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            #region Business Rule

            #endregion


            var animals = (await _unitOfWork.AnimalRepository.GetAllWithIncludeAsync(a => a.Owner, a => a.Variety)).Where(e => e.Id == id).FirstOrDefault();

            if (animals == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Animal>());
            }
            else
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, animals);
            }
        }

        public async Task<IBusinessResult> Save(Animal animal)
        {
            try
            {
                int result = -1;
                var findAnimal = await _unitOfWork.AnimalRepository.GetByIdAsync(animal.Id);

                if (findAnimal != null)
                {
                    result = await _unitOfWork.AnimalRepository.UpdateAsync(animal);

                    if (result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, findAnimal);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, new List<Animal>());
                    }
                }

                result = await _unitOfWork.AnimalRepository.CreateAsync(animal);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, animal);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, new List<Animal>());
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
                var animals = await _unitOfWork.AnimalRepository.GetByIdAsync(id);

                if (animals == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<Animal>());
                }
                else
                {
                    var result = await _unitOfWork.AnimalRepository.RemoveAsync(animals);

                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, animals);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, animals);
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
