
using EmployeeTasks.ApiResource.Requests;
using EmployeeTasks.ApiResource.Responses;
using EmployeeTasks.Helpers;
using EmployeeTasks.Routes;
using EmployeeTasks.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeTasks.ApiResource.Requests;
using EmployeeTasks.ApiResource.Responses;

namespace EmployeeTasks.Controllers.Tasks;

public class TasksController : ControllerBase
{
        private ITasksServices _tasksServices;

    public TasksController(ITasksServices tasksServices){
        _tasksServices = tasksServices;
    }


        [HttpGet(ApiRoutes.TasksRouteName.TasksRouterName)]
        public async Task<IActionResult> index()
        {
            var EmployeeData = await _tasksServices.GetAllTasksAsync();
            if (EmployeeData == null)
                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorResponse
                    {
                        Errors = new[] { "SomeThing Wrong" },
                        Success = false
                    }
                };

            return Ok(EmployeeData);

        }

        [HttpPost(ApiRoutes.TasksRouteName.TasksRouterName)]
        public async Task<IActionResult> store([FromBody]TaskRequest model)
        {
            var EmployeeData = await _tasksServices.CreateTasksAsync(model);
            if (EmployeeData == null)
                throw new HttpResponseException()
                {
                    Status = 404,
                    Value = new ErrorResponse
                    {
                        Errors = new[] { "SomeThing Wrong" },
                        Success = false
                    }
                };

            return Ok(EmployeeData);

        }        

}