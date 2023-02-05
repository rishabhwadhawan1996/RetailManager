using System;

namespace RMDesktopUILibrary.Models
{
    public interface ILoggedInUserModel
    {
        string Token { get; set; }
        string Address { get; set; }
        DateTime CreatedDate { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        string LastName { get; set; }

        void ResetModel();
    }
}