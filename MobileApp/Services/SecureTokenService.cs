using Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public class SecureTokenService : ITokenService
    {
        private const string Key = "authToken";
        public async Task<string?> GetTokenAsync() => await SecureStorage.GetAsync(Key);
        public async Task SetTokenAsync(string token) => await SecureStorage.SetAsync(Key, token);
        public async Task RemoveTokenAsync() => SecureStorage.Remove(Key);
    }
}
