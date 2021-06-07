using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Controllers
{
    public class QrcodeController : Controller
    {
        public IActionResult Generate()
        {
            return View();
        }
    }
}
