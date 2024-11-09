using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using KoiShow.Common;
using KoiShow.Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KoiShow.Service.Base;

namespace KoiShow.MVCWebApp.Controllers
{
    public class RegisterFormsController : Controller
    {
        private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;

        public RegisterFormsController(FA24_SE1716_PRN231_G2_KoiShowContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(Const.APIEndPoint + "RegisterForm");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result?.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<RegisterForm>>(result.Data.ToString());

                            // Apply search filter if searchTerm is provided
                            if (!string.IsNullOrEmpty(searchTerm))
                            {
                                data = data.Where(d =>
                                    (d.EntryTitle != null && d.EntryTitle.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                                    (d.CheckinStatus != null && d.CheckinStatus.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                                    (d.Notes != null && d.Notes.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                                ).ToList();
                            }

                            ViewBag.SearchTerm = searchTerm; // Pass searchTerm to the view for display
                            return View(data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception for future debugging
                    // _logger.LogError(ex, "Error occurred while fetching RegisterForms from API.");
                }
            }

            // Return an empty view if the request fails
            return View(new List<RegisterForm>());
        }

        // GET: RegisterForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("ID cannot be null.");
            }

            try
            {
                var registerForm = await _context.RegisterForms
                    .Include(r => r.Animal)
                    .Include(r => r.Contest)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (registerForm == null)
                {
                    return NotFound($"RegisterForm with ID {id} not found.");
                }

                return View(registerForm);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, $"Error fetching RegisterForm with ID {id}.");

                return StatusCode(500, "Internal server error.");
            }
        }

        // GET: RegisterForms/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "AnimalName");
                ViewData["ContestId"] = new SelectList(_context.Contests, "Id", "CompetitionType");

                return View();
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error occurred while loading the Create form.");

                return StatusCode(500, "An error occurred while preparing the form.");
            }
        }

        // POST: RegisterForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContestId,AnimalId,EntryTitle,CheckinStatus,RegistrationDate,ApprovalStatus,Notes,Status,Image,HealthStatus,Color,Shape,Pattern")] RegisterForm registerForm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(registerForm);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "AnimalName", registerForm.AnimalId);
                ViewData["ContestId"] = new SelectList(_context.Contests, "Id", "CompetitionType", registerForm.ContestId);
                return View(registerForm);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error occurred while creating a new RegisterForm.");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET: RegisterForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("ID cannot be null.");
            }

            try
            {
                var registerForm = await _context.RegisterForms.FindAsync(id);
                if (registerForm == null)
                {
                    return NotFound($"RegisterForm with ID {id} not found.");
                }

                ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "AnimalName", registerForm.AnimalId);
                ViewData["ContestId"] = new SelectList(_context.Contests, "Id", "CompetitionType", registerForm.ContestId);

                return View(registerForm);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error occurred while retrieving the RegisterForm for editing.");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST: RegisterForms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContestId,AnimalId,EntryTitle,CheckinStatus,RegistrationDate,ApprovalStatus,Notes,Status,Image,HealthStatus,Color,Shape,Pattern")] RegisterForm registerForm)
        {
            if (id != registerForm.Id)
            {
                return BadRequest("Mismatched ID in request.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerForm);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterFormExists(registerForm.Id))
                    {
                        return NotFound($"RegisterForm with ID {registerForm.Id} no longer exists.");
                    }

                    throw;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    // _logger.LogError(ex, "Error occurred while updating RegisterForm.");

                    return StatusCode(500, "An error occurred while updating the RegisterForm.");
                }
            }

            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "AnimalName", registerForm.AnimalId);
            ViewData["ContestId"] = new SelectList(_context.Contests, "Id", "CompetitionType", registerForm.ContestId);

            return View(registerForm);
        }

        // GET: RegisterForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("ID cannot be null.");
            }

            try
            {
                var registerForm = await _context.RegisterForms
                    .Include(r => r.Animal)
                    .Include(r => r.Contest)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (registerForm == null)
                {
                    return NotFound($"RegisterForm with ID {id} not found.");
                }

                return View(registerForm);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error occurred while trying to retrieve RegisterForm for deletion.");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST: RegisterForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var registerForm = await _context.RegisterForms.FindAsync(id);
                if (registerForm == null)
                {
                    return NotFound($"RegisterForm with ID {id} not found or already deleted.");
                }

                _context.RegisterForms.Remove(registerForm);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error occurred while trying to delete RegisterForm.");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        private bool RegisterFormExists(int id)
        {
            return _context.RegisterForms.Any(e => e.Id == id);
        }
    }
}
