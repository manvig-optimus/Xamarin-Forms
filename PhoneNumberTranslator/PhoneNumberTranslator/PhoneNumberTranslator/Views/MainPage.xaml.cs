using PhoneNumberTranslator.Dialer;
using PhoneNumberTranslator.ViewModels;
using PhoneNumberTranslator.Views;
using System;
using Xamarin.Forms;

namespace PhoneNumberTranslator
{
	public partial class MainPage : ContentPage
	{
        MainPageViewModel vm;

		public MainPage()
		{
            vm = new MainPageViewModel();
            BindingContext = vm;

            InitializeComponent();

            MessagingCenter.Subscribe<MainPageViewModel>(vm, "NumberTranslated", OnTranslation);
		}

        private void OnTranslation(MainPageViewModel obj)
        {
            if (!String.IsNullOrEmpty(vm.TranslatedNumber))
            {
                btnCall.IsEnabled = true;
                btnCall.Text = "Call Number: " + vm.TranslatedNumber;
            }
            else
            {
                btnCall.IsEnabled = false;
                btnCall.Text = "Call";
            }
        }

        private async void OnCall(object sender, EventArgs e)
        {
            if (await DisplayAlert("Dial a Number", "Would you like to call "+vm.TranslatedNumber + "?", "Ok", "Cancel"))
            {
                var dialer = DependencyService.Get<IDialer>();

                if (dialer != null)
                {
                    dialer.Dial(vm.TranslatedNumber);
                }
            }
        }

        private void OnCallHistory(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CallHistory());
        }
    }
}
