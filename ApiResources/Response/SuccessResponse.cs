
namespace EmployeeTasks.ApiResource.Responses;
    public class SuccessResponse
    {
        public int code { get; set; }
        public object Result { get; set; }
        public string MessageResult { get; set; }
        public bool Success { get; set; } = true;
    }

