using KoiShow.Service;
using KoiShow.Service.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiShow.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private readonly ContestService _contestService;

        public ContestController(ContestService contestService) =>
            _contestService = contestService;

        [HttpGet]
        public async Task<IBusinessResult> GetContestResults()
        {
            return await _contestService.GetAllAsync();
        }
    }
}
