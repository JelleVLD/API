using API.Helpers;
using API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings  _appSettings;
        private readonly APIContext  _apiContext;
        public UserService(IOptions<AppSettings> appSettings, APIContext apiContext)
        {
            _appSettings = appSettings.Value;
            _apiContext = apiContext;
        }
        public Gebruiker Authenticate(string username, string password)
        {
            var user = _apiContext.Gebruikers.SingleOrDefault(x => x.gebruikersnaam == username && x.wachtwoord == password);
            if(user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[ ]
            {
                new Claim("gebruikerID", user.gebruikerID.ToString()),
                new Claim("email", user.email),
                new Claim("gebruikersnaam", user.gebruikersnaam)
            }
            ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.wachtwoord = null;
            return user;
        }
        }
}
