﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBookAPI.Controllers
{
    public class NotFoundController : Controller
    {
        [HttpGet]
        public IActionResult NoData()
        {
            return View();
        }
    }
}
