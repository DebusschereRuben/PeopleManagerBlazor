using System.Net.Http.Json;
using PeopleManager.Dto.Filters;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Sdk.Extensions;
using Vives.Presentation.Authentication;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;

namespace PeopleManager.Sdk
{
    public class PersonSdk(IHttpClientFactory httpClientFactory, IBearerTokenStore tokenStore)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IBearerTokenStore _tokenStore = tokenStore;

        //Find
        public async Task<PagedServiceResult<PersonResult, PersonFilter>> Find(Paging paging, PersonFilter? filter = null, Sorting? sorting = null)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");

            var route = $"People?paging.offset={paging.Offset}&paging.limit={paging.Limit}";
            if (filter is not null)
            {
                route = $"{route}&filter.Search={filter.Search}&filter.OrganizationId={filter.OrganizationId}";
            }

            if (sorting is not null)
            {
                route =
                    $"{route}&sorting.PropertyName={sorting.PropertyName}&sorting.IsDescending={sorting.IsDescending}";
            }

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PagedServiceResult<PersonResult, PersonFilter>>();
            if (result is null)
            {
                return new PagedServiceResult<PersonResult, PersonFilter>
                {
                    Data = new List<PersonResult>(), 
                    Paging = paging,
                    Filter = filter,
                    Sorting = sorting
                };
            }

            return result;
        }

        //Get
        public async Task<ServiceResult<PersonResult>> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");

            var route = $"People/{id}";
            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();
            if (result is null)
            {
                return new ServiceResult<PersonResult>();
            }

            return result;
        }

        //Create
        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");

            var route = "People";
            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();
            if (result is null)
            {
                return new ServiceResult<PersonResult>();
            }

            return result;
        }

        //Update
        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");

            var route = $"People/{id}";
            var response = await httpClient.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();
            if (result is null)
            {
                result = new ServiceResult<PersonResult>();
                result.NotFound(nameof(PersonResult), id);
            }

            return result;
        }

        //Delete
        public async Task<ServiceResult<PersonResult>> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");

            var route = $"People/{id}";
            var response = await httpClient.DeleteAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();
            if (result is null)
            {
                return new ServiceResult<PersonResult>();
            }

            return result;
        }
    }
}
