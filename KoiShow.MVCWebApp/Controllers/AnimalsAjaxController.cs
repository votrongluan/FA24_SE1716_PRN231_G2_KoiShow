﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiShow.Data.Models;

namespace KoiShow.MVCWebApp.Controllers
{
    public class AnimalsAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
