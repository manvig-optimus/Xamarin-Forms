using DocumentManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocumentManagementSystem.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProfile : ContentPage
	{
        UserProfileViewModel vm;

        public UserProfile ()
		{
            vm = new UserProfileViewModel();
            BindingContext = vm;
            InitializeComponent ();            
        }
        
        protected override void OnAppearing()
        {            
            base.OnAppearing();
            MessagingCenter.Subscribe<UserProfileViewModel>(this, "IncorrectRequest", HandleIncorrectRequest);
        }

        protected override void OnDisappearing()
        {            
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<UserProfileViewModel>(this, "IncorrectRequest");
        }

        private void HandleIncorrectRequest(UserProfileViewModel obj)
        {
            DisplayAlert("Error", "Some error occurred while fetching the User info. Please try again later.", "Ok");
        }
    }
}