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
        => new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
    public IDbConnection CreateMasterConnection()
        => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

}