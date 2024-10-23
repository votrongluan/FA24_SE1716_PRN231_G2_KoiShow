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


        // GET: api/RegisterForms
        [HttpGet]
        public async Task<IBusinessResult> GetRegisterForms()
        {
            return await _registerFormService.GetAll();
        }

        // GET: api/RegisterForms/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetRegisterForm(int id)
        {
            var registerForm = await _registerFormService.GetById(id);

            return registerForm;
        }

        // PUT: api/RegisterForms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> PutRegisterForm(RegisterForm registerForm)
        {
            return await _registerFormService.Save(registerForm);
        }

        // POST: api/RegisterForms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostRegisterForm(RegisterForm registerForm)
        {
            return await _registerFormService.Save(registerForm);
        }

        // DELETE: api/RegisterForms/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteRegisterForm(int id)
        {
            return await _registerFormService.DeleteById(id);
        }

        private bool RegisterFormExists(int id)
        {
            return _registerFormService.GetById(id) != null;
        }
    }
}
