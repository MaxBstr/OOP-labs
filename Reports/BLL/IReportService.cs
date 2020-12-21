using System.Collections.Generic;

namespace BLL
{
    public interface IReportService
    {
        Task CreateTask(string taskName, string taskDescription, IEmployee employee);
        IEnumerable<Task> GetAllTasks();
        Task GetTaskById(int id);
        void GetEmployeesTasksByDirector(string directorName);
        Task GetTaskByEmployee(Employee employee);
        IEnumerable<Task> GetActiveTasks();
        
    }
}