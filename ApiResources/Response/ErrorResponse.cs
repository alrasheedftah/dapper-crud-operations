
namespace EmployeeCrudTaskAPi.ApiResource.Responses;
    public class ErrorResponse
    {

        public bool Success { get; set; } = false;

        public int Code { get; set; } = 0;
        public string MessageResult { get; set; }
        public object Result { get; set; }

        // public string ErrorMessage {get ; set ;}
        public IEnumerable<string> Errors { get; set; }
    }
