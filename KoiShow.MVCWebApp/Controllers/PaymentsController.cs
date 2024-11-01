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
using KoiShow.MVCWebApp.Helpers;
using KoiShow.Common.DTO.DTORequest;
using KoiShow.Common.DTO.DtoResponse;
using System.Net.Http;
using System.Text;

namespace KoiShow.MVCWebApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;

        public PaymentsController(FA24_SE1716_PRN231_G2_KoiShowContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            List<Payment> payments = new List<Payment>();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Const.APIEndPoint + "Payments");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                    if (result != null && result.Data != null)
                    {
                        payments = JsonConvert.DeserializeObject<List<Payment>>(result.Data.ToString());
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to load payments from API.");
                }
            }

            var pagedPayments = PaginationHelper<Payment>.GetPagedData(payments.AsQueryable(), pageNumber, pageSize);
            return View(pagedPayments);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Payments/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Payment>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }
            return View(new Payment());
        }

        public async Task<IActionResult> SearchPayments(string transactionId, string description, string paymentStatus, int pageNumber = 1, int pageSize = 5)
        {
            // Validate if at least one parameter is provided
            if (string.IsNullOrEmpty(transactionId) && string.IsNullOrEmpty(description) && string.IsNullOrEmpty(paymentStatus))
            {
                return RedirectToAction(nameof(Index));
            }

            List<Payment> searchedPayments = new List<Payment>();

            using (var httpClient = new HttpClient())
            {
                // Construct the query string with the provided parameters
                var query = $"transactionId={transactionId}&description={description}&paymentStatus={paymentStatus}";
                var response = await httpClient.GetAsync(Const.APIEndPoint + $"Payments/search?{query}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                    if (result != null && result.Data != null)
                    {
                        searchedPayments = JsonConvert.DeserializeObject<List<Payment>>(result.Data.ToString());
                    }
                }
            }

            // Paginate the search results as well
            var pagedPayments = PaginationHelper<Payment>.GetPagedData(searchedPayments.AsQueryable(), pageNumber, pageSize);

            return View("Index", pagedPayments);
        }

        public async Task<List<RegisterForm>> GetRegisterForm()
        {
            List<RegisterForm> registerForms = new();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"RegisterForm"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<RegisterForm>>(result.Data.ToString());

                            registerForms = data;
                        }
                    }
                }
            }

            return registerForms;
        }
        public async Task<IActionResult> Create()
        {
            ViewData["RegisterFormId"] = new SelectList(await GetRegisterForm(), "RegisterFormId", "RegisterFormId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegisterFormId,TransactionId,PaymentAmount,PaymentDate,PaymentStatus,Description,Currency,OrderType")] PaymentDto paymentDto)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    // Send PaymentDto to the API
                    var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "Payments/create", paymentDto);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to create payment.");
                    }
                }
            }

            ViewData["RegisterFormId"] = new SelectList(await GetRegisterForm(), "RegisterFormId", "RegisterFormId", paymentDto.RegisterFormId);
            return View(paymentDto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Payment payment = null;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Const.APIEndPoint + $"Payments/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                    if (result != null && result.Data != null)
                    {
                        payment = JsonConvert.DeserializeObject<Payment>(result.Data.ToString());
                    }
                }
            }

            if (payment == null) return NotFound();

            ViewData["RegisterFormId"] = new SelectList(await GetRegisterForm(), "RegisterFormId", "RegisterFormId", payment.RegisterFormId);
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Payment payment)
        {
            if (id != payment.Id) return NotFound();

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + $"Payments/{id}/paid", payment);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        // Check if URL generation was successful
                        if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE && result.Data != null)
                        {
                            // Extract the PaymentUrl from the response
                            var paymentData = JsonConvert.DeserializeObject<dynamic>(result.Data.ToString());
                            string paymentUrl = paymentData.paymentUrl.data.paymentUrl.ToString();

                            // Redirect user to VNPay URL
                            return Redirect(paymentUrl);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to update payment or generate payment URL.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to update payment.");
                    }
                }
            }

            ViewData["RegisterFormId"] = new SelectList(await GetRegisterForm(), "RegisterFormId", "RegisterFormId", payment.RegisterFormId);
            return View(payment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Payment payment = null;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Const.APIEndPoint + $"Payments/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                    if (result != null && result.Data != null)
                    {
                        payment = JsonConvert.DeserializeObject<Payment>(result.Data.ToString());
                    }
                }
            }

            if (payment == null) return NotFound();

            ViewData["RegisterFormId"] = new SelectList(await GetRegisterForm(), "RegisterFormId", "RegisterFormId", payment.RegisterFormId);
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Payment payment)
        {
            if (id != payment.Id) return NotFound();

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PutAsJsonAsync(Const.APIEndPoint + $"Payments/{id}/cancel", payment);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to update payment.");
                    }
                }
            }

            ViewData["RegisterFormId"] = new SelectList(await GetRegisterForm(), "RegisterFormId", "RegisterFormId", payment.RegisterFormId);
            return View(payment);
        }
    }
}
