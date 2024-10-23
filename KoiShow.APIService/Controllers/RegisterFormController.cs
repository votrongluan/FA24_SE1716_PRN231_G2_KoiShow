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

namespace KoiShow.API.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterFormController : ControllerBase
    {
        private readonly RegisterFormService _registerFormService;
        public RegisterFormController(RegisterFormService registerFormService) => _registerFormService = registerFormService;

        // GET: api/Payments
        [HttpGet]
        public async Task<IBusinessResult> GetPayments()
        {
            return await _registerFormService.GetAllWithPayment();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetPaymentById(int id)
        {
            return await _registerFormService.GetByIdWithPayment(id);
        }

    }
}
