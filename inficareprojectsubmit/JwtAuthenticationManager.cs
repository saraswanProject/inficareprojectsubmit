using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace inficareprojectsubmit
{
    public class JwtAuthenticationManager :IJwtAuthenticationManager
    {
        private readonly IDictionary<string, string> _jwt = new Dictionary<string, string>
        {
            {"bhuwan","bhuwan255" },
            {"inficare","inficare123" }
        };
        //when we will have database we will attached that database here
        private readonly string key;

        public JwtAuthenticationManager (string key)
        {
            this.key = key; 
        }

        public string Authenticate(string username,string password)
        {
            if(!_jwt.Any(x=>x.Key==username&&x.Value==password))
            {
                return "Authentication failed. Invalid username or password.";
                //return "1";
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
            
        }
    }
}
