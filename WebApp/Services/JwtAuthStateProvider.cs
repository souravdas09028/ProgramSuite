using Application.Contracts.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace WebApp.Services
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ITokenService _tokenService;

        public JwtAuthStateProvider(ILocalStorageService localStorage, ITokenService tokenService)
        {
            _localStorage = localStorage;
            _tokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void NotifyAuthStateChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            string json = Base64UrlDecode(payload);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var claims = new List<Claim>();

            foreach (var prop in root.EnumerateObject())
            {
                if (prop.NameEquals("role"))
                {
                    if (prop.Value.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var r in prop.Value.EnumerateArray())
                            claims.Add(new Claim(ClaimTypes.Role, r.GetString()!));
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, prop.Value.GetString()!));
                    }
                }
                else
                {
                    claims.Add(new Claim(prop.Name, prop.Value.ToString()!));
                }
            }
            return claims;
        }

        private static string Base64UrlDecode(string input)
        {
            string s = input.Replace('-', '+').Replace('_', '/');
            switch (s.Length % 4)
            {
                case 2: s += "=="; break;
                case 3: s += "="; break;
            }
            var bytes = Convert.FromBase64String(s);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
