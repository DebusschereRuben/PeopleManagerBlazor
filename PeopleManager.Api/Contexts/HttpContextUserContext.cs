using Vives.Services.Model.Contracts;

namespace PeopleManager.Api.Contexts
{
    public class HttpContextUserContext: IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextUserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            if (_httpContextAccessor.HttpContext is null 
                || !_httpContextAccessor.HttpContext.User.HasClaim(c => c.Type == "Id"))
            {
                return string.Empty;
            }

            return _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "Id").Value;
        }
    }
}
