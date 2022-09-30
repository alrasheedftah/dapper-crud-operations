

using EmployeeCrudTaskAPi.ApiResource.Requests;
using EmployeeCrudTaskAPi.ApiResource.Responses;
using EmployeeTasks.Routes;
using EmployeeTasks.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTasks.Controllers.Auth;

    public class AuthController : ControllerBase
    {
        private IAuthServices _authServices;

        public AuthController(IAuthServices authService)
        {
            _authServices = authService;
        }


        [AllowAnonymous]
        [HttpPost(ApiRoutes.AuthRoute.Login)]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                return Ok(new ErrorResponse
                {
                    Errors = new[] { " Validation Error  " }
                });
            var authResponse = await _authServices.Authenticate(model.Username, model.Password);
            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.AuthRoute.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return Ok(new ErrorResponse
                {
                    Errors = new[] { " Validation Error  " }
                });
            var authResponse = await _authServices.Register(model.Email,model.Username, model.Password);

            return Ok(authResponse);
        }

    }
