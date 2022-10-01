
using EmployeeTasks.ApiResource.Requests;
using EmployeeTasks.Models;

namespace EmployeeTasks.Services.Employee;

public interface IEmployeeServices {
        Task <IEnumerable<Employees>> GetAllEmployeeAsync();
        Task <Employees> GetEmployeeByIdAsync(int id);
        Task <Employees> CreateEmployeeAsync(EmployeeRequest Employee);
        Task <Employees> CreateEmployeeWithTAsksAsync(EmployeeRequest Employee);
        Task UpdateEmployeeAsync(long id, EmployeeRequest Employee);
        Task <int> DeleteEmployeeByIdAsync(long id);
        Task <List<Employees>> GetMultipleEmployeesAndTaskssAsyn();
        // Task CreateListOfSchoolsByAsync(List < SchoolDto > schoolList);

}