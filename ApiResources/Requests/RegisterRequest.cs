using System.ComponentModel.DataAnnotations;

namespace EmployeeCrudTaskAPi.ApiResource.Requests;
    public class RegisterRequest
    {
            [Required]
            public string Password { get; set; }
            [Required]
            public string Username { get; set; }
            [Required]
           public string Email { get; set; }

    }
