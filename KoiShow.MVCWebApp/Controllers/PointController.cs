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

        // GET: Points
        public async Task<IActionResult> Index(
            string sortOrder,
            int pageNumber = 1,
            int pageSize = 5,
            string searchTerm = "")
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(Const.APIEndPoint + "Points");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                if (result != null && result.Data != null)
                {
                    var data = JsonConvert.DeserializeObject<List<PointResponseDTO>>(result.Data.ToString());

                    // Search filter
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        data = data.Where(x =>
                            x.JuryName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            x.AnimalName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            x.ContestName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    // Sorting parameters
                    ViewBag.AnimalNameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                    ViewBag.TotalPointSortParam = sortOrder == "total_asc" ? "total_desc" : "total_asc";
                    ViewBag.ColorPointSortParam = sortOrder == "color_asc" ? "color_desc" : "color_asc";
                    ViewBag.PatternPointSortParam = sortOrder == "pattern_asc" ? "pattern_desc" : "pattern_asc";
                    ViewBag.ShapePointSortParam = sortOrder == "shape_asc" ? "shape_desc" : "shape_asc";

                    // Sorting logic
                    data = sortOrder switch
                    {
                        "name_desc" => data.OrderByDescending(x => x.AnimalName).ToList(),
                        "total_asc" => data.OrderBy(x => x.TotalScore).ToList(),
                        "total_desc" => data.OrderByDescending(x => x.TotalScore).ToList(),
                        "color_asc" => data.OrderBy(x => x.ColorPoint).ToList(),
                        "color_desc" => data.OrderByDescending(x => x.ColorPoint).ToList(),
                        "pattern_asc" => data.OrderBy(x => x.PatternPoint).ToList(),
                        "pattern_desc" => data.OrderByDescending(x => x.PatternPoint).ToList(),
                        "shape_asc" => data.OrderBy(x => x.ShapePoint).ToList(),
                        "shape_desc" => data.OrderByDescending(x => x.ShapePoint).ToList(),
                        _ => data.OrderBy(x => x.AnimalName).ToList(),
                    };

                    // Pagination logic
                    var totalResults = data.Count;
                    var totalPages = (int)Math.Ceiling(totalResults / (double)pageSize);
                    var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    ViewBag.CurrentPage = pageNumber;
                    ViewBag.TotalPages = totalPages;
                    ViewBag.SearchTerm = searchTerm; // Keep search term
                    ViewBag.PageSize = pageSize;

                    return View(pagedData);
                }
            }
            return View(new List<PointResponseDTO>());
        }

        // GET: Animals/Create
        public async Task<IActionResult> Create()
        {
            // Lấy danh sách các RegisterForms
            var registerForms = await this.GetRegisterForms();


            // Kết hợp các thông tin vào danh sách để sử dụng trong dropdown
            var combinedItems = registerForms.Select(r => new
            {
                Id = r.Id,
                Text = $"{r.EntryTitle} - Animal: {r.AnimalId}, Contest: {r.ContestId}" // Kết hợp thông tin
            });

            ViewBag.RegisterFormId = new SelectList(combinedItems, "Id", "Text");

            return View(new PointRequestDTO()); // Trả về model rỗng
        }



        // POST: Points/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegisterFormId,JuryId,ShapePoint,ColorPoint,PatternPoint,Comment,PointStatus,JudgeRank,Penalties,TotalScore")] PointRequestDTO pointRequestDTO)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                var point = new Point
                {
                    RegisterFormId = pointRequestDTO.RegisterFormId,
                    JuryId = 1,
                    ShapePoint = pointRequestDTO.ShapePoint,
                    ColorPoint = pointRequestDTO.ColorPoint,
                    PatternPoint = pointRequestDTO.PatternPoint,
                    Comment = pointRequestDTO.Comment,
                    PointStatus = 1,
                    JudgeRank = pointRequestDTO.JudgeRank,
                    Penalties = pointRequestDTO.Penalties,
                    TotalScore = pointRequestDTO.ShapePoint + pointRequestDTO.ColorPoint + pointRequestDTO.PatternPoint
                };

                using (var httpClient = new HttpClient())
                {
                    using (var response =
                           await httpClient.PostAsJsonAsync(Const.APIEndPoint + $"Points", point))
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
                ViewData["RegisterFormId"] = new SelectList(await this.GetRegisterForms(), "Id", "ContestName");
                return View(pointRequestDTO);
            }
        }

        // Các phương thức khác giữ nguyên...

        // Helper methods to get RegisterForms and Accounts
        private async Task<IEnumerable<RegisterForm>> GetRegisterForms()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(Const.APIEndPoint + "RegisterForm");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                return JsonConvert.DeserializeObject<List<RegisterForm>>(result.Data.ToString());
            }

            return Enumerable.Empty<RegisterForm>();
        }

        // GET: Points/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Get existing point data
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{Const.APIEndPoint}Points/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                if (result != null && result.Data != null)
                {
                    var point = JsonConvert.DeserializeObject<PointResponseDTO>(result.Data.ToString());
                    var pointRequestDTO = new PointRequestDTO
                    {
                        RegisterFormId = point.RegisterFormId.Value,
                        ShapePoint = point.ShapePoint.Value,
                        ColorPoint = point.ColorPoint.Value,
                        PatternPoint = point.PatternPoint.Value,
                        Comment = point.Comment,
                        JudgeRank = point.JudgeRank,
                        Penalties = point.Penalties.Value
                        // You can also map other fields if necessary
                    };

                    // Get register forms for dropdown
                    var registerForms = await this.GetRegisterForms();
                    var combinedItems = registerForms.Select(r => new
                    {
                        Id = r.Id,
                        Text = $"{r.EntryTitle} - Animal: {r.AnimalId}, Contest: {r.ContestId}" // Combine information
                    });

                    ViewBag.RegisterFormId = new SelectList(combinedItems, "Id", "Text", point.RegisterFormId);
                    return View(pointRequestDTO);
                }
            }

            return NotFound();
        }

        // POST: Points/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegisterFormId,JuryId,ShapePoint,ColorPoint,PatternPoint,Comment,PointStatus,JudgeRank,Penalties,TotalScore")] PointRequestDTO pointRequestDTO)
        {
            if (id != pointRequestDTO.RegisterFormId) // You can validate using a suitable identifier
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var point = new Point
                {
                    RegisterFormId = pointRequestDTO.RegisterFormId,
                    JuryId = 1,
                    ShapePoint = pointRequestDTO.ShapePoint,
                    ColorPoint = pointRequestDTO.ColorPoint,
                    PatternPoint = pointRequestDTO.PatternPoint,
                    Comment = pointRequestDTO.Comment,
                    PointStatus = 1,
                    JudgeRank = pointRequestDTO.JudgeRank,
                    Penalties = pointRequestDTO.Penalties,
                    TotalScore = pointRequestDTO.ShapePoint + pointRequestDTO.ColorPoint + pointRequestDTO.PatternPoint
                };

                using var httpClient = new HttpClient();
                using var response = await httpClient.PutAsJsonAsync($"{Const.APIEndPoint}Points/{id}", point);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            // If we got this far, something failed; re-load the dropdown and return the view.
            ViewBag.RegisterFormId = new SelectList(await this.GetRegisterForms(), "Id", "Text", pointRequestDTO.RegisterFormId);
            return View(pointRequestDTO);
        }

        // GET: Points/Details/5
        public async Task<IActionResult> Details(int id)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(Const.APIEndPoint + $"Points/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                if (result != null && result.Data != null)
                {
                    var point = JsonConvert.DeserializeObject<Point>(result.Data.ToString());
                    return View(point);
                }
            }

            return NotFound(); // Return a 404 if the point was not found
        }


        // GET: Points/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(Const.APIEndPoint + $"Points/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                if (result != null && result.Data != null)
                {
                    var point = JsonConvert.DeserializeObject<Point>(result.Data.ToString());
                    return View(point);
                }
            }

            return NotFound(); // Return a 404 if the point was not found
        }

        // POST: Points/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.DeleteAsync(Const.APIEndPoint + $"Points/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); // Redirect to Index after successful deletion
            }

            return NotFound(); // Return a 404 if the deletion failed
        }


    }
}
