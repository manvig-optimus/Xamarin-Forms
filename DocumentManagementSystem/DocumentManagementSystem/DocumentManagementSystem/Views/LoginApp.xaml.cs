using System;
using DocumentManagementSystem.Authentication;
using DocumentManagementSystem.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocumentManagementSystem.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginApp : ContentPage
	{
        LoginAppViewModel vm;

        public LoginApp()
        {
            vm = new LoginAppViewModel();
            BindingContext = vm;
            InitializeComponent();
        }        

        protected override void OnAppearing()
        {            
            base.OnAppearing();
            MessagingCenter.Subscribe<AuthenticationHandler>(this, "LoginSuccessful", AuthenticationComplete);
        }

        protected override void OnDisappearing()
        {            
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<AuthenticationHandler>(this, "LoginSuccessful");
        }

        private void AuthenticationComplete(AuthenticationHandler obj)
        {
            DisplayAlert("Success", "Login Successful", "Ok");
            App.Current.MainPage = new NavigationPage(new MainPage()); ;
        }
    }
}