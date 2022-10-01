

using EmployeeTasks.ApiResource.Requests;
using EmployeeTasks.ApiResource.Responses;
using Dapper;
using EmployeeTasks.EmployeeDbContext;
using EmployeeTasks.Models;
using System.Data;
using EmployeeTasks.ApiResource.Requests;

namespace EmployeeTasks.Services.Tasks;

class TasksServices : ITasksServices
{

    private readonly DapperDBContext _dapperContext;

    public TasksServices(DapperDBContext dapperDBContext){
        _dapperContext =  dapperDBContext;
    }

    public async Task<TasksModel> CreateTasksAsync(TaskRequest Employee)
    {
            var query = "INSERT INTO tasks (Name,DateAdded) VALUES (@Name,@DateAdded)" +
                "SELECT  CAST(SCOPE_IDENTITY() as bigint) ";//in MSSQL

            var parameters = new DynamicParameters();
            parameters.Add("Name", Employee.Name, DbType.String);
            parameters.Add("DateAdded", DateTime.Now, DbType.Date);

            using var connection = _dapperContext.CreateConnection();

            var id = await connection.QuerySingleAsync<long>(query, parameters);

            var createdEmloyee = new TasksModel
            {
                Id = id,
                Name = Employee.Name,
            };

            return createdEmloyee;

    }

    public Task DeleteTasksByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TasksModel>> GetAllTasksAsync()
    {
            var query = "SELECT * FROM tasks";
            using
            var conn = _dapperContext.CreateConnection();
            var employees = await conn.QueryAsync<TasksModel> (query);
            return employees.ToList();    

    }

    public Task<List<TasksModel>> GetMultipleEmployeesAndTaskssAsyn()
    {
        throw new NotImplementedException();
    }

    public Task<TasksModel> GetTasksByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTasksAsync(int id, TasksModel Employee)
    {
        throw new NotImplementedException();
    }
}

