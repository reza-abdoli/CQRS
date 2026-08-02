using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using UI.Contracts;
using UI.Services.Base;

namespace UI.Services
{
    public class AuthenticateService : BaseHttpService, IAuthenticateService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AuthenticateService(IClient client, ILoacalStorageService localStorage, IHttpContextAccessor httpContextAccessor)
            : base(client, localStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }
        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest authenticateRequest = new()
                {
                    Email = email,
                    Password = password
                };
                var authenticateResponse = await _client.LoginAsync(authenticateRequest);
                if (authenticateResponse.Token != string.Empty)
                {
                    var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(authenticateResponse.Token);
                    var claims = ParseClaims(tokenContent);

                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme));

                    await _httpContextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, user);

                    _localStorageService.SetStorageValue("token", authenticateResponse.Token);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task Logout()
        {
            _localStorageService.ClearStorageValue(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Register(string firstName, string lastName, string email, string password, string userName)
        {
            RegistrationRequest registrationRequest = new()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Password = password
            };

            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
                return true;

            return false;
        }
        private IList<Claim> ParseClaims(JwtSecurityToken token)
        {
            var claims = token.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, token.Subject));
            return claims;
        }
    }
}