using DocumentManagementSystem.Models;
using DocumentManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DocumentManagementSystem.ViewModels
{
    public class MainPageMasterViewModel
    {   
        public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }
        private ICommand _handleSignOutOperation;
        public ICommand HandleSignOutOperation
        {
            get
            {
                return _handleSignOutOperation ?? (_handleSignOutOperation = new Command(SignOutHandler));
            }
        }

        public MainPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
            {
                new MainPageMenuItem { Id = 0, Title = "Profile", TargetType = typeof(UserProfile) },
                new MainPageMenuItem { Id = 1, Title = "View Files", TargetType = typeof(FilesListing) }
            });
        }

        private void SignOutHandler()
        {
            App.AuthenticationInstance.SignOutFromAccount();
        }
    }
}
