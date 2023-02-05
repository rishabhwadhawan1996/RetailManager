using System;

namespace RMDesktopUILibrary.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Token { get; set; }
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime CreatedDate { get; set; }

        public void ResetModel()
        {
            Token = "";
            Id = "";
            FirstName = "";
            LastName = "";
            Address = "";
            CreatedDate = DateTime.Now;
        }
    }
}
