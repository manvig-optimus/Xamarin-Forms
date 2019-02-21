using DocumentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DocumentManagementSystem.ViewModels
{
    public class FileDetailsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string WebViewLink { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string ModifiedBy { get; set; }
        public ObservableCollection<Permission> Permissions { get; set; } = new ObservableCollection<Permission>();

        public ICommand _openFileCommand { get; set; }
        public ICommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ?? (_openFileCommand = new Command<string>(OpenFile));
            }
        }

        public FileDetailsViewModel(File file)
        {
            InitializeFileData(file);
        } 

        private void InitializeFileData(File file)
        {
            Id = file.Id;
            Name = file.Name;
            WebViewLink = file.WebViewLink;
            ModifiedTime = Convert.ToDateTime(file.ModifiedTime);
            ModifiedBy = String.Format("{0} ({1})", file.LastModifyingUser.Email, file.LastModifyingUser.Name);

            if (file.Permissions != null)
                LoadPermissions(file.Permissions);
        }
        
        private void LoadPermissions(List<Permission> permissions)
        {
            foreach(var permission in permissions)
            {
                if (permission.Type == "user" && permission.Deleted == "false")
                {
                    permission.PhotoLink = permission.PhotoLink ?? "N/A";
                    if (permission.Role == "owner")
                        Permissions.Insert(0, permission);
                    else
                        Permissions.Add(permission);
                }
            }
        }

        private void OpenFile(string url)
        {
            Device.OpenUri(new System.Uri(url));
        }
    }
}
