using DocumentManagementSystem.Authentication;
using DocumentManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocumentManagementSystem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;
        MainPageMasterViewModel vm;

        public MainPageMaster()
        {
            vm = new MainPageMasterViewModel();
            BindingContext = vm;
            InitializeComponent();            
            ListView = MenuItemsListView;
        }        
    }
}