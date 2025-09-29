using System.ComponentModel.DataAnnotations;

namespace BlazorStandAlone.Models
{
    public class AddEmployeeDto
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
    }



}
