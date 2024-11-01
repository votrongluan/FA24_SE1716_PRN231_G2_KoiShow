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
        public async Task<IActionResult> Index(string sortOrder, int pageNumber = 1, int pageSize = 4, string searchName = "",
                string searchVariety = "", string searchHeathStatus = "")
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

                            if (!string.IsNullOrEmpty(searchName))
                            {
                                data = data.Where(x => x.AnimalName.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
                            }
                            if (!string.IsNullOrEmpty(searchVariety))
                            {
                                data = data.Where(x => x.Variety.Name.Contains(searchVariety, StringComparison.OrdinalIgnoreCase)).ToList();
                            }
                            if (!string.IsNullOrEmpty(searchHeathStatus))
                            {
                                data = data.Where(x => x.HeathStatus.Contains(searchHeathStatus, StringComparison.OrdinalIgnoreCase)).ToList();
                            }

                            // Default sort order
                            ViewBag.AnimalNameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                            ViewBag.SizeSortParam = sortOrder == "size_asc" ? "size_desc" : "size_asc";
                            ViewBag.BirthDateSortParam = sortOrder == "birth_asc" ? "birth_desc" : "birth_asc";
                            ViewBag.WeightSortParam = sortOrder == "weight_asc" ? "weight_desc" : "weight_asc";
                            ViewBag.HeathStatusSortParam = sortOrder == "heath_asc" ? "heath_desc" : "heath_asc";
                            ViewBag.GenderSortParam = sortOrder == "gender_asc" ? "gender_desc" : "gender_asc";
                            ViewBag.VarietySortParam = sortOrder == "variety_asc" ? "variety_desc" : "variety_asc";

                            // Sort data
                            switch (sortOrder)
                            {
                                case "name_desc":
                                    data = data.OrderByDescending(x => x.AnimalName).ToList();
                                    break;
                                case "size_asc":
                                    data = data.OrderBy(x => x.Size).ToList();
                                    break;
                                case "size_desc":
                                    data = data.OrderByDescending(x => x.Size).ToList();
                                    break;
                                case "birth_asc":
                                    data = data.OrderBy(x => x.BirthDate).ToList();
                                    break;
                                case "birth_desc":
                                    data = data.OrderByDescending(x => x.BirthDate).ToList();
                                    break;
                                case "weight_asc":
                                    data = data.OrderBy(x => x.Weight).ToList();
                                    break;
                                case "weight_desc":
                                    data = data.OrderByDescending(x => x.Weight).ToList();
                                    break;
                                case "heath_asc":
                                    data = data.OrderBy(x => x.HeathStatus).ToList();
                                    break;
                                case "heath_desc":
                                    data = data.OrderByDescending(x => x.HeathStatus).ToList();
                                    break;
                                case "gender_asc":
                                    data = data.OrderBy(x => x.Gender).ToList();
                                    break;
                                case "gender_desc":
                                    data = data.OrderByDescending(x => x.Gender).ToList();
                                    break;
                                case "variety_asc":
                                    data = data.OrderBy(x => x.Variety.Name).ToList();
                                    break;
                                case "variety_desc":
                                    data = data.OrderByDescending(x => x.Variety.Name).ToList();
                                    break;
                                default:
                                    data = data.OrderBy(x => x.AnimalName).ToList();
                                    break;
                            }

                            var totalResults = data.Count;
                            var totalPages = (int)Math.Ceiling(totalResults / (double)pageSize);
                            var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                            ViewBag.CurrentPage = pageNumber;
                            ViewBag.TotalPages = totalPages;
                            ViewBag.SearchName = searchName;
                            ViewBag.SearchVariety = searchVariety;
                            ViewBag.SearchHeathStatus = searchHeathStatus;

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

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalName,VarietyId,Size,BirthDate,ImgLink,OwnerId,Weight,Description,HeathStatus,Gender")] Animal animal)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "Animals", animal))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
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
