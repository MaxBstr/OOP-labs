using System;
using System.Collections.Generic;

namespace DAL
{
    public class Database : IDatabase
    {
        private static Database _instance;
        private Database() {}

        public static Database Create()
        {
            if (_instance == null)
                _instance = new Database();

            return _instance;
        }
        
        private List<Task> _tasks = new List<Task>();

        public void CreateTask(Task task)
        {
            task.Id = GetCount() + 1;
            _tasks.Add(task);
        }

        public IEnumerable<Task> GetAll()
        {
            return _tasks;
        }

        public int GetCount()
        {
            return _tasks.Count;
        }

        public void Update(Task task)
        {
            var curTask = _tasks.Find(t => t.Id == task.Id);
            _tasks.Remove(curTask);
            _tasks.Add(task);
        }

        public Task GetTaskById(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task == null)
                throw new Exception($"{id} task was not found");

            return task;
        }

        public Task GetTaskByEmployee(string name)
        {
            var task = _tasks.Find(t => t.EmployeeName == name);
            if (task == null)
                throw new Exception($"Task does not match to {name} or does not exist!");
            return task;
        }

        public IEnumerable<Task> GetActiveTasks()
        {
            return _tasks.FindAll(t => t.Status == TaskStatus.Active);
        }
    }
}