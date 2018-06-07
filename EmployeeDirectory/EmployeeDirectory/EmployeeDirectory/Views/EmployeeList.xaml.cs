using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeDirectory.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmployeeList : ContentPage
	{
        EmployeeListViewModel vm;

        public EmployeeList ()
		{
            vm = new EmployeeListViewModel();
            BindingContext = vm;

            InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.RefreshEmployeeData();
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            vm.ReverseEmployeeDataOrder();
            ((ListView)sender).IsRefreshing = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        
        private void MenuItemEdit_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var employeeId = menuItem.CommandParameter;
            Navigation.PushAsync(new MainPage(App.Database.GetEmployee((int)employeeId)));
        }

        private void MenuItemDelete_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var employeeId = (int)menuItem.CommandParameter;
            if (App.Database.DeleteEmployee(employeeId) > 0)
            {
                vm.RefreshEmployeeData();
                DisplayAlert("Deleted", "Employee Data deleted successfully.", "Ok");
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (ListView)sender;
            string employeeInfo = vm.GetEmployeeInfoString((Employee)list.SelectedItem);

            DisplayAlert("Employee Info: ", employeeInfo, "Cancel");
        }
    }
}