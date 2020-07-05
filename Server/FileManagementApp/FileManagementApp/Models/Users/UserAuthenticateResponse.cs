using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Models.Users
{
    public class UserAuthenticateResponse
    {
        public string  UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public UserAuthenticateResponse(User user, string token)
        {
            UserId = user.UserId;
            Username = user.Username;
            Email = user.Email;
            Token = token;
        }
    }
}
