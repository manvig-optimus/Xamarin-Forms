using DocumentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace DocumentManagementSystem.Authentication
{
    public class AuthenticationHandler
    {
        private static string ClientId
        {
            get
            {
                return (Device.RuntimePlatform == Device.iOS ? Constants.iOSClientId : Constants.AndroidClientId);
            }
        }

        private static string RedirectUri
        {
            get
            {
                return (Device.RuntimePlatform == Device.iOS ? Constants.iOSRedirectUrl : Constants.AndroidRedirectUrl);
            }
        }

        private void CreateAuthenticator()
        {
            var authenticator = new OAuth2Authenticator(
                ClientId,
                null,
                Constants.Scope,
                new Uri(Constants.AuthorizeUrl),
                new Uri(RedirectUri),
                new Uri(Constants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;
        }

        public void AuthenticateAccount()
        {
            CreateAuthenticator();
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(AuthenticationState.Authenticator);
        }

        void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            if (e.IsAuthenticated)
            {
                SetTokenAndExpiryDate(e.Account);
                SaveAccountInfo(e.Account);
                MessagingCenter.Send<AuthenticationHandler>(this, "LoginSuccessful");
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
        }
        
        private async void SetTokenAndExpiryDate(Account account)
        {
            var tokenExpiryPeriod = Convert.ToDouble(account.Properties["expires_in"]);
            var expiryDate = DateTime.Now.AddSeconds(tokenExpiryPeriod).ToString();

            if (Application.Current.Properties.ContainsKey("ExpiryDate"))
                Application.Current.Properties.Remove("ExpiryDate");
            Application.Current.Properties.Add("ExpiryDate", expiryDate);

            if (account.Properties.ContainsKey("refresh_token"))
            {
                if (Application.Current.Properties.ContainsKey("RefreshToken"))
                    Application.Current.Properties.Remove("RefreshToken");
                Application.Current.Properties.Add("RefreshToken", account.Properties["refresh_token"]);
            }

            await Application.Current.SavePropertiesAsync();
        }

        private void DeleteAccount()
        {
            var account = App.Store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            if (account != null)
            {
                App.Store.Delete(account, Constants.AppName);
            }
        }

        private async void SaveAccountInfo(Account authenticatedAccount)
        {
            DeleteAccount();
            await App.Store.SaveAsync(authenticatedAccount, Constants.AppName);
        }
        
        public void SignOutFromAccount()
        {
            DeleteAccount();
            Application.Current.Properties.Clear();
            MessagingCenter.Send<AuthenticationHandler>(this, "SignOutSuccessful");
        }

        public async Task<bool> RequestRefreshTokenAsync(Account account)
        {
            if (!Application.Current.Properties.ContainsKey("RefreshToken"))
            {
                SignOutFromAccount();
                return false;
            }
            else
            {
                var postDictionary = new Dictionary<string, string>();
                postDictionary.Add("refresh_token", Application.Current.Properties["RefreshToken"].ToString());
                postDictionary.Add("client_id", ClientId);
                postDictionary.Add("grant_type", "refresh_token");

                try
                {
                    CreateAuthenticator();
                    return await AuthenticationState.Authenticator.RequestAccessTokenAsync(postDictionary).ContinueWith
                    (
                        result =>
                        {
                            var accountProperties = result.Result;
                            AuthenticationState.Authenticator.OnRetrievedAccountProperties(accountProperties);
                            return true;
                        }
                    );
                }
                catch
                {
                    SignOutFromAccount();
                    return false;
                }
            }
        }
    }
}
