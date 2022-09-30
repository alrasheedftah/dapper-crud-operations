
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTasks.Models;

public class Employees{
    public Guid Id { get; set;}
    public string Name { get; set;}
    public DateTime DateAdded { get; set; }
    // [NotMapped]
    // public virtual ICollection<Tasks> Tasks { get; set; }


}
