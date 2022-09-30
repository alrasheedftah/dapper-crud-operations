
namespace EmployeeTasks.Models;
public class Tasks{
    public Guid Id { get; set;}
    public string Name { get; set;}
    public DateTime DateAdded { get; set; }

    public Guid EmployeeId { get ; set; }
}
