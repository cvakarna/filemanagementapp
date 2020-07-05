using FileManagementApp.Models.Configuration;
using FileManagementApp.Models.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementApp.Services.Repository
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly AppSettings _appSettings;
        //static Users
        private List<User> _users = new List<User> { 
            new User { Email = "ks@gmail.com", Password = "test@123", UserId = "613958dc-bd9f-11ea-b3de-0242ac130004",Username="Siva Karna" },
            new User { Email = "nirmala@gmail.com", Password = "test@123", UserId = "513958da-bd9f-31ea-b3de-0242ac130005",Username="Niramala" },
            new User { Email = "raj@gmail.com", Password = "test@123", UserId = "413958da-6d9f-31ea-b3de-0242ac130004",Username="Raj" },
            new User { Email = "user@gmail.com", Password = "test@123", UserId = "413958da-6d9f-31ea-b3de-0242ac130004",Username="User" }
        };
        public UserAuthenticationService(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }
        public UserAuthenticateResponse AuthenticateUser(UserAuthenticateRequest model)
        {
            var user = this._users.SingleOrDefault(user => user.Email == model.Email && user.Password == model.Password);//return only one element if its matched other wise default value of model
            if (user == null)//return null if user not found or wrong credentials
            {
                return null;
            }

            var token = GenerateJwtToken(user);
            return new UserAuthenticateResponse(user, token);
        }

        private string GenerateJwtToken(User user)
        {
            //generate the token valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_appSettings.Secret); //reading from config file
            //token descriptor
            var tokenDiscriptor = new SecurityTokenDescriptor {

                Subject = new System.Security.Claims.ClaimsIdentity(new System.Security.Claims.Claim[]
                {
                    new System.Security.Claims.Claim(ClaimTypes.Name, user.UserId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDiscriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
