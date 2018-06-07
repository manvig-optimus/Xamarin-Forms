using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EmployeeDirectory.Data
{
    public interface ISQLiteConnectivity
    {
        SQLiteConnection GetConnection(string fileName);
    }
}
