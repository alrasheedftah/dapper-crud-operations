using System.ComponentModel.DataAnnotations;

namespace EmployeeTasks.ApiResource.Requests;
    public class RegisterRequest
    {
            [Required]
            public string Password { get; set; }
            [Required]
            public string Username { get; set; }
            [Required]
           public string Email { get; set; }

    }
