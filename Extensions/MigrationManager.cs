
using EmployeeTasks.Migrations;
using FluentMigrator.Runner;

namespace EmployeeTasks.Extensions;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
            var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            try
            {
                databaseService.CreateDatabase("employeesdb");

                migrationService.ListMigrations();
                migrationService.MigrateUp();     

            }
            catch
            {
                //log errors or ...
                // Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaa");
                throw;
            }
        }

        return host;
    }
}