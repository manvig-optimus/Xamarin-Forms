using DocumentManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocumentManagementSystem.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilesListing : ContentPage
	{
        FilesListingViewModel vm;

        public FilesListing ()
		{
            vm = new FilesListingViewModel();
            BindingContext = vm;
            InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (vm.TokenSource.IsCancellationRequested)
            {
                vm.LoadFiles();
            }
            MessagingCenter.Subscribe<FilesListingViewModel>(this, "IncorrectRequest", HandleIncorrectRequest);            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.TokenSource.Cancel();
            MessagingCenter.Unsubscribe<FilesListingViewModel>(this, "IncorrectRequest");            
        }

        private void HandleIncorrectRequest(FilesListingViewModel obj)
        {
            DisplayAlert("Error", "Some error occurred while requesting the files. Please try again later.", "Ok");
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedFile = (File)e.SelectedItem;
            Navigation.PushAsync(new FileDetails(selectedFile));
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            vm.TokenSource.Cancel();
            vm.SelectedFilter = Convert.ToString(((Picker)sender).SelectedItem);
            vm.LoadFiles();
        }
    }
}