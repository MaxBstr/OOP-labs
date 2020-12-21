using System;

namespace BLL
{
    public class Employee : IEmployee
    {
        private string Name;
        public Task CurrentTask { get; private set; }

        public Employee(string name)
        {
            Name = name;
        }

        public void SetTask(Task task)
        {
            CurrentTask = task;
        }

        public void AddReport(int date, string changes)
        {
            CurrentTask.AddReport(date, changes);
        }

        public Report MakeSprintReport()
        {
            return CurrentTask.MakeSprintReport();
        }

        public string GetName()
        {
            return Name;
        }

        public void EndTask()
        {
            CurrentTask.EndTask();
        }

        public void GetTaskInfo()
        {
            Console.WriteLine($"{Name}`s task:");
            Console.WriteLine($"{CurrentTask.taskName}; {CurrentTask.taskDescription}; {CurrentTask.status}");
        }
    }
}