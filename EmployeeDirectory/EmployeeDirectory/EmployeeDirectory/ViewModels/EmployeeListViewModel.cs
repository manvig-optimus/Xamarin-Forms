using EmployeeDirectory.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;

namespace EmployeeDirectory.ViewModels
{
    public class EmployeeListViewModel
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public EmployeeListViewModel()
        {
            Employees =  new ObservableCollection<Employee>();
        }

        public string GetEmployeeInfoString(Employee employee)
        {
            var employeeInfo = new StringBuilder();

            employeeInfo.Append("\r\nName: ")
                .Append(employee.FirstName)
                .Append(" ")
                .Append(employee.LastName)
                .Append("\r\nAddress: ")
                .Append(employee.Address)
                .Append("\r\nGender: ")
                .Append(employee.Gender)
                .Append("\r\nAge: ")
                .Append(employee.Age)
                .Append("\r\nDesignation: ")
                .Append(employee.Designation)
                .Append("\r\nDepartment: ")
                .Append(employee.Department);

            return employeeInfo.ToString();
        }

        public void RefreshEmployeeData()
        {
            Employees.Clear();

            var employeeList = App.Database.GetEmployees();

            foreach(var employee in employeeList)
            {
                Employees.Add(employee);
            }
        }

        public void ReverseEmployeeDataOrder()
        {
            var list = Employees.Reverse().ToList();
            Employees.Clear();

            foreach (var employee in list)
            {
                Employees.Add(employee);
            }
        }
    }
}
