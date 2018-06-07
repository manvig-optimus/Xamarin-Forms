using EmployeeDirectory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EmployeeDirectory.ViewModels
{
    public class MainPageViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }        
        public bool HasInterestInClub { get; set; }

        public List<string> Departments { get; set; }
        public List<string> Designations { get; set; }    

        public MainPageViewModel(Employee employee)
        {
            InstantiatePickers();

            if (employee != null)
            {
                EmployeeId = employee.EmployeeId;
                FirstName = employee.FirstName;
                LastName = employee.LastName;
                Address = employee.Address;
                Gender = employee.Gender;
                Age = employee.Age;
                Department = employee.Department;
                Designation = employee.Designation;
                HasInterestInClub = employee.HasInterestInClub;
            }
        }

        private void InstantiatePickers()
        {
            Designations = new List<string>()
            {
                "Software Trainee", "Software Engineer", "Senior Software Engineer", "Team Lead", "Manager", "Senior Manager", "Director"
            };

            Departments = new List<string>()
            {
                "Development", "Testing", "Business Intelligence", "DBA", "Networking", "HR", "Admin"
            };
        }

        public int SaveEmployeeData()
        {
            var employee = new Employee()
            {
                EmployeeId = this.EmployeeId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Address = this.Address,
                Gender = this.Gender,
                Age = this.Age,
                Department = this.Department,
                Designation = this.Designation,
                HasInterestInClub = this.HasInterestInClub
            };

            return App.Database.SaveEmployee(employee);            
        }
    }
}
