using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using RMDesktopUILibrary.Models;

namespace RMDesktopUILibrary.Helpers
{
    public class APIHelper : IAPIHelper
    {
        private ILoggedInUserModel loggedInUserModel;

        private HttpClient apiClient { get; set; }

        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
            loggedInUserModel = loggedInUser;
        }

        private void InitializeClient()
        {
            string apiBasePath = ConfigurationManager.AppSettings["Api"];
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(apiBasePath);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("password",password)
            });

            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task GetLoggedInUserInfo(string token)
        {
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");
            using(HttpResponseMessage response = await apiClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    loggedInUserModel.Address = result.Address;
                    loggedInUserModel.CreatedDate = result.CreatedDate;
                    loggedInUserModel.FirstName = result.FirstName;
                    loggedInUserModel.Id = result.Id;
                    loggedInUserModel.LastName = result.LastName;
                    loggedInUserModel.Token = result.Token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
