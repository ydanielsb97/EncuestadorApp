using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestadorApp.Logic1.Controllers
{
    public class EncuestasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
