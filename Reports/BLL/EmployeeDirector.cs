using System;
using System.Collections.Generic;

namespace BLL
{
    public class EmployeeDirector : IEmployee
    {
        private readonly string _name;
        private readonly List<Employee> _childrenEmployees = new List<Employee>();

        public EmployeeDirector(string name)
        {
            _name = name;
        }

        public Employee ApplyEmployee(string name)
        {
            var newEmployee = new Employee(name);
            _childrenEmployees.Add(newEmployee);
            return newEmployee;
        }

        public void DismissEmployee(Employee employee)
        {
            _childrenEmployees.Remove(employee);
        }

        public void AddTaskToEmployee(Employee emp, Task newTask)
        {
            var employee = _childrenEmployees.Find(em => em == emp);
            if (employee == null)
                throw new Exception($"Employee {emp.GetName()} does not exist");
            
            newTask.SetEmployee(emp);
            employee.SetTask(newTask);
        }

        public string GetName()
        {
            return _name;
        }

        public Report MakeSprintReport()
        {
            var sprintReport = new Report();
            sprintReport.AddChanges($"Director {_name} checked reports");
            
            foreach (var emp in _childrenEmployees)
            {
                var empReport = emp.MakeSprintReport();
                sprintReport.AddChanges(empReport.GetInfo());
            }
            return sprintReport;
        }

        public Employee FindEmployee(string name)
        {
            return _childrenEmployees.Find(e => e.GetName() == name);
        }

        public void GetEmployeesTasks()
        {
            Console.WriteLine($"Director`s {_name} employees tasks:\n");
            foreach (var emp in _childrenEmployees)
            {
                emp.GetTaskInfo();
                Console.WriteLine("\n");
            }
        }
    }
}