using System.ComponentModel.DataAnnotations;

namespace CRUD_Employee_standAlone.Models
{
    public class UpdateUserRequest
    {
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        public string? FullName { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}