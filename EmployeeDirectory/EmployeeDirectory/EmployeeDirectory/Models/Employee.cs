using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDirectory.Models
{
    public class Employee
    {
        [AutoIncrement, PrimaryKey]
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public bool HasInterestInClub { get; set; }
    }
}
