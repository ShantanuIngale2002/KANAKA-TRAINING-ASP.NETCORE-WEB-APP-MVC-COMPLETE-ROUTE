using CoreTaskEmployee.Data;
using Microsoft.EntityFrameworkCore;
using CoreTaskEmployee.Models;
using System.Data;
using EmployeeTaskCore.Data;
using EmployeeTaskCore.Models;

namespace CoreTaskEmployee.Repository.EFCore
{
    public class EmployeeDbContext : DbContext
    {

        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<EmployeeProjectMap> EmployeeProjectMap { get; set; }
        public DbSet<EmployeeRoleMap> EmployeeRoleMap { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<LeaveHistory> LeaveHistory { get; set; }
        public DbSet<CoreTaskEmployee.Models.EmployeeLeaveModel> EmployeeLeaveModel { get; set; } = default!;
        public DbSet<EmployeeTaskCore.Models.EmployeeLeaveHistoryModel> EmployeeLeaveHistoryModel { get; set; } = default!;
        public DbSet<EmployeeTaskCore.Models.LeaveApplyModel> LeaveApplyModel { get; set; } = default!;
        public DbSet<CoreTaskEmployee.Models.EmployeeIdModel> EmployeeIdModel { get; set; } = default!;
        public DbSet<CoreTaskEmployee.Models.EmployeeCompleteModel> EmployeeCompleteModel { get; set; } = default!;
        public DbSet<EmployeeTaskCore.Models.EmployeeProjectModel> EmployeeProjectModel { get; set; } = default!;
        public DbSet<EmployeeTaskCore.Models.EmployeeRoleModel> EmployeeRoleModel { get; set; } = default!;

    }
}
