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
    public class VarietiesController : ControllerBase
    {
        private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;
        private readonly VarietyService _varietyService;

        public VarietiesController(FA24_SE1716_PRN231_G2_KoiShowContext context, VarietyService varietyService)
        {
            _context = context;
            _varietyService = varietyService;
        }

        // GET: api/Varieties
        [HttpGet]
        public async Task<IBusinessResult> GetVarieties()
        {
            return await _varietyService.GetAll();
        }

        // GET: api/Varieties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Variety>> GetVariety(int id)
        {
            var variety = await _context.Varieties.FindAsync(id);

            if (variety == null)
            {
                return NotFound();
            }

            return variety;
        }

        // PUT: api/Varieties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVariety(int id, Variety variety)
        {
            if (id != variety.Id)
            {
                return BadRequest();
            }

            _context.Entry(variety).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VarietyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Varieties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Variety>> PostVariety(Variety variety)
        {
            _context.Varieties.Add(variety);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVariety", new { id = variety.Id }, variety);
        }

        // DELETE: api/Varieties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariety(int id)
        {
            var variety = await _context.Varieties.FindAsync(id);
            if (variety == null)
            {
                return NotFound();
            }

            _context.Varieties.Remove(variety);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VarietyExists(int id)
        {
            return _context.Varieties.Any(e => e.Id == id);
        }
    }
}
