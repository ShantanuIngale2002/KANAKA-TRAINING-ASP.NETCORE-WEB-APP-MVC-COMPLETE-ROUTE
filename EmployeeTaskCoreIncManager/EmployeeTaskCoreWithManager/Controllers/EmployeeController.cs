using CoreTaskEmployee.Models;
using CoreTaskEmployee.Repository.Interface;
using EmployeeTaskCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskCore.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployeeDashboard()
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
            string empId = HttpContext.Session.GetString("acquiredID").ToString();
            int getEmpId = Convert.ToInt32(empId);
            EmployeeDisplayModel model = employeeRepo.GetEmployee(getEmpId);
            return View(model);
        }



        public IActionResult ProjectInformation()
        {
            string empId = HttpContext.Session.GetString("acquiredID").ToString();
            int getEmpId = Convert.ToInt32(empId);
            EmployeeProjectModel model = employeeRepo.GetEmployeeProjectDetails(getEmpId);
            return View(model);
        }



        public IActionResult EmployeeLeave()
        {
            string empId = HttpContext.Session.GetString("acquiredID").ToString();
            int getEmpId = Convert.ToInt32(empId);
            EmployeeLeaveModel empLeaveData = employeeRepo.GetEmployeeLeaveData(getEmpId);
            return View(empLeaveData);
        }


        public IActionResult LeavesHistory()
        {
            string empId = HttpContext.Session.GetString("acquiredID").ToString();
            int getEmpId = Convert.ToInt32(empId);
            IEnumerable<EmployeeLeaveHistoryModel> empLeaveHistory = employeeRepo.GetEmployeeLeaveHistory(getEmpId);
            return View(empLeaveHistory);
        }

        public IActionResult EmployeePaidLeaveApply()
        {
            string empId = HttpContext.Session.GetString("acquiredID").ToString();
            int getEmpId = Convert.ToInt32(empId);
            bool isApproved = employeeRepo.LeavesForEmployee(getEmpId);
            if(isApproved)
            {
                return RedirectToAction("ApplyPaidLeave");
            }
            ViewBag.PaidLeaves = "No Paid Leaves Remaining";
            return View();
        }


        public IActionResult ApplyPaidLeave()
        {
            LeaveApplyModel model = new LeaveApplyModel();
            string empId = HttpContext.Session.GetString("acquiredID").ToString();
            int getEmpId = Convert.ToInt32(empId);

            string managerRoleName = employeeRepo.GetEmployeeManagerName(getEmpId);

            model.AppliedTo = managerRoleName;

            return View(model);
        }

        [HttpPost]
        public IActionResult ApplyPaidLeave(LeaveApplyModel model)
        {
            if (ModelState.IsValid)
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
            return View();
        }




        public IActionResult ApplyPaylossLeave()
        {
            LeaveApplyModel model = new LeaveApplyModel();
            string empId = HttpContext.Session.GetString("acquiredID").ToString();
            int getEmpId = Convert.ToInt32(empId);

            string managerRoleName = employeeRepo.GetEmployeeManagerName(getEmpId);

            model.AppliedTo = managerRoleName;

            return View(model);
        }

        [HttpPost]
        public IActionResult ApplyPaylossLeave(LeaveApplyModel model)
        {
            if (ModelState.IsValid)
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
            return View();
        }



        public IActionResult EmployeeLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

    }
}
