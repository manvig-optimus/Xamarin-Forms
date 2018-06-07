using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneNumberTranslator.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CallHistory : ContentPage
	{
		public CallHistory ()
		{
            BindingContext = App.PhoneNumbers;
            InitializeComponent ();

            if (App.PhoneNumbers.Count > 0)
            {
                btnClearLogs.IsEnabled = true;
            }
		}

        private async void btnClearLogs_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Delete Call History", "Press Ok to delete the call history.", "Ok", "Cancel"))
            {
                App.PhoneNumbers.Clear();
                btnClearLogs.IsEnabled = false;
            }
        }
    }
}