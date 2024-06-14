using CoreTaskEmployee.Models;
using EmployeeTaskCore.Models;
using System.Collections;

namespace CoreTaskEmployee.Repository.Interface
{
    public interface IEmployeeRepository
    {
        public EmployeeDisplayModel GetEmployee(int empid);
        public bool DeleteEmployee(int empid);
        public bool LeavesForEmployee(int empid);
        public EmployeeProjectModel GetEmployeeProjectDetails(int empID);
        public EmployeeLeaveModel GetEmployeeLeaveData(int empId);
        public bool EmployeePaidLeaveApply(LeaveApplyModel model, int empID);
        public bool EmployeePaylossLeaveApply(LeaveApplyModel model, int empID);
        public IEnumerable<EmployeeLeaveHistoryModel> GetEmployeeLeaveHistory(int empID);

        public string GetEmployeeManagerName(int empId);
        public bool GetAdmin(AdminLoginModel model);
        public EmployeeCompleteModel FetchCompleteEmployeeDetails(int emp_id);
        public bool AddEmployee(EmployeeCompleteModel model);
        public bool UpdateEmployee(EmployeeCompleteModel model, int empId);

        public IEnumerable<EmployeeProjectModel> GetProjects();
        public EmployeeProjectModel GetProjectDetailsByProjectId(int projectId);
        public bool UpdateProjectDetailsByProjectId(EmployeeProjectModel model);
        public bool AddNewProject(EmployeeProjectModel model);

        public IEnumerable<EmployeeRoleModel> GetRoles();
        public EmployeeRoleModel GetRoleDetailsByRoleId(int projectId);
        public bool UpdateRoleDetailsByRoleId(EmployeeRoleModel model);
        public bool AddNewRole(EmployeeRoleModel model);

        public IEnumerable<EmployeeCompleteModel> GetEmployees();
        //public EmployeeCompleteModel GetEmployeeDetailsByEmployeeId(int projectId);
        //public bool UpdateEmployeeDetailsByEmployeeId(EmployeeCompleteModel model);
       // public bool AddNewEmployee(EmployeeCompleteModel model);

    }
}
