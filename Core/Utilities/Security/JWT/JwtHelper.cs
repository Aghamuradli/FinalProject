using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT   ///Kamp 14
{
       public class JwtHelper : ITokenHelper
        {
            public IConfiguration Configuration { get; }
            private TokenOptions _tokenOptions;
            private DateTime _accessTokenExpiration;
            public JwtHelper(IConfiguration configuration)
            {
                Configuration = configuration;
                _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            }
            public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
            {
                _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); //Hemen andan sonra 10 deq islesin
                var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); //tokendeki sec.key ile tezesini yaratamaq
                var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); //Hansi alqoritm ve acar hansidir?
                var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); //Her birinden istifade ederem method yaradiriq 
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var token = jwtSecurityTokenHandler.WriteToken(jwt);

                return new AccessToken
                {
                    Token = token,
                    Expiration = _accessTokenExpiration
                };

            }

            public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, 
                SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
            {
                var jwt = new JwtSecurityToken(  ///Yeni token yaradir .
                    issuer: tokenOptions.Issuer,
                    audience: tokenOptions.Audience,
                    expires: _accessTokenExpiration,
                    notBefore: DateTime.Now,
                    claims: SetClaims(user, operationClaims),
                    signingCredentials: signingCredentials
                );
                return jwt;
            }

            private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims) //Istifadecini melumatlari 
            {
                var claims = new List<Claim>();
                claims.AddNameIdentifier(user.Id.ToString());
                claims.AddEmail(user.Email);
                claims.AddName($"{user.FirstName} {user.LastName}");
                claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

                return claims;
            }
        }
    }
