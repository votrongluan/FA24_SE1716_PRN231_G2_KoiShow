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

namespace KoiShow.MVCWebApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly FA24_SE1716_PRN231_G2_KoiShowContext _context;

        public PaymentsController(FA24_SE1716_PRN231_G2_KoiShowContext context)
        {
            _context = context;
        }

        // GET: Payments
        //public async Task<IActionResult> Index()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Payments"))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var content = await response.Content.ReadAsStringAsync();
        //                var result = JsonConvert.DeserializeObject<BusinessResult>(content);

        //                if (result != null && result.Data != null)
        //                {
        //                    var data = JsonConvert.DeserializeObject<List<Payment>>(result.Data.ToString());

        //                    return View(data);
        //                }
        //            }
        //        }
        //    }
        //    return View(new List<Payment>());
        //}

        public async Task<IActionResult> Index()
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
                    // Handle error response from the API (optional)
                    ModelState.AddModelError(string.Empty, "Failed to load payments from API.");
                }
            }
            return View(payments);
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

        // GET: Payments/Create
        //public async Task<IActionResult> Create()
        //{
        //    var data = await this.GetRegisterForm();
        //    ViewData["RegisterFormId"] = new SelectList(_context.RegisterForms, "RegisterFormId", "RegisterFormId");
        //    return View();
        //}

        //// POST: Payments/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PaymentId,RegisterFormId,TransactionId,PaymentAmount,PaymentDate,PaymentStatus,Description,Vatrate,ActualCost,DiscountAmount,FinalAmount,Currency")] Payment Payment)
        //{
        //    {
        //        bool saveStatus = false;

        //        if (ModelState.IsValid)
        //        {
        //            using (var httpClient = new HttpClient())
        //            {
        //                using (var response =
        //                       await httpClient.PostAsJsonAsync(Const.APIEndPoint + $"Payments", Payment))
        //                {
        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        var content = await response.Content.ReadAsStringAsync();
        //                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

        //                        if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
        //                        {
        //                            saveStatus = true;
        //                        }
        //                        else
        //                        {
        //                            saveStatus = false;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (saveStatus)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ViewData["RegisterFormId"] = new SelectList(_context.Contests, "ContestId", "CompetitionType", Payment.RegisterFormId);
        //            return View(Payment);
        //        }
        //    }
        //}

        public async Task<IActionResult> Create()
        {
            ViewData["RegisterFormId"] = new SelectList(await GetRegisterForm(), "RegisterFormId", "RegisterFormId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,RegisterFormId,TransactionId,PaymentAmount,PaymentDate,PaymentStatus,Description,Vatrate,ActualCost,DiscountAmount,FinalAmount,Currency")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsJsonAsync(Const.APIEndPoint + "Payments/create", payment);

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

            ViewData["RegisterFormId"] = new SelectList(await GetRegisterForm(), "RegisterFormId", "RegisterFormId", payment.RegisterFormId);
            return View(payment);
        }

        //// GET: Payments/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Payment Payments = null;

        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Payments/{id}"))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var content = await response.Content.ReadAsStringAsync();
        //                var result = JsonConvert.DeserializeObject<BusinessResult>(content);

        //                if (result != null && result.Data != null)
        //                {
        //                    Payments = JsonConvert.DeserializeObject<Payment>(result.Data.ToString());
        //                }
        //            }
        //        }
        //    }

        //    if (Payments == null)
        //    {
        //        return NotFound();
        //    }
        //        ViewData["RegisterFormId"] = new SelectList(_context.Contests, "RegisterFormId", "RegisterFormId", Payments.RegisterFormId);
        //        return View(Payments);
        //    }


        //    // POST: Paymentss/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, Payment payment)
        //    {
        //        if (id != payment.Id)
        //        {
        //            return NotFound();
        //        }

        //         bool saveStatus = false;

        //        if (ModelState.IsValid)
        //        {
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var response =
        //                   await httpClient.PutAsJsonAsync(Const.APIEndPoint + $"Payments/{id}", payment))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var content = await response.Content.ReadAsStringAsync();
        //                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);

        //                    if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
        //                    {
        //                        saveStatus = true;
        //                    }
        //                    else
        //                    {
        //                        saveStatus = false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //        if (saveStatus)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ViewData["RegisterFormId"] = new SelectList(_context.RegisterForms, "RegisterFormId", "ApprovalStatus", payment.RegisterFormId);
        //            return View(payment);
        //    }
        //}

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


        //// GET: Paymentss/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Payment Payments = null;

        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Payments/{id}"))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var content = await response.Content.ReadAsStringAsync();
        //                var result = JsonConvert.DeserializeObject<BusinessResult>(content);

        //                if (result != null && result.Data != null)
        //                {
        //                    Payments = JsonConvert.DeserializeObject<Payment>(result.Data.ToString());
        //                }
        //            }
        //        }
        //    }

        //    if (Payments == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(Payments);
        //}

        //// POST: Paymentss/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    bool deleteStatus = false;

        //    if (ModelState.IsValid)
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var response =
        //                   await httpClient.DeleteAsync(Const.APIEndPoint + $"ContestResults/{id}"))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var content = await response.Content.ReadAsStringAsync();
        //                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);

        //                    if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
        //                    {
        //                        deleteStatus = true;
        //                    }
        //                    else
        //                    {
        //                        deleteStatus = false;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    if (deleteStatus)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        return RedirectToAction(nameof(Delete));
        //    }


        //}

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

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(Const.APIEndPoint + $"Payments/{id}");

                if (response.IsSuccessStatusCode)
                {
                    deleteStatus = true;
                }
            }

            if (deleteStatus) return RedirectToAction(nameof(Index));

            return View();
        }
    }
}
