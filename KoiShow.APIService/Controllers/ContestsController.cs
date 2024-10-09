using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiShow.Data.Models;
using KoiShow.Service;
using KoiShow.Service.Base;
using KoiShow.Service;

namespace KoiShow.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestsController : ControllerBase
    {
        //private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;
        private readonly ContestService _contestService;

        public ContestsController(ContestService contestService) => _contestService = contestService;

        // GET: api/Contests
        [HttpGet]
        public async Task<IBusinessResult> GetContests()
        {
            return await _contestService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetContest(int id)
        {
            var contest = await _contestService.GetById(id);

            return contest;
        }

        // PUT: api/Contests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutContest(int id, Contest contest)
        {
            return await _contestService.Save(contest);
        }

        // POST: api/Contests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostContest(Contest contest)
        {

            return await _contestService.Save(contest);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteContest(int id)
        {
            return await _contestService.DeleteById(id);
        }
        
        private bool ContestExists(int id)
        {
            return _contestService.GetById(id) != null;
        }
    }
}
