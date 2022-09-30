using Dapper;
using EmployeeTasks.EmployeeDbContext;

namespace EmployeeTasks.Migrations;
public class Database
{
    private readonly DapperDBContext _context;
    public Database(DapperDBContext context)
    {
        _context = context;
    }
    public void CreateDatabase(string dbName)
    {
        var query = "SELECT * FROM pg_database WHERE datname = @datname"; //sys.databases in MSSQL
        var parameters = new DynamicParameters();
        parameters.Add("datname", dbName); // name in MSSQL
        using (var connection = _context.CreateMasterConnection())
        {
            var records = connection.Query(query, parameters);
            Console.Write("sorryratsta "+records);
            if (!records.Any())
                connection.Execute($"CREATE DATABASE {dbName}");
        }
    }
}