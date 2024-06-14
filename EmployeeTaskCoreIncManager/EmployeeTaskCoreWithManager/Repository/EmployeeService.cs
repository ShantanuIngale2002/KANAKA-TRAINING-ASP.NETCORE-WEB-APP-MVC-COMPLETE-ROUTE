using CoreTaskEmployee.Data;
using CoreTaskEmployee.Models;
using CoreTaskEmployee.Repository.EFCore;
using CoreTaskEmployee.Repository.Interface;
using EmployeeTaskCore.Data;
using EmployeeTaskCore.Models;
using System.Collections;
using System.Data;

namespace CoreTaskEmployee.Repository
{
    public class EmployeeService : IEmployeeRepository
    {

        EmployeeDbContext dbcontext;
        public EmployeeService(EmployeeDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        

        public EmployeeDisplayModel GetEmployee(int emp_id)
        {

            var EmpInfo = (from empInfo in dbcontext.Employee
                           join projectMap in dbcontext.EmployeeProjectMap on empInfo.EmployeeId equals projectMap.EmployeeId
                           join project in dbcontext.Project on projectMap.ProjectId equals project.ProjectId
                           join roleMap in dbcontext.EmployeeRoleMap on empInfo.EmployeeId equals roleMap.EmployeeId
                           join role in dbcontext.Role on roleMap.RoleId equals role.RoleId
                           join leave in dbcontext.Leave on empInfo.EmployeeId equals leave.EmployeeId
                           where empInfo.EmployeeId == emp_id
                           select new
                           {
                               EmployeeName = empInfo.EmployeeName,
                               EmployeeDOB = empInfo.EmployeeDOB,
                               EmployeeDOJ = empInfo.EmployeeDOJ,
                               EmployeeCity= empInfo.EmployeeCity,
                               EmployeeState = empInfo.EmployeeState,
                               EmployeeCountry = empInfo.EmployeeCountry,
                               EmployeeBloodGroup = empInfo.EmployeeBloodGroup,
                               EmployeeContact = empInfo.EmployeeContact,
                               EmployeeProjectName = project.ProjectName,
                               EmployeeRoleName = role.RoleName,
                               EmployeePaidLeavesTaken = leave.PaidLeavesTaken,
                               EmployeePaidLeavesRemain = leave.PaidLeavesRemaining,
                               EmployeePaylossLeavesTaken = leave.PaylossLeavesTaken,
                           }
                           ).FirstOrDefault();
            if(EmpInfo == null)
            {
                return new EmployeeDisplayModel();
            }

            EmployeeDisplayModel emp = new()
            {
                EmployeeName = EmpInfo.EmployeeName,
                EmployeeDOB = EmpInfo.EmployeeDOB,
                EmployeeDOJ = EmpInfo.EmployeeDOJ,
                EmployeeCity = EmpInfo.EmployeeCity,
                EmployeeState = EmpInfo.EmployeeState,
                EmployeeCountry = EmpInfo.EmployeeCountry,
                EmployeeBloodGroup = EmpInfo.EmployeeBloodGroup,
                EmployeeContact = EmpInfo.EmployeeContact,
                EmployeeProject = EmpInfo.EmployeeProjectName,
                EmployeeRole = EmpInfo.EmployeeRoleName,
                EmployeePaidLeavesTaken = EmpInfo.EmployeePaidLeavesTaken,
                EmployeePaidLeavesRemaining = EmpInfo.EmployeePaidLeavesRemain,
                EmployeePaylossLeavesTaken = EmpInfo.EmployeePaylossLeavesTaken,
            };

            return emp;
        }




        public string GetEmployeeManagerName(int empId)
        {
            string resultingManager = "";
            var empRoleFind = (from rolemap in dbcontext.EmployeeRoleMap
                               join role in dbcontext.Role on rolemap.RoleId equals role.RoleId
                               where rolemap.EmployeeId == empId
                               select role.RoleName
                              ).FirstOrDefault();

            string rolename = empRoleFind.ToString();

            if(rolename != "Admin" && rolename != "Manager")
            {
                // find manager : managerName
                resultingManager += "Manager. ";

                var empProjectIdFind = (from projectmap in dbcontext.EmployeeProjectMap
                                        where projectmap.EmployeeId == empId
                                        select projectmap.ProjectId
                                       ).FirstOrDefault();

                int empProjectId = Convert.ToInt32(empProjectIdFind);

                var managerNameFind = (from employee in dbcontext.Employee
                                       join rolemap in dbcontext.EmployeeRoleMap on employee.EmployeeId equals rolemap.EmployeeId
                                       join role in dbcontext.Role on rolemap.RoleId equals role.RoleId
                                       join projectmap in dbcontext.EmployeeProjectMap on employee.EmployeeId equals projectmap.EmployeeId
                                       join project in dbcontext.Project on projectmap.ProjectId equals project.ProjectId
                                       where project.ProjectId == empProjectId && role.RoleName == "Manager"
                                       select employee.EmployeeName
                                      ).FirstOrDefault();

                string managerName = managerNameFind.ToString();

                resultingManager += managerName;

                return resultingManager;

                
            }
            else
            {
                // find admin : adminName
                resultingManager += "Admin. ";

                var adminNameFind = (from employee in dbcontext.Employee
                                     join rolemap in dbcontext.EmployeeRoleMap on employee.EmployeeId equals rolemap.EmployeeId
                                     join role in dbcontext.Role on rolemap.RoleId equals role.RoleId
                                     where role.RoleName == "Admin"
                                     select employee.EmployeeName
                                    ).FirstOrDefault();

                string adminName = adminNameFind.ToString();

                resultingManager += adminName;

                return resultingManager;
            }
        }



        public bool LeavesForEmployee(int empID)
        {
            var EmpLeave = dbcontext.Leave.Where(model=>model.EmployeeId == empID).FirstOrDefault();
            if (EmpLeave != null)
            {
                int leaveRemains = (int)EmpLeave.PaidLeavesRemaining;
                if (leaveRemains > 0)
                {
                    return true;
                }
            }
            return false;
        }


        public EmployeeLeaveModel GetEmployeeLeaveData(int empId)
        {
            var data = dbcontext.Leave.Where(model => model.EmployeeId == empId).FirstOrDefault();
            EmployeeLeaveModel empLeave = new()
            {
                EmployeePaidLeavesTaken = data.PaidLeavesTaken,
                EmployeePaidLeavesRemain = data.PaidLeavesRemaining,
                EmployeePaylossLeavesTaken = data.PaylossLeavesTaken,
            };
            return empLeave;
        }


        public IEnumerable<EmployeeLeaveHistoryModel> GetEmployeeLeaveHistory(int empId)
        {
            IEnumerable<EmployeeLeaveHistoryModel> EmpLeaveHistory = (from leaveH in dbcontext.LeaveHistory
                                   where leaveH.EmployeeId == empId
                                   select new EmployeeLeaveHistoryModel
                                   {
                                       StartDate = leaveH.StartDate,
                                       EndDate = leaveH.EndDate,
                                       Reason = leaveH.Reason,
                                       Type = leaveH.Type,
                                   }).ToList();
            return EmpLeaveHistory;
        }

        public bool EmployeePaidLeaveApply(LeaveApplyModel model, int empID)
        {
            EmployeeLeaveModel empLeaveData = this.GetEmployeeLeaveData(empID);

            int totalDays = (model.EndDate - model.StartDate).Days + 1;
            string startDateFormatted = model.StartDate.ToString("dd/MM/yyyy");
            string endDateFormatted = model.EndDate.ToString("dd/MM/yyyy");

            if(totalDays > (int)empLeaveData.EmployeePaidLeavesRemain)
            {
                return false;
            }

            var empCurrentLeaves = dbcontext.Leave.SingleOrDefault(m => m.EmployeeId == empID);
            if(empCurrentLeaves != null)
            {
                empCurrentLeaves.PaidLeavesTaken += totalDays;
                empCurrentLeaves.PaidLeavesRemaining -= totalDays;

                LeaveHistory empLeaveHistory = new()
                {
                    EmployeeId = empID,
                    StartDate = startDateFormatted,
                    EndDate = endDateFormatted,
                    Reason = model.Reason,
                    Type = "Paid",
                };
                dbcontext.LeaveHistory.Add(empLeaveHistory);

                dbcontext.SaveChanges();
            }

            return true;
        }

        public bool EmployeePaylossLeaveApply(LeaveApplyModel model, int empID)
        {
            EmployeeLeaveModel empLeaveData = this.GetEmployeeLeaveData(empID);

            int totalDays = (model.EndDate - model.StartDate).Days + 1;
            string startDateFormatted = model.StartDate.ToString("dd/MM/yyyy");
            string endDateFormatted = model.EndDate.ToString("dd/MM/yyyy");

            var empCurrentLeaves = dbcontext.Leave.SingleOrDefault(m => m.EmployeeId == empID);
            if (empCurrentLeaves != null)
            {
                empCurrentLeaves.PaylossLeavesTaken += totalDays;

                LeaveHistory empLeaveHistory = new()
                {
                    EmployeeId = empID,
                    StartDate = startDateFormatted,
                    EndDate = endDateFormatted,
                    Reason = model.Reason,
                    Type = "Payloss",
                };
                dbcontext.LeaveHistory.Add(empLeaveHistory);

                dbcontext.SaveChanges();
            }

            return true;
        }


        public bool GetAdmin(AdminLoginModel model)
        {
            var data = (from admin in dbcontext.Admin 
                       where admin.Username == model.Username && admin.Password == model.Password
                       select admin).FirstOrDefault();
            if (data != null)
            {
                return true;
            }
            return false;
        }


        public EmployeeProjectModel GetEmployeeProjectDetails(int empID)
        {
            var projectInfo = (from projectmap in dbcontext.EmployeeProjectMap
                           join project in dbcontext.Project on projectmap.ProjectId equals project.ProjectId
                           where projectmap.EmployeeId == empID
                           select project).FirstOrDefault();
            EmployeeProjectModel EmpProject = new()
            {
                ProjectId = projectInfo.ProjectId,
                ProjectName = projectInfo.ProjectName,
                StartDate = projectInfo.StartDate,
                DueDate = projectInfo.EndDate,
                Resources = projectInfo.ResourcesCount,
            };
            return EmpProject;
        }








        // Admin Requirements

        public EmployeeCompleteModel FetchCompleteEmployeeDetails(int emp_id)
        {

            var EmpInfo = (from empInfo in dbcontext.Employee
                           join projectMap in dbcontext.EmployeeProjectMap on empInfo.EmployeeId equals projectMap.EmployeeId
                           join project in dbcontext.Project on projectMap.ProjectId equals project.ProjectId
                           join roleMap in dbcontext.EmployeeRoleMap on empInfo.EmployeeId equals roleMap.EmployeeId
                           join role in dbcontext.Role on roleMap.RoleId equals role.RoleId
                           join leave in dbcontext.Leave on empInfo.EmployeeId equals leave.EmployeeId
                           where empInfo.EmployeeId == emp_id
                           select new
                           {
                               EmployeeName = empInfo.EmployeeName,
                               EmployeeDOB = empInfo.EmployeeDOB,
                               EmployeeDOJ = empInfo.EmployeeDOJ,
                               EmployeeCity = empInfo.EmployeeCity,
                               EmployeeState = empInfo.EmployeeState,
                               EmployeeCountry = empInfo.EmployeeCountry,
                               EmployeeBloodGroup = empInfo.EmployeeBloodGroup,
                               EmployeeContact = empInfo.EmployeeContact,
                               EmployeeProject = project.ProjectId,
                               EmployeeRole = role.RoleId,
                               EmployeePaidLeavesTaken = leave.PaidLeavesTaken,
                               EmployeePaidLeavesRemain = leave.PaidLeavesRemaining,
                               EmployeePaylossLeavesTaken = leave.PaylossLeavesTaken,
                           }
                           ).FirstOrDefault();
            if (EmpInfo == null)
            {
                return new EmployeeCompleteModel();
            }

            EmployeeCompleteModel emp = new()
            {
                EmployeeName = EmpInfo.EmployeeName,
                EmployeeDOB = EmpInfo.EmployeeDOB,
                EmployeeDOJ = EmpInfo.EmployeeDOJ,
                EmployeeCity = EmpInfo.EmployeeCity,
                EmployeeState = EmpInfo.EmployeeState,
                EmployeeCountry = EmpInfo.EmployeeCountry,
                EmployeeBloodGroup = EmpInfo.EmployeeBloodGroup,
                EmployeeContact = EmpInfo.EmployeeContact,
                EmployeeProject = EmpInfo.EmployeeProject,
                EmployeeRole = EmpInfo.EmployeeRole,
                EmployeePaidLeavesTaken = EmpInfo.EmployeePaidLeavesTaken,
                EmployeePaidLeavesRemaining = EmpInfo.EmployeePaidLeavesRemain,
                EmployeePaylossLeavesTaken = EmpInfo.EmployeePaylossLeavesTaken,
            };

            return emp;
        }




        public bool UpdateEmployee(EmployeeCompleteModel model, int empId)
        {

            var EmpInfo = dbcontext.Employee.Where(m=>m.EmployeeId==empId).FirstOrDefault();
            var EmpProject = dbcontext.EmployeeProjectMap.Where(m => m.EmployeeId == empId).FirstOrDefault();
            var EmpRole = dbcontext.EmployeeRoleMap.Where(m => m.EmployeeId == empId).FirstOrDefault();
            var EmpLeave = dbcontext.Leave.Where(m => m.EmployeeId == empId).FirstOrDefault();

            if (EmpInfo != null && EmpProject != null && EmpRole != null && EmpLeave != null)
            //if (EmpInfo != null && EmpProject != null && EmpRole != null)
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

                EmpLeave.PaidLeavesTaken = model.EmployeePaidLeavesTaken;
                EmpLeave.PaidLeavesRemaining = model.EmployeePaidLeavesRemaining;
                EmpLeave.PaylossLeavesTaken = model.EmployeePaylossLeavesTaken;

                int row = dbcontext.SaveChanges();

                if (row > 0)
                {
                    return true;
                }
            }

            return false;
        }










        public IEnumerable<EmployeeProjectModel> GetProjects()
        {
            IEnumerable<EmployeeProjectModel> projectsData = (from project in dbcontext.Project
                                                              select new EmployeeProjectModel
                                                              {
                                                                  ProjectId = project.ProjectId,
                                                                  ProjectName = project.ProjectName,
                                                                  StartDate = project.StartDate,
                                                                  DueDate = project.EndDate,
                                                                  Resources = project.ResourcesCount,
                                                              }).ToList();

            return projectsData;
        }

        public EmployeeProjectModel GetProjectDetailsByProjectId(int projectId)
        {
            var projectInfo = (from project in dbcontext.Project
                               where project.ProjectId == projectId
                               select project).FirstOrDefault();

            EmployeeProjectModel EmpProject = new()
            {
                ProjectId = projectId,
                ProjectName = projectInfo.ProjectName,
                StartDate = projectInfo.StartDate,
                DueDate = projectInfo.EndDate,
                Resources = projectInfo.ResourcesCount,
            };
            return EmpProject;
        }

        public bool UpdateProjectDetailsByProjectId(EmployeeProjectModel model)
        {
            var project = dbcontext.Project.Where(m=>m.ProjectId == model.ProjectId).FirstOrDefault();
            
            project.ProjectName = model.ProjectName;
            project.StartDate = model.StartDate;
            project.EndDate = model.DueDate;
            project.ResourcesCount = model.Resources;

            int row = dbcontext.SaveChanges();

            if (row > 0)
            {
                return true;
            }
            return false;
        }

        public bool AddNewProject(EmployeeProjectModel model)
        {
            Project newProject = new()
            {
                ProjectId = model.ProjectId,
                ProjectName = model.ProjectName,
                StartDate = model.StartDate,
                EndDate = model.DueDate,
                ResourcesCount = model.Resources,
            };

            dbcontext.Project.Add(newProject);
            int row = dbcontext.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            return false;
        }









        public IEnumerable<EmployeeRoleModel> GetRoles()
        {
            IEnumerable<EmployeeRoleModel> rolesData = (from role in dbcontext.Role
                                                              select new EmployeeRoleModel
                                                              {
                                                                  RoleId = role.RoleId,
                                                                  RoleName = role.RoleName,
                                                              }).ToList();

            return rolesData;
        }

        public EmployeeRoleModel GetRoleDetailsByRoleId(int roleId)
        {
            var roleInfo = (from role in dbcontext.Role
                               where role.RoleId == roleId
                               select role).FirstOrDefault();

            EmployeeRoleModel EmpRole = new()
            {
                RoleId = roleInfo.RoleId,
                RoleName = roleInfo.RoleName,
            };
            return EmpRole;
        }

        public bool UpdateRoleDetailsByRoleId(EmployeeRoleModel model)
        {
            var role = dbcontext.Role.Where(m => m.RoleId == model.RoleId).FirstOrDefault();

            role.RoleName = model.RoleName;

            int row = dbcontext.SaveChanges();

            if (row > 0)
            {
                return true;
            }
            return false;
        }

        public bool AddNewRole(EmployeeRoleModel model)
        {
            Role newRole = new()
            {
                RoleId = model.RoleId,
                RoleName = model.RoleName,
            };

            dbcontext.Role.Add(newRole);
            int row = dbcontext.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            return false;
        }









        public IEnumerable<EmployeeCompleteModel> GetEmployees()
        {
            IEnumerable<EmployeeCompleteModel> allEmployees = (from empInfo in dbcontext.Employee
                                                               join projectMap in dbcontext.EmployeeProjectMap on empInfo.EmployeeId equals projectMap.EmployeeId
                                                               join project in dbcontext.Project on projectMap.ProjectId equals project.ProjectId
                                                               join roleMap in dbcontext.EmployeeRoleMap on empInfo.EmployeeId equals roleMap.EmployeeId
                                                               join role in dbcontext.Role on roleMap.RoleId equals role.RoleId
                                                               join leave in dbcontext.Leave on empInfo.EmployeeId equals leave.EmployeeId
                                                               select new EmployeeCompleteModel
                                                               {
                                                                   EmployeeId = empInfo.EmployeeId,
                                                                   EmployeeName = empInfo.EmployeeName,
                                                                   EmployeeDOB = empInfo.EmployeeDOB,
                                                                   EmployeeDOJ = empInfo.EmployeeDOJ,
                                                                   EmployeeCity = empInfo.EmployeeCity,
                                                                   EmployeeState = empInfo.EmployeeState,
                                                                   EmployeeCountry = empInfo.EmployeeCountry,
                                                                   EmployeeBloodGroup = empInfo.EmployeeBloodGroup,
                                                                   EmployeeContact = empInfo.EmployeeContact,
                                                                   EmployeeProject = project.ProjectId,
                                                                   EmployeeRole = role.RoleId,
                                                                   EmployeePaidLeavesTaken = leave.PaidLeavesTaken,
                                                                   EmployeePaidLeavesRemaining = leave.PaidLeavesRemaining,
                                                                   EmployeePaylossLeavesTaken = leave.PaylossLeavesTaken,
                                                               }).ToList();
            return allEmployees;
        }

        public bool DeleteEmployee(int empId)
        {
            // RoleMap and ProjectMap and Leave have emp_id as foreign key from Employee hence we cannot find, Remove and saveChanges as combined ie. it raises errpr. Hence did separately
            // First of al delete Foregin relational table ie. Project, Role and Leave
            var EmpProject = dbcontext.EmployeeProjectMap.Where(m => m.EmployeeId == empId).FirstOrDefault();
            var EmpRole = dbcontext.EmployeeRoleMap.Where(m => m.EmployeeId == empId).FirstOrDefault();
            var EmpLeave = dbcontext.Leave.Where(m => m.EmployeeId == empId).FirstOrDefault();
            if (EmpProject != null && EmpRole != null & EmpLeave != null)
            {
                dbcontext.EmployeeProjectMap.Remove(EmpProject);
                dbcontext.EmployeeRoleMap.Remove(EmpRole);
                dbcontext.Leave.Remove(EmpLeave);
                dbcontext.SaveChanges();
            }
            // Lastly delete the Employee
            var EmpInfo = dbcontext.Employee.Where(m => m.EmployeeId == empId).FirstOrDefault();
            if (EmpInfo != null)
            {
                dbcontext.Employee.Remove(EmpInfo);
                dbcontext.SaveChanges();
            }

            return true;
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

        







    }
}
