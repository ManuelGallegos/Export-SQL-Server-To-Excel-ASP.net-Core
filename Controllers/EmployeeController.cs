using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Export_SQL_Server_To_Excel_ASP.net_Core.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

    }
}
