﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSO.Controllers
{
    public class Account1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}