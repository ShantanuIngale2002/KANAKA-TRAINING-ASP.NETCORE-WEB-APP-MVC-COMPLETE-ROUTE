using CoreTaskEmployee.Data;
using CoreTaskEmployee.Models;
using CoreTaskEmployee.Repository.Interface;
using EmployeeTaskCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskCore.Controllers
{
    public class AdminController : Controller
    {
        IEmployeeRepository employeeRepo;

        public AdminController(IEmployeeRepository employeeRepo)
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
            bool proof = employeeRepo.GetAdmin(model);
            if(proof)
            {
                return RedirectToAction("AdminDashboard");
            }
            ModelState.Clear();
            return View();
        }

        public IActionResult AdminDashboard()
        {

            if (HttpContext.Session.GetString("acquiredID") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("acquiredID").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult DisplayEmployee()
        {
            return RedirectToAction("DisplayEmployee","Employee");
        }

        public IActionResult ProjectInformation()
        {
            return RedirectToAction("ProjectInformation", "Employee");
        }

        public IActionResult EmployeeLeave()
        {
            return RedirectToAction("EmployeeLeave", "Employee");
        }

        public IActionResult LeavesHistory()
        {
            return RedirectToAction("LeavesHistory", "Employee");
        }

        public IActionResult EmployeePaidLeaveApply()
        {
            return RedirectToAction("EmployeePaidLeaveApply", "Employee");
        }

        public IActionResult ApplyPaidLeave()
        {
            return RedirectToAction("ApplyPaidLeave", "Employee");
        }

        [HttpPost]
        public IActionResult ApplyPaidLeave(LeaveApplyModel model)
        {
            /*if (ModelState.IsValid)
            {
                string empId = HttpContext.Session.GetString("acquiredID").ToString();
                int getEmpId = Convert.ToInt32(empId);
                bool leaveTaken = employeeRepo.EmployeePaidLeaveApply(model, getEmpId);
                if (leaveTaken)
                {
                    return RedirectToAction("EmployeeLeave", "Employee");
                }
            }
            ModelState.Clear();
            return View();*/
            return RedirectToAction("ApplyPaidLeave", "Employee");
        }

        public IActionResult ApplyPaylossLeave()
        {
            return RedirectToAction("ApplyPaylossLeave", "Employee");
        }

        [HttpPost]
        public IActionResult ApplyPaylossLeave(LeaveApplyModel model)
        {
            /*if (ModelState.IsValid)
            {
                string empId = HttpContext.Session.GetString("acquiredID").ToString();
                int getEmpId = Convert.ToInt32(empId);
                bool leaveTaken = employeeRepo.EmployeePaylossLeaveApply(model, getEmpId);
                if (leaveTaken)
                {
                    return RedirectToAction("EmployeeLeave", "Employee");
                }
            }
            ModelState.Clear();
            return View();*/
            return RedirectToAction("ApplyPaylossLeave", "Employee");
        }






        public IActionResult PostEmployeeID()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostEmployeeID(EmployeeIdModel model)
        {
            HttpContext.Session.SetString("AdminUseEmpId", model.empID.ToString());
            return RedirectToAction("GetEmployeeDetails");
        }


        public IActionResult GetEmployeeDetails()
        {
            string empId = HttpContext.Session.GetString("AdminUseEmpId").ToString();
            int getEmpId = Convert.ToInt32(empId);
            EmployeeCompleteModel model = employeeRepo.FetchCompleteEmployeeDetails(getEmpId);
            return View(model);
        }

        [HttpPost]
        public IActionResult GetEmployeeDetails(EmployeeCompleteModel model)
        {
            if(ModelState.IsValid)
            {
                string empId = HttpContext.Session.GetString("AdminUseEmpId").ToString();
                int getEmpId = Convert.ToInt32(empId);
                bool approved = employeeRepo.UpdateEmployee(model, getEmpId);
                if (approved)
                {
                    return RedirectToAction("PostEmployeeID");
                }
            }
            return View(model);
        }








        public IActionResult CompleteProjectInformation()
        {
            IEnumerable<EmployeeProjectModel> model = employeeRepo.GetProjects();
            return View(model);
        }

        public IActionResult EditProject(int projectId)
        {
            EmployeeProjectModel model = employeeRepo.GetProjectDetailsByProjectId(projectId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProject(EmployeeProjectModel model)
        {
            bool approved = employeeRepo.UpdateProjectDetailsByProjectId(model);
            if (approved)
            {
                return RedirectToAction("AdminDashboard");
            }
            return View(model);
        }


        public IActionResult AddNewProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProject(EmployeeProjectModel model)
        {
            bool approved = employeeRepo.AddNewProject(model);
            if (approved)
            {
                return RedirectToAction("AdminDashboard");
            }
            return View();
        }









        public IActionResult CompleteRoleInformation()
        {
            IEnumerable<EmployeeRoleModel> model = employeeRepo.GetRoles();
            return View(model);
        }

        public IActionResult EditRole(int roleId)
        {
            EmployeeRoleModel model = employeeRepo.GetRoleDetailsByRoleId(roleId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditRole(EmployeeRoleModel model)
        {
            bool approved = employeeRepo.UpdateRoleDetailsByRoleId(model);
            if (approved)
            {
                return RedirectToAction("AdminDashboard");
            }
            return View(model);
        }


        public IActionResult AddNewRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewRole(EmployeeRoleModel model)
        {
            bool approved = employeeRepo.AddNewRole(model);
            if (approved)
            {
                return RedirectToAction("AdminDashboard");
            }
            return View();
        }






        public IActionResult CompleteEmployeeInformation()
        {
            IEnumerable<EmployeeCompleteModel> model = employeeRepo.GetEmployees();
            return View(model);
        }

        public IActionResult EditEmployee(int EmployeeId)
        {
            EmployeeCompleteModel model = employeeRepo.FetchCompleteEmployeeDetails(EmployeeId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditEmployee(EmployeeCompleteModel model)
        {
            int empId = model.EmployeeId;
            bool approved = employeeRepo.UpdateEmployee(model, empId);
            if (approved)
            {
                return RedirectToAction("AdminDashboard");
            }
            return View(model);
        }

        public IActionResult DeleteEmployee(int EmployeeId)
        {
            EmployeeCompleteModel model = employeeRepo.FetchCompleteEmployeeDetails(EmployeeId);
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteEmployee(EmployeeCompleteModel model)
        {
            int empId = model.EmployeeId;
            bool approved = employeeRepo.DeleteEmployee(empId);
            if (approved)
            {
                return RedirectToAction("AdminDashboard");
            }
            return View(model);
        }



        public IActionResult AddNewEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewEmployee(EmployeeCompleteModel model)
        {
            bool approved = employeeRepo.AddEmployee(model);
            if (approved)
            {
                return RedirectToAction("AdminDashboard");
            }
            return View();
        }


    }
}
