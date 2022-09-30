

using EmployeeTasks.Models;
using FluentMigrator;

namespace EmployeeTasks.Migrations;
[Migration(20220930002)]
public class InitialSeeder : Migration
{
    public override void Down()
    {
        throw new NotImplementedException();
    }

    public override void Up()
    {
        Insert.IntoTable("Employees")
            .Row(new Employees
            {
                Id = new Guid("67fbac34-1ee1-4697-b916-1748861dd275"),
                Name = "EMployee 1",
                DateAdded = DateTime.Now
            });
        Insert.IntoTable("Tasks")
            .Row(new Tasks
            {
                Id = new Guid("59c0d403-71ce-4ac8-9c2c-b0e54e7c043b"),
                Name = "Task Name",
                EmployeeId = new Guid("67fbac34-1ee1-4697-b916-1748861dd275"),
                DateAdded = DateTime.Now
            });
    }
}