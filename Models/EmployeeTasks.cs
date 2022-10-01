using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTasks.Models;


public class EmployeeOwnedTasks{
    public long TaskId { get; set;}
    public long EmployeeId { get; set;}
}
