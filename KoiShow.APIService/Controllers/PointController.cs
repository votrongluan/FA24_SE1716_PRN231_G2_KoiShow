using KoiShow.Service.Base;
using KoiShow.Service;
using Microsoft.AspNetCore.Mvc;

namespace KoiShow.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        //private readonly FA24_SE1716_PRN231_G2_KOISHOWContext _context;
        private readonly PointService _pointService;

        public PointsController(PointService pointService) => _pointService = pointService;
        //public PointsController(FA24_SE1716_PRN231_G2_KOISHOWContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Points
        [HttpGet]
        public async Task<IBusinessResult> GetPoints()
        {
            return await _pointService.GetAll();
        }

        // GET: api/Points/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetPoint(int id)
        {
            var point = await _pointService.GetById(id);

            return point;
        }

        // PUT: api/Points/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutPoint(int id, Point point)
        {
            return await _pointService.Save(point);
        }

        // POST: api/Points
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostPoint(Point point)
        {
            return await _pointService.Create(point);
        }

        // DELETE: api/Points/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeletePoint(int id)
        {
            return await _pointService.Delete(id);
        }

    }
}
