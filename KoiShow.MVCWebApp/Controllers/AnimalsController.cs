using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiShow.Data.Models;
using KoiShow.Common;
using KoiShow.Service.Base;
using Newtonsoft.Json;

namespace KoiShow.MVCWebApp.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;

        public AnimalsController(FA24_SE1716_PRN231_G2_KoiShowContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Animals"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Animal>>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }

            return View(new List<Contest>());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Animals/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Animal>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }
            return View(new Animal());
        }

        public async Task<List<Variety>> GetVarieties()
        {
            List<Variety> variety = new();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Varieties"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var varieties = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(varieties);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Variety>>(result.Data.ToString());

                            variety = data;
                        }
                    }
                }
            }

            return variety;
        }

        // GET: Animals/Create
        public async Task<IActionResult> Create()
        {
            var data = await this.GetVarieties();
            ViewData["VarietyId"] = new SelectList(await this.GetVarieties(), "Id", "Name");
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalName,VarietyId,Size,BirthDate,ImgLink,OwnerId,Weight,Description,HeathStatus,Gender,Id,CreatedBy,LastUpdatedBy,DeletedBy,CreatedTime,LastUpdatedTime,DeletedTime")] Animal animal)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response =
                           await httpClient.PostAsJsonAsync(Const.APIEndPoint + $"Animals", animal))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["VarietyId"] = new SelectList(await this.GetVarieties(), "Id", "Name");
                return View(animal);
            }
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Accounts, "Id", "Id", animal.OwnerId);
            ViewData["VarietyId"] = new SelectList(_context.Varieties, "Id", "Id", animal.VarietyId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalName,VarietyId,Size,BirthDate,ImgLink,OwnerId,Weight,Description,HeathStatus,Gender,Id,CreatedBy,LastUpdatedBy,DeletedBy,CreatedTime,LastUpdatedTime,DeletedTime")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Accounts, "Id", "Id", animal.OwnerId);
            ViewData["VarietyId"] = new SelectList(_context.Varieties, "Id", "Id", animal.VarietyId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Owner)
                .Include(a => a.Variety)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
    }
}
