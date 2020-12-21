using System.Collections.Generic;

namespace DAL
{
    public interface IDatabase
    {
        void CreateTask(Task task);
        IEnumerable<Task> GetAll();
        int GetCount();

        void Update(Task task);
        Task GetTaskById(int id);
        Task GetTaskByEmployee(string name);
        IEnumerable<Task> GetActiveTasks();
    }
}