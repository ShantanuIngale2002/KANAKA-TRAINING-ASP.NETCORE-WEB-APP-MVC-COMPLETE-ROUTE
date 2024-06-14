using CoreTaskEmployee.Models;
using CoreTaskEmployee.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreTaskEmployee.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        IEmployeeRepository employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        [HttpGet("Display")]
        public EmployeeDisplayModel GetEmployeeDetails()
        {
            return employeeRepo.GetEmployee(5);
        }

        [HttpPost("Add")]
        public bool AddEmployeeDetails(EmployeeCompleteModel model)
        {
            return employeeRepo.AddEmployee(model);
        }

        [HttpPost("Update")]
        public bool UpdateEmployeeDetails(EmployeeCompleteModel model)
        {
            return employeeRepo.UpdateEmployee(model);
        }

        [HttpDelete("Delete")]
        public bool DeleteEmployeeDetails()
        {
            return employeeRepo.DeleteEmployee(5);
        }

        [HttpPost("Leave")]
        public bool LeaveForEmployee(EmployeeLeaveModel model)
        {
            return employeeRepo.LeavesForEmployee(model);
        }
    }
}
