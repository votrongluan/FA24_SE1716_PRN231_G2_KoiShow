using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiShow.Data.Models;
using KoiShow.Common;
using Newtonsoft.Json;
using KoiShow.Service.Base;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KoiShow.MVCWebApp.Controllers
{
    public class ContestsController : Controller
    {
        private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;

        public ContestsController(FA24_SE1716_PRN231_G2_KoiShowContext context)
        {
            _context = context;
        }

        // GET: Contests
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Contests.ToListAsync());
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Contests"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Contest>>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }

            return View(new List<Contest>());

        }

        // GET: Contests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var contest = await _context.Contests
            //    .FirstOrDefaultAsync(m => m.ContestId == id);
            //if (contest == null)
            //{
            //    return NotFound();
            //}

            //return View(contest);

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Contests/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Contest>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }
            return View(new Contest());
        }

        // GET: Contests/Create
        public IActionResult Create()
        {
            //ViewData["BankId"] = new SelectList(await this.Get.Banks(), "BankId", "FullName");
            return View();
        }

        // POST: Contests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContestId,ContestName,Title,Description,StartDate,EndDate,Location,CompetitionType,Status,Participants,Image,ContactInfo,ShapePointPercent,ColorPointPercent,PatternPointPercent")] Contest contest)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "Contests/", contest))
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
                //_context.Add(contest);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //ViewData["BankId"] = new SelectList(await this.Get.Banks(), "BankId", "FullName", catBankAccount.BankId);
                return View(contest);
            } 
        }

        // GET: Contests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contest = await _context.Contests.FindAsync(id);
            if (contest == null)
            {
                return NotFound();
            }
            return View(contest);
        }

        // POST: Contests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContestId,ContestName,Title,Description,StartDate,EndDate,Location,CompetitionType,Status,Participants,Image,ContactInfo,ShapePointPercent,ColorPointPercent,PatternPointPercent")] Contest contest)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    //using (var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + "Contests", contest))
                    using (var response = await httpClient.PutAsJsonAsync($"{Const.APIEndPoint}Contests/{id}", contest))
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
                //ViewData["BankId"] = new SelectList(await this.Get.Banks(), "BankId", "FullName", catBankAccount.BankId);
                return View(contest);
            }
            //if (id != contest.ContestId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(contest);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ContestExists(contest.ContestId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(contest);
        }

        // GET: Contests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Contests/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Contest>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }
            return View(new Contest());
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var contest = await _context.Contests
            //    .FirstOrDefaultAsync(m => m.ContestId == id);
            //if (contest == null)
            //{
            //    return NotFound();
            //}

            //return View(contest);
        }

        // POST: Contests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync($"{Const.APIEndPoint}Contests/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
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
                //ViewData["BankId"] = new SelectList(await this.Get.Banks(), "BankId", "FullName", catBankAccount.BankId);
                return RedirectToAction(nameof(Delete));
            }
            //var contest = await _context.Contests.FindAsync(id);
            //if (contest != null)
            //{
            //    _context.Contests.Remove(contest);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private bool ContestExists(int id)
        {
            return _context.Contests.Any(e => e.Id == id);
        }
    }
}
