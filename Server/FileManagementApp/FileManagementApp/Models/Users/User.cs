using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementApp.Models.Users
{
    public class User
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
