using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Export_SQL_Server_To_Excel_ASP.net_Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ClosedXML.Excel;
using System.IO;


namespace Export_SQL_Server_To_Excel_ASP.net_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private AppDbContext db = null;

        public EmployeesController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<Employee> data = await db.Employees.ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Export()
        {
    
            if (ModelState.IsValid)
            {
                List<Employee> data =  db.Employees.ToList();

                var builder = new StringBuilder();
                builder.AppendLine("EmployeeID,FirstName");
                foreach (var employee in data)
                {
                    builder.AppendLine($"{employee.EmployeeID},{employee.FirstName}");
                }

                return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Employees.csv");
            }
            else
            {
                return BadRequest();
            }
            
            /*
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "EmployeeID";
                worksheet.Cell(currentRow, 2).Value = "FirstName";
                foreach (var employee in data)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = employee.EmployeeID;
                    worksheet.Cell(currentRow, 2).Value = employee.FirstName;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();


                    return File(
                        fileContents: content,
                         contentType: "applicaton/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                         fileDownloadName: "employees.xlsx");
                }; 

            }
            */
        }
    }
}

