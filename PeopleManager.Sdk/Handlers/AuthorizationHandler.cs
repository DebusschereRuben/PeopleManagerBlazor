using PeopleManager.Sdk.Extensions;
using Vives.Presentation.Authentication;

namespace PeopleManager.Sdk.Handlers
{
    public class AuthorizationHandler(IBearerTokenStore tokenStore): DelegatingHandler
    {
        private readonly IBearerTokenStore _tokenStore = tokenStore;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _tokenStore.GetToken();
            request.Headers.AddAuthorization(token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
