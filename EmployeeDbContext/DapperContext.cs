using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EmployeeTasks.EmployeeDbContext;
public class DapperDBContext 
{
    private readonly IConfiguration _configuration;
    public DapperDBContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_configuration.GetConnectionString("SqlConnection"));
    public IDbConnection CreateMasterConnection()
        => new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));

}