using CoreTaskEmployee.Data;
using CoreTaskEmployee.Models;
using EmployeeTaskCore.Models;
using Microsoft.CodeAnalysis;
using Microsoft.Identity.Client;
using System.Collections;

namespace CoreTaskEmployee.Repository.Interface
{
    public interface IEmployeeRepository
    {
        public int FetchEmployeeId(EmployeeLoginModel login);
        public EmployeeDisplayModel GetEmployee(int empid);
        public EmployeeCredentialModel GetEmployeeCredential(int empid);
        public bool UpdateEmployeeCredential(EmployeeCredentialModel model);
        public bool DeleteEmployee(int empid);
        public bool LeavesForEmployee(int empid);
        public List<EmployeeProjectModel> GetEmployeeProjectDetails(int empID);
        public EmployeeLeaveModel GetEmployeeLeaveData(int empId);
        public bool EmployeePaidLeaveApply(LeaveApplyModel model, int empID);
        public bool EmployeePaylossLeaveApply(LeaveApplyModel model, int empID);
        public IEnumerable<EmployeeLeaveHistoryModel> GetEmployeeLeaveHistory(int empID);

        public string GetEmployeeManagerName(int empId);
        public bool GetAdmin(EmployeeLoginModel model);
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

        public IEnumerable<EmployeeDisplayModel> GetEmployeesOnProjectId(int projectId);
        public int GetProjectIdUsingProjectName(string projectName);
        public bool RemoveEmployeeFromProject(EmployeeCompleteModel model);
        public bool ManagerAddNewProject(int managerId, EmployeeProjectModel model);
        public IEnumerable<EmployeeDisplayModel> GetEmployeesNotInProject(int projectId);
        public bool AssignEmployeeToProject(int EmployeeId, int projectId);

    }
}
