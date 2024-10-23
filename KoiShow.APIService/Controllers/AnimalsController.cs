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

namespace KoiShow.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        //private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;
        private readonly AnimalService _animalService;

        public AnimalsController(AnimalService animalService)
        {
            //_context = context;
            _animalService = animalService;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<IBusinessResult> GetAnimals()
        {
            return await _animalService.GetAll();
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetAnimal(int id)
        {
            var animal = await _animalService.GetById(id);

            return animal;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutAnimal(int id, Animal animal)
        {
            return await _animalService.Save(animal);
        }

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostAnimal(Animal animal)
        {
            return await _animalService.Save(animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAnimal(int id)
        {
            return await _animalService.DeleteById(id);
        }

        private bool AnimalExists(int id)
        {
            return _animalService.GetById(id) != null;
        }
    }
}
