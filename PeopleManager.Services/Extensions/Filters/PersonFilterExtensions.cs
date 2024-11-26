using PeopleManager.Dto.Filters;
using PeopleManager.Dto.Results;
using PeopleManager.Model;

namespace PeopleManager.Services.Extensions.Filters
{
    public static class PersonFilterExtensions
    {
        public static IQueryable<PersonResult> ApplyFilter(this IQueryable<PersonResult> query, PersonFilter? filter)
        {
            if (filter is null)
            {
                return query;
            }

            if (filter.OrganizationId.HasValue)
            {
                query = query.Where(p => p.OrganizationId == filter.OrganizationId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var search = filter.Search.ToLowerInvariant();
                query = query.Where(p =>
                    p.FirstName.ToLowerInvariant().Contains(search)
                    || p.LastName.ToLowerInvariant().Contains(search)
                    || (p.Email != null && p.Email.ToLowerInvariant().Contains(search))
                    || (p.OrganizationName != null && p.OrganizationName.ToLowerInvariant().Contains(search))
                );
            }

            return query;
        }
    }
}
