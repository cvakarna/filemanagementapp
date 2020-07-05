using FileManagementApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Services
{
    public interface IUserAuthenticationService
    {
        UserAuthenticateResponse AuthenticateUser(UserAuthenticateRequest model);
    }
}
