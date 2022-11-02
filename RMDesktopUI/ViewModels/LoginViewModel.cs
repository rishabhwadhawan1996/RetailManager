﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Caliburn.Micro;

using RMDesktopUI.Helpers;

using RMDesktopUILibrary.Helpers;

namespace RMDesktopUI.ViewModels
{
    public class LoginViewModel:Screen
    {
        public LoginViewModel(IAPIHelper ApiHelper)
        {
            apiHelper = ApiHelper;
        }

        private string username;
        private string password;
        private IAPIHelper apiHelper;

        public string Username       
        {
            get 
            { 
                return username;
            }
            set 
            {
                username = value;
                NotifyOfPropertyChange(nameof(Username));
                NotifyOfPropertyChange(()=> CanLogIn);
            }
        }

        public string Password
        {
            get { return password; }
            set 
            {
                password = value;
                NotifyOfPropertyChange(nameof(Password));
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool IsErrorVisible
        {
            get 
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set 
            {
                errorMessage = value;
                NotifyOfPropertyChange(nameof(ErrorMessage));
                NotifyOfPropertyChange(nameof(IsErrorVisible));
            }
        }


        public bool CanLogIn
        {
            get{
                bool output = false;
                if (username?.Length > 0 && password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task LogIn()
        {
            try
            {
                var result = await apiHelper.Authenticate(Username, Password);
                await apiHelper.GetLoggedInUserInfo(result.AccessToken);
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
