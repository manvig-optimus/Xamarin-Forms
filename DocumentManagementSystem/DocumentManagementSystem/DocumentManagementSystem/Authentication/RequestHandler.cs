using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace DocumentManagementSystem.Authentication
{
    public static class RequestHandler
    {
        public static async Task<string> ProcessRequest(string url, Dictionary<string, string> parameters)
        {
            var result = await RefreshAccountIfTokenExpired();
            if (result)
            {
                return await GetResponseForRequest(url, parameters);
            }
            else
            {
                return "";
            }
        }

        //    return await RefreshAccountIfTokenExpired().ContinueWith(
        //            async result =>
        //            {
        //                //if (result.Result)
        //                //{
        //                    var data = await GetResponseForRequest(url, parameters);
        //                    return data;
        //                //}
        //                //return "testing";
        //                //return "";
        //            }
        //    );
        //}

        private static async Task<string> GetResponseForRequest(string url, Dictionary<string, string> parameters)
        {            
            var account = App.Store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            var request = new OAuth2Request("GET", new Uri(url), parameters, account);

            try
            {
                var response = await request.GetResponseAsync();                
                try
                {
                    if (response != null)
                    {
                        //var result = response.Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return response.GetResponseText();
                        }
                        else
                        {
                            return Constants.IncorrectRequest;
                        }
                    }
                }
                catch (Exception e)
                {
                    return "test";
                }
                return "";
                //return await request.GetResponseAsync().ContinueWith(
                //    response =>
                //    {
                //        try
                //        {
                //            if (response != null)
                //            {
                //                var result = response.Result;
                //                if (result.StatusCode == HttpStatusCode.OK)
                //                {
                //                    return result.GetResponseText();
                //                }
                //                else
                //                {
                //                    return Constants.IncorrectRequest;
                //                }
                //            }
                //        }
                //        catch(Exception e)
                //        {
                //            return "test";
                //        }
                //        return "";
                //    });
            }
            catch(Exception e)
            {
                return "test";
            }
        }

        private static async Task<bool> RefreshAccountIfTokenExpired()
        {            
            if (Application.Current.Properties.ContainsKey("ExpiryDate"))
            {
                DateTime expiryDate = Convert.ToDateTime(Application.Current.Properties["ExpiryDate"]);

                if (expiryDate < DateTime.Now)
                {
                    var account = App.Store.FindAccountsForService(Constants.AppName).FirstOrDefault();
                    return await App.AuthenticationInstance.RequestRefreshTokenAsync(account);
                }
            }
            return true;
            //App.AuthenticationInstance.RequestRefreshTokenAsync(account).Wait();
        }
    }
}
