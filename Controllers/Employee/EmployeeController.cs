
using EmployeeTasks.ApiResource.Responses;
using EmployeeTasks.ApiResource.Requests;
using EmployeeTasks.Helpers;
using EmployeeTasks.Routes;
using EmployeeTasks.Services.Auth;
using EmployeeTasks.Services.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTasks.Controllers.Employee;

// [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = "Bearer")]
// [ApiController]
[ApiController]
public class EmployeeController : ControllerBase
{

        private IEmployeeServices _employeeServices;
 
        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }
   

        [HttpGet(ApiRoutes.EmployRoute.EmployeesRouterName)]
        public async Task<IActionResult> index()
        {
            var EmployeeData = await _employeeServices.GetMultipleEmployeesAndTaskssAsyn();
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

            return Ok(
                    new SuccessResponse
                    {
                    Result = EmployeeData,
                    Success = true,
                    }                
            );

        }


        [HttpPost(ApiRoutes.EmployRoute.EmployeesRouterName)]
        public async Task<IActionResult> store([FromBody] EmployeeRequest model)
        {
            var EmployeeData = await _employeeServices.CreateEmployeeWithTAsksAsync(model);
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

            return Ok(
                    new SuccessResponse
                    {
                    Result = EmployeeData,
                    Success = true,
                    }                
            );
        }        


        [HttpPut(ApiRoutes.EmployRoute.EmployeesRouterName+"/{Id}")]
        public async Task<IActionResult> update([FromRoute]long Id ,[FromBody] EmployeeRequest model)
        {
             await _employeeServices.UpdateEmployeeAsync(Id,model);
            // if (EmployeeData == null)
            //     throw new HttpResponseException()
            //     {
            //         Status = 404,
            //         Value = new ErrorResponse
            //         {
            //             Errors = new[] { "SomeThing Wrong" },
            //             Success = false
            //         }
            //     };

            return Ok(
                    new SuccessResponse
                    {
                    Result = null,
                    Success = true,
                    MessageResult = " Updated Successfuly",
                    }                
            );
        }                  

        [HttpDelete(ApiRoutes.EmployRoute.EmployeesRouterName+"/{Id}")]
        public async Task<IActionResult> delete([FromRoute]long Id)
        {
             var numRowsEffectEmp =await _employeeServices.DeleteEmployeeByIdAsync(Id);

            // TODO Add If For numbEffect
             return Ok(
                    new SuccessResponse
                    {
                    Result = null,
                    Success = true,
                    MessageResult = " DEleted Succefully"
                    }                
            );
        }                           
}