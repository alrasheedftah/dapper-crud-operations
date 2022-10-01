using System.ComponentModel.DataAnnotations;

namespace EmployeeTasks.ApiResource.Requests;
    public class EmployeeRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public List<TaskRequest> TasksId { get; set; }
    }

    public class TaskRequest{
        public long Id {get;set;}
        public string Name {get;set;}
    }
