using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace PhoneNumberTranslator
{
	public partial class App : Application
	{
        public static ObservableCollection<string> PhoneNumbers;

		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());

            PhoneNumbers = new ObservableCollection<string>();
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
