using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Caliburn.Micro;

using RMDesktopUI.Helpers;

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
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
