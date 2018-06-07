using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeDirectory.Models;
using SQLite;
using Xamarin.Forms;

namespace EmployeeDirectory.Data
{
    public class EmployeeDatabase
    {
        private readonly SQLiteConnection connection;

        public EmployeeDatabase(string fileName)
        {
            if (connection == null)
            {
                connection = DependencyService.Get<ISQLiteConnectivity>().GetConnection(fileName);
                connection.CreateTable<Employee>();
            }
        }

        public List<Employee> GetEmployees()
        {
            return (from c in connection.Table<Employee>() orderby c.FirstName select c).ToList();
        }

        public Employee GetEmployee(int id)
        {
            return connection.Get<Employee>(id);
        }

        public int DeleteEmployee(int id)
        {
            return connection.Delete<Employee>(id);
        }

        public int SaveEmployee(Employee employee)
        {
            if (employee.EmployeeId == 0)
            {
                return connection.Insert(employee);
            }
            else
            {
                return connection.Update(employee);
            }
        }
    }
}
