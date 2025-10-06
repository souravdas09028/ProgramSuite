using Application.Contracts.Services;
using Blazored.LocalStorage;

namespace WebApp.Services
{
    public class LocalTokenService : ITokenService
    {
        private readonly ILocalStorageService _storage;
        public LocalTokenService(ILocalStorageService storage) => _storage = storage;
        public Task<string?> GetTokenAsync() => _storage.GetItemAsync<string>("authToken").AsTask();
        public Task SetTokenAsync(string token) => _storage.SetItemAsync("authToken", token).AsTask();
        public Task RemoveTokenAsync() => _storage.RemoveItemAsync("authToken").AsTask();
    }
}
