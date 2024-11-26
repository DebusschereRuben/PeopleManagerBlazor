using Vives.Presentation.Authentication;

namespace PeopleManager.Ui.Mvc.Stores
{
    public class BearerTokenStore(IHttpContextAccessor httpContextAccessor) : IBearerTokenStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private const string CookieName = "BearerToken";


        public string GetToken()
        {
            if (_httpContextAccessor.HttpContext is not null &&
                _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(CookieName))
            {
                return _httpContextAccessor.HttpContext.Request.Cookies[CookieName] ?? string.Empty;
            }

            return string.Empty;
        }

        public void SetToken(string token)
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return;
            }

            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(CookieName))
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieName);
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieName, token, new CookieOptions { HttpOnly = true });
        }
    }
}
