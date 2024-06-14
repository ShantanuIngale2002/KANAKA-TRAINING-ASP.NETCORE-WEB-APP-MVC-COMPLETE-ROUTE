using System.ComponentModel.DataAnnotations;

namespace CoreTaskEmployee.Models
{
    public class EmployeeCompleteModel
    {
        [Key]
        public int EmployeeId { get; set; }

        // Employee
        [Required]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        public string EmployeeDOB { get; set; } = string.Empty;
        
        [Required]
        public string EmployeeDOJ { get; set; } = string.Empty;

        [Required]
        public string EmployeeCity {  get; set; } = string.Empty;

        [Required]
        public string EmployeeState { get; set; } = string.Empty;

        [Required]
        public string EmployeeCountry { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Inappropriate Contact")]
        public string EmployeeContact { get; set; } = string.Empty;

        public string EmployeeBloodGroup {  get; set; } = string.Empty;

        // ProjectMap
        [Required]
        public int EmployeeProject {  get; set; }

        // RoleMap
        [Required]
        public int EmployeeRole {  get; set; }
        

    }
}
