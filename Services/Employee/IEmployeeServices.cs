
using EmployeeTasks.Models;

namespace EmployeeTasks.Services;

public interface IEmployeeServices {
        Task <IEnumerable<Employees>> GetAllEmployeeAsync();
        Task <Employees> GetEmployeeByIdAsync(int id);
        Task < Employees > CreateEmployeeAsync(Object Employee);
        Task UpdateEmployeeAsync(int id, Object Employee);
        Task DeleteEmployeeByIdAsync(int id);
        Task < Employees > GetEmployeeByStudentIdAsync(int EmployeeId);
        Task < Employees > GetEmployeeWithStudentsByEmployeeId(int EmployeeId);
        Task < List < Employees >> GetMultipleEmployeesAndStudentsAsyn();
        // Task CreateListOfSchoolsByAsync(List < SchoolDto > schoolList);

}