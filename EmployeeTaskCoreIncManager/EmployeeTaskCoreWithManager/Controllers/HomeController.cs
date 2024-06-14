using CoreTaskEmployee.Models;
using CoreTaskEmployee.Repository.Interface;
using EmployeeTaskCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeTaskCore.Controllers
{
    public class HomeController : Controller
    {
        IEmployeeRepository employeeRepo;

        public HomeController(IEmployeeRepository employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(EmployeeIdModel model)
        {
            if (ModelState.IsValid)
            {
                int empId = (int)model.empID;
                var displayModel = employeeRepo.GetEmployee(empId);
                if (displayModel!=null)
                {
                    HttpContext.Session.SetString("acquiredID", empId.ToString());
                    if(displayModel.EmployeeRole != "Admin")
                    {
                        HttpContext.Session.SetString("acquiredPerson", "employee");
                        return RedirectToAction("EmployeeDashboard", "Employee");
                    }
                    else
                    {
                        HttpContext.Session.SetString("acquiredPerson", "admin");
                        return RedirectToAction("Login","Admin");
                    }
                }
            }
            return View();
        }

        public IActionResult EmployeeOrAdmin()
        {
            if(HttpContext.Session.GetString("acquiredPerson") == "admin")
            {
                return RedirectToAction("AdminDashboard", "Admin");
            }
            else
            {
                return RedirectToAction("EmployeeDashboard", "Employee");
            }
        }














        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
