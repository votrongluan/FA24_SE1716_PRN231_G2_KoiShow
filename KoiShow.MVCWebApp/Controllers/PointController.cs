using KoiShow.Common;
using KoiShow.Data.DTO.PointDTO;
using KoiShow.Data.Models;
using KoiShow.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiShow.MVCWebApp.Controllers
{
    public class PointController : Controller
    {
        private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;

        public PointController(FA24_SE1716_PRN231_G2_KoiShowContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index(string sortOrder, int pageNumber = 1, int pageSize = 5, string searchTerm = "")
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Points"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Point>>(result.Data.ToString());

                            if (!string.IsNullOrEmpty(searchTerm))
                            {
                                data = data.Where(x => x.Jury.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                    || x.RegisterForm.Animal.AnimalName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                    || x.RegisterForm.Contest.ContestName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                            }

                            // Default sort order
                            ViewBag.AnimalNameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                            ViewBag.ShapePointSortParam = sortOrder == "shape_asc" ? "shape_desc" : "shape_asc";
                            ViewBag.ColorPointSortParam = sortOrder == "color_asc" ? "color_desc" : "color_asc";
                            ViewBag.PatternPointSortParam = sortOrder == "pattern_asc" ? "pattern_desc" : "pattern_asc";
                            ViewBag.TotalPointSortParam = sortOrder == "total_asc" ? "total_desc" : "total_asc";
                            ViewBag.JudgeRankSortParam = sortOrder == "judge_asc" ? "judge_desc" : "judge_asc";

                            // Sort data
                            switch (sortOrder)
                            {
                                case "name_desc":
                                    data = data.OrderByDescending(x => x.RegisterForm.Animal.AnimalName).ToList();
                                    break;
                                case "shape_asc":
                                    data = data.OrderBy(x => x.ShapePoint).ToList();
                                    break;
                                case "shape_desc":
                                    data = data.OrderByDescending(x => x.ShapePoint).ToList();
                                    break;
                                case "color_asc":
                                    data = data.OrderBy(x => x.ColorPoint).ToList();
                                    break;
                                case "color_desc":
                                    data = data.OrderByDescending(x => x.ColorPoint).ToList();
                                    break;
                                case "pattern_asc":
                                    data = data.OrderBy(x => x.PatternPoint).ToList();
                                    break;
                                case "pattern_desc":
                                    data = data.OrderByDescending(x => x.PatternPoint).ToList();
                                    break;
                                case "total_asc":
                                    data = data.OrderBy(x => x.TotalScore).ToList();
                                    break;
                                case "total_desc":
                                    data = data.OrderByDescending(x => x.TotalScore).ToList();
                                    break;
                                case "judge_asc":
                                    data = data.OrderBy(x => x.JudgeRank).ToList();
                                    break;
                                case "judge_desc":
                                    data = data.OrderByDescending(x => x.JudgeRank).ToList();
                                    break;
                                default:
                                    data = data.OrderBy(x => x.RegisterForm.Animal.AnimalName).ToList();
                                    break;
                            }

                            var totalResults = data.Count;
                            var totalPages = (int)Math.Ceiling(totalResults / (double)pageSize);
                            var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                            ViewBag.CurrentPage = pageNumber;
                            ViewBag.TotalPages = totalPages;
                            ViewBag.SearchTerm = searchTerm;
                            ViewBag.PageSize = pageSize;

                            return View(pagedData);
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
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Points/" + id))
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

        public async Task<List<RegisterForm>> RegisterForms()
        {
            List<Contest> contests = new();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"RegisterForm"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var tmpContests = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(tmpContests);

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

            Animal animal = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Animals/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            animal = JsonConvert.DeserializeObject<Animal>(result.Data.ToString());
                        }
                    }
                }
            }

            if (animal == null)
            {
                return NotFound();
            }

            ViewData["VarietyId"] = new SelectList(await this.GetVarieties(), "Id", "Name");
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnimalName,VarietyId,Size,BirthDate,ImgLink,OwnerId,Weight,Description,HeathStatus,Gender,Id,CreatedBy,LastUpdatedBy,DeletedBy,CreatedTime,LastUpdatedTime,DeletedTime")] Animal animal)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response =
                           await httpClient.PutAsJsonAsync(Const.APIEndPoint + $"Animals/{id}", animal))
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
                ViewData["VarietyId"] = new SelectList(await this.GetVarieties(), "Id", "Name");
                return View(animal);
            }
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Animal animal = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Animals/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            animal = JsonConvert.DeserializeObject<Animal>(result.Data.ToString());
                        }
                    }
                }
            }

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
            bool deleteStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response =
                           await httpClient.DeleteAsync(Const.APIEndPoint + $"Animals/{id}"))
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
