using Auth.Application.Configuration;
using Auth.Domain.Data.Entities;
using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Services.TokenGeneration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Application.JWT
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly SymmetricSecurityKey _securityKey;
        private readonly string _audience;
        private readonly string _issuer;
        private readonly int _expireInMinutes;

        public TokenGenerator(IServiceConfiguration configuration)
        {
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JwtSecret));
            _audience = configuration.JwtAudience;
            _issuer = configuration.JwtIssuer;
            _expireInMinutes = configuration.JwtExpireInMinutes;
        }

        public Token GenerateFor(User user, UserAuthentication userAuthentication)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Username.Value));
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email.Value));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Role, user.Role.Value));
            claims.AddClaim(new Claim(ClaimTypes.Sid, userAuthentication.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.GivenName, $"{user.Firstname} {user.Lastname}"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor()
            {
                Audience = _audience,
                Issuer = _issuer,
                Subject = claims,
                SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(_expireInMinutes),
                NotBefore = DateTime.UtcNow
            };
            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

            return new Token(tokenHandler.WriteToken(securityToken), descriptor.Expires.Value, _expireInMinutes * 60 * 1000);
        }
    }
}
