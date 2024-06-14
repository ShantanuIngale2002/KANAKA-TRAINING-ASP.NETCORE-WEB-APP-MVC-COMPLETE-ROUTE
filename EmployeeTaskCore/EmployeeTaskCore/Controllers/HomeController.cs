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
        public IActionResult Login(EmployeeLoginModel model)
        {
            if (ModelState.IsValid)
            {
                int empId = employeeRepo.FetchEmployeeId(model);
                if (empId == 0)
                {
                    ViewBag.UserNotFound = "User does not exist";
                    return RedirectToAction("Login", "Home");
                }
                var displayModel = employeeRepo.GetEmployee(empId);
                if (displayModel!=null)
                {
                    HttpContext.Session.SetString("Username", displayModel.EmployeeName.ToString());
                    HttpContext.Session.SetString("acquiredID", empId.ToString());
                    if(displayModel.EmployeeRole == "Admin")
                    {
                        HttpContext.Session.SetString("acquiredPerson", "admin");
                        return RedirectToAction("AdminDashboard","Admin");
                    }
                    else if(displayModel.EmployeeRole == "Manager")
                    {
                        HttpContext.Session.SetString("acquiredPerson", "manager");
                        return RedirectToAction("EmployeeDashboard", "Employee");
                    }
                    else
                    {
                        HttpContext.Session.SetString("acquiredPerson", "employee");
                        return RedirectToAction("EmployeeDashboard", "Employee");
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
