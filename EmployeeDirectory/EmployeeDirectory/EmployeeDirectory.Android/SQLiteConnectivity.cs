using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EmployeeDirectory.Data;
using EmployeeDirectory.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: DependencyAttribute(typeof(SQLiteConnectivity))]
namespace EmployeeDirectory.Droid
{
    public class SQLiteConnectivity : ISQLiteConnectivity
    {
        public SQLiteConnection GetConnection(string fileName)
        {
            var localPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(localPath, fileName);

            return new SQLiteConnection(filePath);
        }
    }
}