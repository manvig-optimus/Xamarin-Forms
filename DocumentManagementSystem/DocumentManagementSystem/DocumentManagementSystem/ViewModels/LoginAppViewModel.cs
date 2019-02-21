using DocumentManagementSystem.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;

namespace DocumentManagementSystem.ViewModels
{
    public class LoginAppViewModel
    {        
        private ICommand _googleLogin;
        public ICommand GoogleLogin
        {
            get
            {
                return _googleLogin ?? (_googleLogin = new Command(AuthenticateAccount));
            }
        }

        private void AuthenticateAccount(object obj)
        {
            App.AuthenticationInstance.AuthenticateAccount();
        }
    }
}
