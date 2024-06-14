using CoreTaskEmployee.Data;
using CoreTaskEmployee.Models;
using CoreTaskEmployee.Repository.EFCore;
using CoreTaskEmployee.Repository.Interface;

namespace CoreTaskEmployee.Repository
{
    public class EmployeeService : IEmployeeRepository
    {

        EmployeeDbContext dbcontext;
        public EmployeeService(EmployeeDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        public bool AddEmployee(EmployeeCompleteModel model)
        {
            Employee emp = new()
            {
                EmployeeId = model.EmployeeId,
                EmployeeName = model.EmployeeName,
                EmployeeContact = model.EmployeeContact,
                EmployeeBloodGroup = model.EmployeeBloodGroup,
                EmployeeCity = model.EmployeeCity,
                EmployeeCountry = model.EmployeeCountry,
                EmployeeState = model.EmployeeState,
                EmployeeDOB = model.EmployeeDOB,
                EmployeeDOJ = model.EmployeeDOJ,
            };

            EmployeeProjectMap empProject = new()
            {
                EmployeeId = model.EmployeeId,
                ProjectId = model.EmployeeProject,
            };

            EmployeeRoleMap empRole = new()
            {
                EmployeeId = model.EmployeeId,
                RoleId = model.EmployeeRole,
            };

            Leave empLeave = new()
            {
                EmployeeId = model.EmployeeId,
                PaidLeavesRemaining = 24,
                PaidLeavesTaken = 0,
                PaylossLeavesTaken = 0,
            };

            dbcontext.Employee.Add(emp);
            dbcontext.EmployeeProjectMap.Add(empProject);
            dbcontext.EmployeeRoleMap.Add(empRole);
            dbcontext.Leave.Add(empLeave);

            int row = dbcontext.SaveChanges();

            if (row > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteEmployee(int empId)
        {
            // RoleMap and ProjectMap and Leave have emp_id as foreign key from Employee hence we cannot find, Remove and saveChanges as combined ie. it raises errpr. Hence did separately
            // First of al delete Foregin relational table ie. Project, Role and Leave
            var EmpProject = dbcontext.EmployeeProjectMap.Find(empId);
            var EmpRole = dbcontext.EmployeeRoleMap.Find(empId);
            var EmpLeave = dbcontext.Leave.Find(empId);
            if (EmpProject != null && EmpRole != null)
            {
                dbcontext.EmployeeProjectMap.Remove(EmpProject);
                dbcontext.EmployeeRoleMap.Remove(EmpRole);
                dbcontext.Leave.Remove(EmpLeave);
                dbcontext.SaveChanges();
            }
            // Lastly delete the Employee
            var EmpInfo = dbcontext.Employee.Find(empId);
            if (EmpInfo != null)
            {
                dbcontext.Employee.Remove(EmpInfo);
                dbcontext.SaveChanges();
            }

            return true;
        }

        public EmployeeDisplayModel GetEmployee(int emp_id)
        {
            var EmpInfo = dbcontext.Employee.Find(emp_id);
            var EmpProject1 = dbcontext.EmployeeProjectMap.Find(emp_id);
            var EmpProject2 = dbcontext.Project.Find((int)EmpProject1.ProjectId);
            var EmpRole1 = dbcontext.EmployeeRoleMap.Find(emp_id);
            var EmpRole2 = dbcontext.Role.Find((int)EmpRole1.RoleId);
            var EmpLeave = dbcontext.Leave.Find(emp_id);

            EmployeeDisplayModel newEmp = new()
            {
                EmployeeName = EmpInfo.EmployeeName.ToString(),
                EmployeeDOB = EmpInfo.EmployeeDOB.ToString(),
                EmployeeCity = EmpInfo.EmployeeCity.ToString(),
                EmployeeState = EmpInfo.EmployeeState.ToString(),
                EmployeeCountry = EmpInfo.EmployeeCountry.ToString(),
                EmployeeContact = EmpInfo.EmployeeContact.ToString(),
                EmployeeProject = EmpProject2.ProjectName.ToString(),
                EmployeeRole = EmpRole2.RoleName.ToString(),
                EmployeePaidLeavesRemaining = EmpLeave.PaidLeavesRemaining,
                EmployeePaidLeavesTaken = EmpLeave.PaidLeavesTaken,
                EmployeePaylossLeavesTaken = EmpLeave.PaylossLeavesTaken,
            };



            return newEmp;
        }

        public bool UpdateEmployee(EmployeeCompleteModel model)
        {

            var EmpInfo = dbcontext.Employee.Find(model.EmployeeId);
            var EmpProject = dbcontext.EmployeeProjectMap.Find(model.EmployeeId);
            var EmpRole = dbcontext.EmployeeRoleMap.Find(model.EmployeeId);
            //var EmpLeave = dbcontext.Leave.Find(model.EmployeeId);

            //if (EmpInfo != null && EmpProject != null && EmpRole != null && EmpLeave != null)
            if (EmpInfo != null && EmpProject != null && EmpRole != null)
            {
                EmpInfo.EmployeeName = model.EmployeeName;
                EmpInfo.EmployeeDOB = model.EmployeeDOB;
                EmpInfo.EmployeeDOJ = model.EmployeeDOJ;
                EmpInfo.EmployeeContact = model.EmployeeContact;
                EmpInfo.EmployeeCity = model.EmployeeCity;
                EmpInfo.EmployeeState = model.EmployeeState;
                EmpInfo.EmployeeCountry = model.EmployeeCountry;
                EmpInfo.EmployeeBloodGroup = model.EmployeeBloodGroup;

                EmpProject.ProjectId = model.EmployeeProject;
                EmpRole.RoleId = model.EmployeeRole;

                /*EmpLeave.PaidLeavesTaken = model.EmployeePaidLeavesTaken;
                EmpLeave.PaidLeavesRemaining = model.EmployeePaidLeavesRemain;
                EmpLeave.PaylossLeavesTaken = model.EmployeePaylossLeavesTaken;*/

                int row = dbcontext.SaveChanges();

                if(row>0)
                {
                    return true;
                }

            }

            return false;
        }

        public bool LeavesForEmployee(EmployeeLeaveModel model)
        {
            var EmpLeave = dbcontext.Leave.Find(model.EmployeeId);
            if (EmpLeave != null)
            {
                int leaveRemains = (int)EmpLeave.PaidLeavesRemaining;
                if(leaveRemains < 1 || (int)model.EmployeePaidLeavesTaken > leaveRemains)
                {
                    return false;
                }
                leaveRemains = leaveRemains - (int)model.EmployeePaidLeavesTaken;

                EmpLeave.PaidLeavesTaken = (int)EmpLeave.PaidLeavesTaken + model.EmployeePaidLeavesTaken;
                EmpLeave.PaidLeavesRemaining = leaveRemains;
                EmpLeave.PaylossLeavesTaken = model.EmployeePaylossLeavesTaken;

                int row = dbcontext.SaveChanges();
                if (row > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
