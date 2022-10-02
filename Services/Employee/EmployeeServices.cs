using EmployeeTasks.ApiResource.Requests;
using EmployeeTasks.ApiResource.Responses;
using EmployeeTasks.Helpers;
using System.Data;
using Dapper;
using EmployeeTasks.ApiResource.Requests;
using EmployeeTasks.EmployeeDbContext;
using EmployeeTasks.Models;

namespace EmployeeTasks.Services.Employee;

class EmployeeServices : IEmployeeServices
{

    private readonly DapperDBContext _dapperContext;

    public EmployeeServices(DapperDBContext dapperDBContext){
        _dapperContext =  dapperDBContext;
    }

    public async Task<Employees> CreateEmployeeAsync(EmployeeRequest Employee)
    {
        
            var query = "INSERT INTO employees (Name,DateAdded) VALUES (@Name,@DateAdded) " +
                "SELECT  CAST(SCOPE_IDENTITY() as bigint) ";//in MSSQL

            var parameters = new DynamicParameters();
            parameters.Add("Name", Employee.Name, DbType.String);
            parameters.Add("DateAdded", DateTime.Now, DbType.Date);

            using var connection = _dapperContext.CreateConnection();

            var id = await connection.QuerySingleAsync<long>(query, parameters);

            var createdEmloyee = new Employees
            {
                Id = id,
                Name = Employee.Name,
            };

            return createdEmloyee;
            
    }

    public async Task<int> DeleteEmployeeByIdAsync(long id)
    {
            var query = "DELETE   FROM employees   WHERE Id = @Id ";
            var queryTasks = "DELETE   FROM employees_tasks   WHERE EmployeeId = @Id ";
            using
            var conn = _dapperContext.CreateConnection();
            conn.Open();
            using var trans = conn.BeginTransaction();
            var numRowsEffect = await conn.ExecuteScalarAsync<int> (queryTasks,new { Id = id },trans);
            var numRowsEffectEmp = await conn.ExecuteScalarAsync<int> (query,new { Id = id },trans).ConfigureAwait(false);
            
            trans.Commit();
                        
            
            if(numRowsEffectEmp < 0){
                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorResponse
                    {
                        Errors = new[] { "SomeThing Wrong "+numRowsEffect },
                        Success = false
                    }
                };

            }

            return numRowsEffectEmp;     
    }

    public async Task<IEnumerable<Employees>> GetAllEmployeeAsync()
    {
            var query = "SELECT * FROM employees";
            using
            var conn = _dapperContext.CreateConnection();
            var employees = await conn.QueryAsync<Employees> (query);
            return employees.ToList();     
    }

    public Task<Employees> GetEmployeeByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Employees>> GetMultipleEmployeesAndTaskssAsyn()
    {
            var query = "SELECT  emp.*,tsk.* FROM employees emp LEFT JOIN employees_tasks em_task ON emp.Id = em_task.EmployeeId LEFT JOIN  tasks tsk ON tsk.Id = em_task.TaskId";

            using var connection = _dapperContext.CreateConnection();

                var employeeDict = new Dictionary<long, Employees>();
                var TaskDict = new Dictionary<long, TasksModel>();
                var employeesList = await connection.QueryAsync<Employees, TasksModel, Employees>(
                    query, (employee, tasks) =>
                    {
                    //    tasks.EmployeeId = employee.Id; 
                        if (!employeeDict.TryGetValue(employee.Id, out var currentEmployee))
                        {
                            currentEmployee = employee;
                            employeeDict.Add(currentEmployee.Id, currentEmployee);
                        }
                        else{
                            // TODO To Inialize This By Default Value In class 
                            // currentEmployee.Tasks = new List<TasksModel>(); 
                        }

                        currentEmployee.Tasks.Add(tasks);
                        return currentEmployee;
                    },
                    splitOn : "Id"

                );

                return employeesList.Distinct().ToList();
    }


    public async Task<Employees> CreateEmployeeWithTAsksAsync(EmployeeRequest Employee)
    {
        try{
        
            var query = "INSERT INTO employees (Name,DateAdded) VALUES (@Name,@DateAdded) " +
                "SELECT  CAST(SCOPE_IDENTITY() as bigint) ";//in MSSQL

            var queryTaskInsert = "INSERT INTO tasks (Name,DateAdded) VALUES (@Name,@DateAdded) " +
                "SELECT  CAST(SCOPE_IDENTITY() as bigint) ";//in MSSQL


            var queryTasks = "INSERT INTO employees_tasks (TaskId,EmployeeId) VALUES (@TaskId,@EmployeeId)" ;

            var parameters = new DynamicParameters();
            parameters.Add("Name", Employee.Name, DbType.String);
            parameters.Add("DateAdded", DateTime.Now, DbType.Date);

            using var connection = _dapperContext.CreateConnection();
            connection.Open();
            using var trans = connection.BeginTransaction();
            // var id = await connection.QuerySingleAsync<long>(query, parameters);

            var id = await connection.ExecuteScalarAsync<int>(query, parameters, trans);

                    foreach( var a in Employee.TasksId)
                    {
                    var TaskId = await connection.ExecuteScalarAsync<int>(queryTaskInsert, new {Name = a.Name , DateAdded = DateTime.Now }, trans);

                    var arows = await connection.ExecuteAsync(queryTasks, new { TaskId = TaskId , EmployeeId = id }, trans);
                    }

            trans.Commit();

            var createdEmloyee = new Employees
            {
                Id = id,
                Name = Employee.Name,
            };

            return createdEmloyee;
            }catch{

                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorResponse
                    {
                        Errors = new[] { "SomeThing Wrong Or Add Tasks Firs" },
                        Success = false
                    }
                };

            }
    }


    // TODO To Use BUlk Async In Dapper 
    public async Task UpdateEmployeeAsync(long id, EmployeeRequest Employee)
    {
        try{
            var queryEmpDelete = "Delete From employees_tasks where EmployeeId=@EmployeeId ";//in MSSQL

            var queryTasks = "INSERT INTO employees_tasks (EmployeeId,TaskId) VALUES (@EmployeeId,@TaskId) " ;

            var parameters = new DynamicParameters();
            parameters.Add("EmployeeId", id, DbType.Int64);

            using var connection = _dapperContext.CreateConnection();
            connection.Open();
            using var trans = connection.BeginTransaction();

            var ResId = await connection.ExecuteScalarAsync<int>(queryEmpDelete, parameters, trans);

                    foreach( var a in Employee.TasksId)
                    {
                    var arows = await connection.ExecuteAsync(queryTasks, new { EmployeeId = id ,TaskId = a.Id  }, trans);
                    }

            trans.Commit();

            }catch{

                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorResponse
                    {
                        Errors = new[] { "SomeThing Wrong Or Add Tasks Firs" },
                        Success = false
                    }
                };

            }
    }
}

