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
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 2, string SearchTermContestResultName = "", string SearchTermWinnerName = "", string SearchTermRank = "")
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

                            if (!string.IsNullOrEmpty(SearchTermContestResultName) || !string.IsNullOrEmpty(SearchTermRank) || !string.IsNullOrEmpty(SearchTermWinnerName))
                            {
                                data = data.Where(
                                x =>
                                (x.ContestResultName?.Contains(SearchTermContestResultName, StringComparison.OrdinalIgnoreCase) ?? false) &&
                                (x.WinnerName?.Contains(SearchTermWinnerName, StringComparison.OrdinalIgnoreCase) ?? false) &&
                                (x.Rank?.ToString().Contains(SearchTermRank, StringComparison.OrdinalIgnoreCase) ?? false)
                                ).ToList();
                            }

                            var totalResults = data.Count;
                            var totalPages = (int)Math.Ceiling(totalResults / (double)pageSize);
                            var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                            ViewBag.CurrentPage = pageNumber;
                            ViewBag.TotalPages = totalPages;

                            ViewBag.SearchTermContestResultName = SearchTermContestResultName;
                            ViewBag.SearchTermWinnerName = SearchTermWinnerName;
                            ViewBag.SearchTermRank = SearchTermRank;

                            ViewBag.PageSize = pageSize;

                            return View(pagedData);
                        }
                    }
                }
            }

            return View(new List<ContestResult>());
        }

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
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Contests"))
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

        public async Task<IActionResult> Create()
        {
            var data = await this.GetContest();
            ViewData["ContestId"] = new SelectList(await this.GetContest(), "Id", "ContestName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContestId,ContestResultName,ContestResultDescription,TotalScore,Rank,Comments,WinnerName,IsPublished,Category,Prize,PrizeDescription")] ContestResult contestResult)
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
                ViewData["ContestId"] = new SelectList(await this.GetContest(), "Id", "ContestName");
                return View(contestResult);
            }
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContestId,ContestResultName,ContestResultDescription,TotalScore,Rank,Comments,WinnerName,IsPublished,Category,Prize,PrizeDescription")] ContestResult contestResult)
        {
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
                ViewData["ContestId"] = new SelectList(await this.GetContest(), "Id", "ContestName");
                return View(contestResult);
            }
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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
                return RedirectToAction(nameof(Delete));
            }
        }
    }
}