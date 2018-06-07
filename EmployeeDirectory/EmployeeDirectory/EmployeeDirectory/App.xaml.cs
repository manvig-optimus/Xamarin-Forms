using EmployeeDirectory.Data;
using EmployeeDirectory.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace EmployeeDirectory
{
	public partial class App : Application
	{
        private static EmployeeDatabase database;
        public static EmployeeDatabase Database {
            get
            {
                return database ?? (database = new EmployeeDatabase("EmployeeDb.db3"));
            }
        }

        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new EmployeeList());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
