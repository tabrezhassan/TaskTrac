using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml;
using TaskTrac.API.Interfaces;
using TaskTrac.DAL.Models;

namespace TaskTrac.API.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        //Constructor that takes IConfiguration as a dependacy
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Method that generates JWT token asynchronously
        public async Task<string> GenerateJwtToken(Users users, IList<string> roles)
        {
            //Create a list to store claims for the token
            var authClaim = new List<Claim>
            {
                //Adds email claim to indentify the user
                new Claim(ClaimTypes.Name, users.UserName),
                new Claim(ClaimTypes.NameIdentifier,users.Id),

                //Addunique identifier claim for JWT
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //Adds role claims to the list of claims
            foreach(var role in roles) 
            { 
                authClaim.Add(new Claim(ClaimTypes.Role, role));
            }

            //Retrieve the secret key to sign the token.
            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            //Create JWT token with required parameters
            var token = new JwtSecurityToken(

                //Issuer of the token
                issuer: _configuration["JWT:ValidIssuer"],

                //Audience - for who the token is intended.
                audience: _configuration["JWT:ValidAudience"],

                //Expiration time of the token
                expires: DateTime.Now.AddHours(5),

                //Includes claims in the token
                claims: authClaim,

                //Signing credentials for token validation
                signingCredentials:new SigningCredentials(authSignKey,SecurityAlgorithms.HmacSha256)
                );

            //Writes the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
