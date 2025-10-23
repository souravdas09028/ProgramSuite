using Application.Contracts.Services;
using Blazored.LocalStorage;

namespace WebApp.Services
{
    public class LocalTokenService : ITokenService
    {
        private const string Key = "authToken";
        private readonly ILocalStorageService _storage;

        public LocalTokenService(ILocalStorageService storage) => _storage = storage;
        public Task<string?> GetTokenAsync() => _storage.GetItemAsync<string>(Key).AsTask();
        public Task SetTokenAsync(string token) => _storage.SetItemAsync(Key, token).AsTask();
        public Task RemoveTokenAsync() => _storage.RemoveItemAsync(Key).AsTask();
    }
}
