
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTasks.Models;

public class Employees{
    public long Id { get; set;}
    public string Name { get; set;}
    public DateTime DateAdded { get; set; }
    // [NotMapped]
    public virtual ICollection<TasksModel> Tasks { get; set; } = new List<TasksModel>();


}
