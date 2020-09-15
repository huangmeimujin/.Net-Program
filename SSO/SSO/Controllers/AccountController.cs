using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SSO.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
         
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Regist()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}