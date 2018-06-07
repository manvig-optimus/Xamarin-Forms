using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModels;
using EmployeeDirectory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmployeeDirectory
{
	public partial class MainPage : ContentPage
	{
        MainPageViewModel vm;

		public MainPage(Employee employee = null)
		{
            vm = new MainPageViewModel(employee);
            BindingContext = vm;

            InitializeComponent();
        }

        private void btnResetData_Clicked(object sender, EventArgs e)
        {
            firstName.Text = "";
            lastName.Text = "";
            address.Text = "";
            gender.SelectedIndex = -1;
            ageStepper.Value = ageStepper.Minimum;
            department.SelectedIndex = -1;
            designation.SelectedIndex = -1;
            interestedInClub.IsToggled = false;
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnSaveEmployee_Clicked(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                if (vm.SaveEmployeeData() > 0)
                {
                    DisplayAlert("Data saved", "Employee Data saved sucessfully.", "Ok");
                    Navigation.PopAsync();
                }
            }
        }

        private bool ValidateEntryFields(string fieldName)
        {
            DisplayAlert("Data Required", String.Format("Please enter {0}.", fieldName), "Ok");
            return false;
        }

        private bool ValidateFields()
        {
            if (String.IsNullOrEmpty(firstName.Text))
                return ValidateEntryFields("First Name");

            if (String.IsNullOrEmpty(lastName.Text))
                return ValidateEntryFields("Last Name");

            if (String.IsNullOrEmpty(address.Text))
                return ValidateEntryFields("Address");

            if (gender.SelectedIndex == -1)
                return ValidateEntryFields("Gender");

            if (department.SelectedIndex == -1)
                return ValidateEntryFields("Department");

            if (designation.SelectedIndex == -1)
                return ValidateEntryFields("Designation");           

            return true;
        }

        private void firstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
