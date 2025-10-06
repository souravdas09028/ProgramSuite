using Application.Contracts.Services;
using System.Net.Http.Headers;

namespace Ui.Shared.Services
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;
        public JwtAuthorizationHandler(ITokenService tokenService) => _tokenService = tokenService;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
