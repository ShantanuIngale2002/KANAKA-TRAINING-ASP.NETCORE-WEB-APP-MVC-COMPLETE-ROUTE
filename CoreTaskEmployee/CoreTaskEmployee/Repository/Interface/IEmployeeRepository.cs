using CoreTaskEmployee.Models;

namespace CoreTaskEmployee.Repository.Interface
{
    public interface IEmployeeRepository
    {
        public EmployeeDisplayModel GetEmployee(int empid);
        public bool AddEmployee(EmployeeCompleteModel model);
        public bool UpdateEmployee(EmployeeCompleteModel model);
        public bool DeleteEmployee(int empid);

        public bool LeavesForEmployee(EmployeeLeaveModel model);
    }
}
