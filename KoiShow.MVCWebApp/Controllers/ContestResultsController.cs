using KoiShow.Common;
using KoiShow.Data.Models;
using KoiShow.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiShow.MVCWebApp.Controllers
{
    public class ContestResultsController : Controller
    {
        private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;

        public ContestResultsController(FA24_SE1716_PRN231_G2_KoiShowContext context)
        {
            _context = context;
        }

        // GET: ContestResults
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"ContestResults"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<ContestResult>>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }

            return View(new List<ContestResult>());
        }

        // GET: ContestResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"ContestResults/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ContestResult>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }

            return NotFound();
        }

        public async Task<List<Contest>> GetContest()
        {
            List<Contest> contests = new();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Contest"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Contest>>(result.Data.ToString());

                            contests = data;
                        }
                    }
                }
            }

            return contests;
        }

        // GET: ContestResults/Create
        public async Task<IActionResult> Create()
        {
            var data = await this.GetContest();
            ViewData["ContestId"] = new SelectList(await this.GetContest(), "Id", "ContestName");
            return View();
        }

        // POST: ContestResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContestResultId,ContestId,ContestResultName,ContestResultDescription,TotalScore,Rank,Comments,IsFinalized,IsPublished,Category,Status,Prize,PrizeDescription")] ContestResult contestResult)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response =
                           await httpClient.PostAsJsonAsync(Const.APIEndPoint + $"ContestResults", contestResult))
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
                ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestName", contestResult.ContestId);
                return View(contestResult);
            }
        }

        // GET: ContestResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContestResult contestResult = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"ContestResults/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            contestResult = JsonConvert.DeserializeObject<ContestResult>(result.Data.ToString());
                        }
                    }
                }
            }

            if (contestResult == null)
            {
                return NotFound();
            }

            ViewData["ContestId"] = new SelectList(await this.GetContest(), "Id", "ContestName", contestResult.ContestId);
            return View(contestResult);
        }

        // POST: ContestResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContestResultId,ContestId,ContestResultName,ContestResultDescription,TotalScore,Rank,Comments,IsFinalized,IsPublished,Category,Status,Prize,PrizeDescription")] ContestResult contestResult)
        {
            if (id != contestResult.Id)
            {
                return NotFound();
            }

            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response =
                           await httpClient.PutAsJsonAsync(Const.APIEndPoint + $"ContestResults/{id}", contestResult))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
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
                ViewData["ContestId"] = new SelectList(_context.Contests, "ContestId", "ContestName", contestResult.ContestId);
                return View(contestResult);
            }
        }

        // GET: ContestResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContestResult contestResult = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"ContestResults/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            contestResult = JsonConvert.DeserializeObject<ContestResult>(result.Data.ToString());
                        }
                    }
                }
            }

            if (contestResult == null)
            {
                return NotFound();
            }

            return View(contestResult);
        }

        // POST: ContestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            bool deleteStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response =
                           await httpClient.DeleteAsync(Const.APIEndPoint + $"ContestResults/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                deleteStatus = true;
                            }
                            else
                            {
                                deleteStatus = false;
                            }
                        }
                    }
                }
            }

            if (deleteStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction();
            }
        }
    }
}
