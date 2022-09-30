
using FluentMigrator;

namespace EmployeeTasks.Migrations;
[Migration(20220930001)]
public class InializeMigrations : Migration
{
    public override void Down()
    {
        Delete.Table("Tasks");
        Delete.Table("Employees");
    }

    public override void Up()
    {
        Create.Table("Employees")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("DateAdded").AsDateTime().NotNullable();


        Create.Table("Tasks")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("DateAdded").AsDateTime().NotNullable()
            .WithColumn("EmployeeId").AsGuid().NotNullable().ForeignKey("Employees", "Id");
    }
}