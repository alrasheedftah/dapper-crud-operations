
using EmployeeTasks.Models;
using EmployeeTasks.ApiResource.Requests;
using EmployeeTasks.ApiResource.Responses;

namespace EmployeeTasks.Services.Tasks;

public interface ITasksServices {
        Task <IEnumerable<TasksModel>> GetAllTasksAsync();
        Task <TasksModel> GetTasksByIdAsync(int id);
        Task <TasksModel> CreateTasksAsync(TaskRequest Employee);
        Task UpdateTasksAsync(int id, TasksModel Employee);
        Task DeleteTasksByIdAsync(int id);
        Task <List<TasksModel>> GetMultipleEmployeesAndTaskssAsyn();
        // Task CreateListOfSchoolsByAsync(List < SchoolDto > schoolList);

}