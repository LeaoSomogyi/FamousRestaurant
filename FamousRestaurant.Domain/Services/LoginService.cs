using FamousRestaurant.Domain.Configurations;
using FamousRestaurant.Domain.Contracts;
using FamousRestaurant.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FamousRestaurant.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> _repository;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly SigningConfigurations _signingConfigurations;

        public LoginService(IRepository<User> repository, TokenConfigurations tokenConfigurations,
            SigningConfigurations signingConfigurations)
        {
            _repository = repository;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
        }

        public async Task<Token> DoLogin(User user)
        {
            IEnumerable<User> users = await _repository.SearchAsync(User.RetrieveLoginExpression(user));

            if (users != null && users.Count() > 0)
            {
                User _user = users.FirstOrDefault();

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(_user.Id.ToString(), "Login"),
                    new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, _user.Name)
                    }
                );

                DateTime createdAt = DateTime.Now;
                DateTime expiresAt = createdAt.AddSeconds(_tokenConfigurations.Seconds);

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor()
                {
                    Issuer = null,
                    Audience = null,
                    Subject = identity,
                    NotBefore = createdAt,
                    Expires = expiresAt,
                    SigningCredentials = _signingConfigurations.SigningCredentials
                });

                string token = handler.WriteToken(securityToken);

                return new Token()
                {
                    AccessToken = token,
                    Authenticated = true,
                    Created = createdAt,
                    Expiration = expiresAt
                };
            }
            else
            {
                return null;
            }
        }
    }
}
