
using FluentMigrator;
using FluentMigrator.SqlServer;


namespace EmployeeTasks.Migrations;
[Migration(20220930001)]
public class InializeMigrations : Migration
{
    public override void Down()
    {
        Delete.Table("tasks");
        Delete.Table("employees");
    }

    public override void Up()
    {
        Create.Table("employees")
            .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity(10,1)
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("DateAdded").AsDateTime().NotNullable();


        Create.Table("tasks")
            .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity(10,1)
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("DateAdded").AsDateTime().NotNullable();


        Create.Table("employees_tasks")
            .WithColumn("TaskId").AsInt64().NotNullable().ForeignKey("tasks", "Id")
            .WithColumn("EmployeeId").AsInt64().NotNullable().ForeignKey("employees", "Id");            
    }
}