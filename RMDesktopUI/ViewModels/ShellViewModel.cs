using System.Threading;
using System.Threading.Tasks;

using Caliburn.Micro;

using RMDesktopUILibrary.Helpers;
using RMDesktopUILibrary.Models;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel:Conductor<object>,IHandle<LogOnEventModel>
    {
        private ILoggedInUserModel loggedInUser;
        private IAPIHelper apiHelper;
        private readonly SalesViewModel salesVm;
        private readonly IEventAggregator eventAgregator;

        public ShellViewModel(IEventAggregator events,SalesViewModel salesViewModel,ILoggedInUserModel user,IAPIHelper apiHelper)
        {
            loggedInUser = user;
            salesVm = salesViewModel;
            eventAgregator = events;
            eventAgregator.Subscribe(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>()).Wait();
            this.apiHelper = apiHelper;
        }

        public async Task HandleAsync(LogOnEventModel message, CancellationToken cancellationToken)
        {
            ActivateItemAsync(salesVm).Wait();
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void ExitApplication()
        {
            TryCloseAsync();
        }

        public void UserManagement()
        {
            ActivateItemAsync(IoC.Get<UserDisplayViewModel>()).Wait();
        }

        public void LogOut()
        {
            loggedInUser.ResetModel();
            apiHelper.LogOffUser();
            ActivateItemAsync(IoC.Get<LoginViewModel>()).Wait();
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public bool IsLoggedIn
        {
            get
            {
                bool output = false;
                if (string.IsNullOrWhiteSpace(loggedInUser.Token) == false)
                {
                    output = true;
                }
                return output;
            }
        }
    }
}
