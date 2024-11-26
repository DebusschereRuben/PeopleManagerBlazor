using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using Vives.Services.Model;

namespace PeopleManager.Sdk
{
    public class IdentitySdk(IHttpClientFactory httpClientFactory)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<AuthenticationResult> SignIn(SignInRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");

            var route = "Identity/sign-in";
            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
            if (result is null)
            {
                return new AuthenticationResult();
            }

            return result;
        }

        public async Task<AuthenticationResult> Register(RegisterRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");

            var route = "Identity/register";
            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
            if (result is null)
            {
                return new AuthenticationResult();
            }

            return result;
        }
    }
}
