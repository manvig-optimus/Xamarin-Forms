using DocumentManagementSystem.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace DocumentManagementSystem.ViewModels
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _verifiedEmail;
        public bool VerifiedEmail
        {
            get { return _verifiedEmail; }
            set
            {
                if (value != _verifiedEmail)
                {
                    _verifiedEmail = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _link;
        public string Link
        {
            get { return _link; }
            set
            {
                if (value != _link)
                {
                    _link = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _picture;
        public string Picture
        {
            get { return _picture; }
            set
            {
                if (value != _picture)
                {
                    _picture = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set
            {
                if (value != _gender)
                {
                    _gender = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public UserProfileViewModel()
        {
            GetUserInfo();
        }

        private async void GetUserInfo()
        {
            if (App.UserInfo == null)
            {
                string result = await RequestHandler.ProcessRequest(Constants.UserInfoUrl, null);
                if (String.IsNullOrEmpty(result) || result == Constants.IncorrectRequest)
                {
                    MessagingCenter.Send<UserProfileViewModel>(this, "IncorrectRequest");
                    return;
                }
                else
                {
                    App.UserInfo = JsonConvert.DeserializeObject<User>(result);
                }
            }
            SetUserDetails(App.UserInfo);
        }

        private void SetUserDetails(User user)
        {            
            Id = user.Id;
            Email = user.Email;
            VerifiedEmail = user.VerifiedEmail;
            Name = user.Name;
            Link = user.Link;
            Picture = user.Picture;
            Gender = user.Gender;
        }
    }
}
