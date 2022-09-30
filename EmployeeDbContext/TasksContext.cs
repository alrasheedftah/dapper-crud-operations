using Microsoft.EntityFrameworkCore;

namespace EmployeeTasks.EmployeeDbContext;
public class TasksContext : DbContext
{
        public TasksContext(DbContextOptions<TasksContext> options)
            : base(options)
        {
        }

}