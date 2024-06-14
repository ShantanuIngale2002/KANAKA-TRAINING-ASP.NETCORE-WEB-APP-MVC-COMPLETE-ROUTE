using CoreTaskEmployee.Data;
using CoreTaskEmployee.Models;
using CoreTaskEmployee.Repository.Interface;
using EmployeeTaskCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace EmployeeTaskCore.Controllers
{
    public class ManagerController : Controller
    {

        IEmployeeRepository employeeRepo;
        public ManagerController(IEmployeeRepository employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManagerProjectInformation()
        {
            string empId = HttpContext.Session.GetString("acquiredID").ToString();
            int getEmpId = Convert.ToInt32(empId);
            List<EmployeeProjectModel> model = employeeRepo.GetEmployeeProjectDetails(getEmpId);
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
                return RedirectToAction("ManagerProjectInformation");
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
            string empId = HttpContext.Session.GetString("acquiredID").ToString();
            int getEmpId = Convert.ToInt32(empId);
            bool approved = employeeRepo.ManagerAddNewProject(getEmpId, model);
            if (approved)
            {
                return RedirectToAction("ManagerProjectInformation");
            }
            return View();
        }



        public IActionResult ProjectEmployees(int ProjectId)
        {
            IEnumerable<EmployeeDisplayModel> model = employeeRepo.GetEmployeesOnProjectId(ProjectId);
            return View(model);
        }


        public IActionResult RemoveEmployeeFromProject(int EmployeeId, string ProjectName)
        {
            int projectId = employeeRepo.GetProjectIdUsingProjectName(ProjectName);
            EmployeeCompleteModel model = employeeRepo.FetchCompleteEmployeeDetails(EmployeeId);
            model.EmployeeProject = projectId;
            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveEmployeeFromProject(EmployeeCompleteModel model)
        {
            bool isRemoved = employeeRepo.RemoveEmployeeFromProject(model);
            if(isRemoved)
            {
                return RedirectToAction("ProjectEmployees");
            }
            return View();
        }



        public IActionResult AssignNewEmployee(string ProjectName)
        {
            int projectId = employeeRepo.GetProjectIdUsingProjectName(ProjectName);

            IEnumerable<EmployeeDisplayModel> model = employeeRepo.GetEmployeesNotInProject(projectId);

            model.First().EmployeeProject = ProjectName;
            return View(model);
        }


        public IActionResult AssignEmployeeToProject(int EmployeeId, string ProjectName)
        {
            int projectId = employeeRepo.GetProjectIdUsingProjectName(ProjectName);
            bool assigned = employeeRepo.AssignEmployeeToProject(EmployeeId, projectId);
            if(assigned)
            {
                return RedirectToAction("ManagerProjectInformation");
            }
            return View();
        }
    }
}
