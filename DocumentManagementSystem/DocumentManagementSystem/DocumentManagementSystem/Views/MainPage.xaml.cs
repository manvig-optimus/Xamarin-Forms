using DocumentManagementSystem.Authentication;
using DocumentManagementSystem.Models;
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
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);

            Detail = new NavigationPage(page);
            //Detail.ToolbarItems.Add(new ToolbarItem("", "nope.png", () => HandleToolbarItem(nameof(ItemOne))));
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<AuthenticationHandler>(this, "SignOutSuccessful", RedirectToLoginPage);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<AuthenticationHandler>(this, "SignOutSuccessful");
        }

        private void RedirectToLoginPage(AuthenticationHandler obj)
        {
            DisplayAlert("Logged Out", "Logged out from appliation.", "Ok");
            App.Current.MainPage = new LoginApp();
        }
    }
}