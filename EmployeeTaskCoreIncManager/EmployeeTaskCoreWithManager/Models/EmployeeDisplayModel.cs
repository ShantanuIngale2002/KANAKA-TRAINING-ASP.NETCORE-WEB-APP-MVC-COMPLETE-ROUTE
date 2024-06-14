using System.ComponentModel.DataAnnotations;

namespace CoreTaskEmployee.Models
{
    public class EmployeeDisplayModel
    {
        [Key]
        public int EmployeeId { get; set; }
        // Employee Table
        [Required]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        public string EmployeeDOB { get; set; } = string.Empty;

        [Required]
        public string EmployeeDOJ { get; set; } = string.Empty;

        [Required]
        public string EmployeeCity { get; set; } = string.Empty;

        [Required]
        public string EmployeeState { get; set; } = string.Empty;

        [Required]
        public string EmployeeCountry { get; set; } = string.Empty;

        [Required]
        public string EmployeeContact { get; set; } = string.Empty;

        [Required]
        public string EmployeeBloodGroup { get; set; } = string.Empty;

        // Project Table
        [Required]
        public string EmployeeProject { get; set; } = string.Empty;

        // Role Table
        [Required]
        public string EmployeeRole { get; set; } = string.Empty;

        // Leave Table
        [Required]
        public int EmployeePaidLeavesTaken { get; set; }

        [Required]
        public int EmployeePaidLeavesRemaining { get; set; }

        [Required]
        public int EmployeePaylossLeavesTaken { get; set; }
    }
}
