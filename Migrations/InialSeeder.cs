

using EmployeeTasks.Models;
using FluentMigrator;
using FluentMigrator.SqlServer;

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
        Insert.IntoTable("employees")
            .WithIdentityInsert()
            .Row(new Employees
            {
                Id = 11,
                Name = "EMployee 1",
                DateAdded = DateTime.Now
            });
        Insert.IntoTable("tasks")
            .WithIdentityInsert()
            .Row(new TasksModel
            {
                Id = 11,
                Name = "Task Name",
                EmployeeId = 11,
                DateAdded = DateTime.Now
            });

        Insert.IntoTable("employees_tasks")
            .WithIdentityInsert()
            .Row(new EmployeeOwnedTasks
            {
                TaskId = 11,
                EmployeeId = 11
            });            
    }
}