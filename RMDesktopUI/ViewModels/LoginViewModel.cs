using System;
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
        public LoginViewModel(IAPIHelper ApiHelper,IEventAggregator events)
        {
            apiHelper = ApiHelper;
            eventAgregator = events;
        }

        private string username="rishabhwadhawan1996@gmail.com";
        private string password="Rishabh@99";
        private IAPIHelper apiHelper;
        private IEventAggregator eventAgregator;

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
                await apiHelper.GetLoggedInUserInfo(result.Access_Token);
                await eventAgregator.PublishOnUIThreadAsync(new LogOnEventModel());
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
