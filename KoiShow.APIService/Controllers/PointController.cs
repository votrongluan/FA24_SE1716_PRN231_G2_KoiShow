using KoiShow.Service.Base;
using KoiShow.Service;
using Microsoft.AspNetCore.Mvc;
using KoiShow.Data.Models;
using KoiShow.Data.DTO.PointDTO;


namespace KoiShow.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly PointService _pointService;

        public PointsController(PointService pointService)
        {
            _pointService = pointService;
        }

        [HttpGet]
        public async Task<IBusinessResult> GetPoints()
        {
            return await _pointService.GetAll();
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetPoint(int id)
        {
            var animal = await _pointService.GetById(id);

            return animal;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutPoint(int id, Point point)
        {
            return await _pointService.Save(point);
        }

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostAnimal(Point point)
        {
            return await _pointService.Save(point);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAnimal(int id)
        {
            return await _pointService.DeleteById(id);
        }

        private bool AnimalExists(int id)
        {
            return _pointService.GetById(id) != null;
        }

    }
}
