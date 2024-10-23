using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KoiShow.Data.Models;
using KoiShow.Service;
using KoiShow.Service.Base;

namespace KoiShow.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestResultsController : ControllerBase
    {
        private readonly ContestResultService _contestResultService;

        public ContestResultsController(ContestResultService contestResultService)
        {
            _contestResultService = contestResultService;
        }
            
        [HttpGet]
        public async Task<IBusinessResult> GetContestResults()
        {
            return await _contestResultService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetContestResult(int id)
        {
            var contestResult = await _contestResultService.GetByIdAsync(id);

            return contestResult;
        }

        [HttpGet("points/{id}")]
        public async Task<IBusinessResult> GetPointsForContestResult(int id)
        {
            var contestResult = await _contestResultService.GetPointsForContestResult(id);

            return contestResult;
        }

        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutContestResult(int id, ContestResult contestResult)
        {
            return await _contestResultService.SaveAsync(contestResult);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteContestResult(int id)
        {
            return await _contestResultService.DeleteByIdAsync(id);
        }

        [HttpPost]
        public async Task<IBusinessResult> PostContestResult(ContestResult contestResult)
        {
            return await _contestResultService.SaveAsync(contestResult);
        }

        private async Task<bool> ContestResultExists(int id)
        {
            return await _contestResultService.GetByIdAsync(id) != null;
        }
    }
}
