using Caliburn.Micro;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel:Conductor<object>
    {
        private readonly LoginViewModel loginViewModel;

        public ShellViewModel(LoginViewModel loginVM)
        {
            loginViewModel = loginVM;
            ActivateItemAsync(loginViewModel).Wait();
        }

    }
}
