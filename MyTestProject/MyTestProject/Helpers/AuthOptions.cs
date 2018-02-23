using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProject.Helpers
{
    public class AuthOptions
    {
        public const string ISSUER = "MyTokenCreator"; // token publisher
        public const string AUDIENCE = "http://localhost:59046/"; // token consumer
        const string KEY = "mysupersecret_secretkey!123";   // key
        public const int LIFETIME = 20; // token lifetime
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
