using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Caliburn.Micro;

using RMDesktopUILibrary.API;

namespace RMDesktopUI.ViewModels
{
    public class UserDisplayViewModel : Screen
    {
        private IUserEndpoint userEndpoint;
        private StatusInfoViewModel status;
        private IWindowManager dialog;

        BindingList<UserModel> users;

        public BindingList<UserModel> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        private UserModel selectedUser;

        public UserModel SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                selectedUser = value;
                SelectedUserName = value.Email;
                UserRoles.Clear();
                UserRoles = new BindingList<string>(value.
                    Roles.Select(x => x.Value).ToList());
                LoadRoles();
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        public BindingList<string> userRoles = new BindingList<string>();

        public BindingList<string> UserRoles
        {
            get
            {
                return userRoles;
            }
            set
            {
                userRoles = value;
                NotifyOfPropertyChange(() => UserRoles);
            }
        }

        public BindingList<string> availableRoles = new BindingList<string>();

        public BindingList<string> AvailableRoles
        {
            get
            {
                return availableRoles;
            }
            set
            {
                availableRoles = value;
                NotifyOfPropertyChange(() => AvailableRoles);
            }
        }

        private string selectedUserRole;

        public string SelectedUserRole
        {
            get { return selectedUserRole; }
            set
            {
                selectedUserRole = value;
                NotifyOfPropertyChange(() => SelectedUserRole);
            }
        }

        private string selectedAvailableRole;

        public string SelectedAvailableRole
        {
            get 
            { 
                return selectedAvailableRole;
            }
            set
            {
                selectedAvailableRole = value;
                NotifyOfPropertyChange(() => SelectedAvailableRole);
            }
        }


        private string selectedUserName;

        public string SelectedUserName
        {
            get { return selectedUserName; }
            set
            {
                selectedUserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }


        public UserDisplayViewModel(IUserEndpoint userEndpoint, StatusInfoViewModel status, IWindowManager window)
        {
            this.userEndpoint = userEndpoint;
            this.status = status;
            dialog = window;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadProducts();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";
                if (ex.Message.ToLower() == "unauthorized")
                {
                    this.status.UpdateMessage("Unauthrozied Access", "Please check if access permissions are there for accessing Sales form");
                    await dialog.ShowDialogAsync(this.status, null, settings);
                }
                else
                {
                    this.status.UpdateMessage("Fatal Exception", ex.Message);
                    await dialog.ShowDialogAsync(this.status, null, settings);
                }

                await TryCloseAsync();
            }
        }

        private async Task LoadProducts()
        {
            var usersList = await userEndpoint.GetAll();
            Users = new BindingList<UserModel>(usersList);
        }

        private async Task LoadRoles()
        {
            var roles = await userEndpoint.GetAllRoles();
            foreach (var role in roles)
            {
                if (UserRoles.IndexOf(role.Value) < 0)
                {
                    AvailableRoles.Add(role.Value);
                }
            }
        }

        public async void AddSelectedRole()
        {
            await userEndpoint.AddUserToRole(SelectedUser.Id, SelectedAvailableRole);
            userRoles.Add(SelectedAvailableRole);
            AvailableRoles.Remove(SelectedAvailableRole);
        }

        public async void RemoveSelectedRole()
        {
            await userEndpoint.RemoveUserFromRole(SelectedUser.Id, SelectedUserRole);
            userRoles.Remove(SelectedUserRole);
            AvailableRoles.Add(SelectedUserRole);
        }
    }
}
