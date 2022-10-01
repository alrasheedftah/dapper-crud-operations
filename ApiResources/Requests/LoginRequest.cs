using System.ComponentModel.DataAnnotations;

namespace EmployeeTasks.ApiResource.Requests;
    public class LoginRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
