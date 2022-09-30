




using EmployeeCrudTaskAPi.ApiResource.Responses;

namespace EmployeeTasks.Services.Auth;

    public interface IAuthServices
    {
        Task<SuccessResponse> Authenticate(string username, string password);
        Task<SuccessResponse> Register(string email,string username, string password);
    }

