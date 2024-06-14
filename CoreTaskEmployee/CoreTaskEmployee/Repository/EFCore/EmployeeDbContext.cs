using CoreTaskEmployee.Data;
using Microsoft.EntityFrameworkCore;
using CoreTaskEmployee.Models;

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
        public DbSet<EmployeeIdModel> EmployeeIdModel { get; set; }
        public DbSet<EmployeeDisplayModel> EmployeeDisplayModel { get; set; }

    }
}
