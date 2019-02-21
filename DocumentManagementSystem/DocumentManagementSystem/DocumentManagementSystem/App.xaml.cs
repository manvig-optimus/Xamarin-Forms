using DocumentManagementSystem.Authentication;
using DocumentManagementSystem.Views;
using Newtonsoft.Json;
using System;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DocumentManagementSystem
{
    public partial class App : Application
    {
        public static AccountStore Store;
        public static User UserInfo;

        private static AuthenticationHandler _instance;
        public static AuthenticationHandler AuthenticationInstance
        {
            get
            {
                return _instance ?? (_instance = new AuthenticationHandler());
            }
        }

        public App()
        {
            InitializeComponent();

            Store = AccountStore.Create();
            var account = Store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            //Handle Connectivity

            if (account == null || !Application.Current.Properties.ContainsKey("ExpiryDate"))
            {
                MainPage = new LoginApp();
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
