using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTasks.Models;
public class TasksModel{
    [ColumnAttribute("id")]
    public long Id { get; set;}
    public string Name { get; set;}
    public DateTime DateAdded { get; set; }

    public long EmployeeId { get ; set; }
}
