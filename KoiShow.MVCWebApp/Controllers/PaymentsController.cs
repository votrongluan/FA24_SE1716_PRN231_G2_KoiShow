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
        public async Task<IActionResult> Create([Bind("RegisterFormId,TransactionId,PaymentAmount,PaymentDate,PaymentStatus,Description,Currency,OrderType")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    // Create PaymentDto object to send to API
                    var paymentDto = new PaymentDto
                    {
                        PaymentId = 0, // Assuming paymentId is auto-generated
                        RegisterFormId = payment.RegisterFormId ?? 0,
                        TransactionId = payment.TransactionId ?? "Mã giao dịch",
                        PaymentAmount = payment.PaymentAmount ?? 0.0,
                        PaymentDate = payment.PaymentDate ?? DateTime.Now,
                        PaymentStatus = payment.PaymentStatus ?? "Chờ xử lý",
                        Description = payment.Description ?? "Mô tả thanh toán",
                        Currency = payment.Currency ?? "VND",
                        OrderType = payment.OrderType ?? "Thanh toán trực tuyến"
                    };

                    // Call the API to create the payment and generate the payment URL
                    var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "Payments/create-and-generate-url", paymentDto);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(content); // Deserialize as dynamic to extract fields

                        // Check if the API returned success and if the data contains the payment URL
                        if (result != null && result.status == 1 && result.data != null)
                        {
                            // Extract the paymentUrl directly from the data object
                            var paymentUrl = result.data.paymentUrl.ToString();

                            // Check if the payment URL is not null or empty
                            if (!string.IsNullOrEmpty(paymentUrl))
                            {
                                // Redirect the user to the payment gateway URL
                                return Redirect(paymentUrl);
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Payment URL is null or empty. Could not redirect to payment gateway.");
                                return View(payment); // Stay on the current page with error
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to create payment and generate URL.");
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Failed to create payment. Error: {errorContent}");
                    }
                }
            }

            // If model is invalid or API call fails, reload the view with the input data
            ViewData["RegisterFormId"] = new SelectList(await GetRegisterForm(), "RegisterFormId", "RegisterFormId", payment.RegisterFormId);
            return View(payment);
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
