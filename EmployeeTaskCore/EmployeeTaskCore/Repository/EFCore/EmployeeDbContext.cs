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

        public DbSet<Credential> Credential { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeProjectMap> EmployeeProjectMap { get; set; }
        public DbSet<EmployeeRoleMap> EmployeeRoleMap { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<Credential> Admin { get; set; }
        public DbSet<LeaveHistory> LeaveHistory { get; set; }
        public DbSet<EmployeeTaskCore.Models.EmployeeLoginModel> EmployeeLoginModel { get; set; } = default!;
        public DbSet<EmployeeTaskCore.Models.EmployeeProjectModel> EmployeeProjectModel { get; set; } = default!;
        public DbSet<CoreTaskEmployee.Models.EmployeeDisplayModel> EmployeeDisplayModel { get; set; } = default!;
        public DbSet<CoreTaskEmployee.Models.EmployeeCompleteModel> EmployeeCompleteModel { get; set; } = default!;
        public DbSet<EmployeeTaskCore.Models.EmployeeCredentialModel> EmployeeCredentialModel { get; set; } = default!;

    }
}
