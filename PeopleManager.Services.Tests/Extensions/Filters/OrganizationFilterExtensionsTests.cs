using PeopleManager.Dto.Filters;
using PeopleManager.Dto.Results;
using PeopleManager.Services.Extensions.Filters;
using Vives.Services.Model;

namespace PeopleManager.Services.Tests.Extensions.Filters
{
    public class OrganizationFilterExtensionsTests
    {
        [Fact]
        public void Filter_NumberOfPeopleBetweenToIncluded_Returns_People()
        {
            //Arrange
            var query = new List<OrganizationResult>
            {
                new OrganizationResult { Name = "Organization1", NumberOfMembers = 4 },
                new OrganizationResult { Name = "Organization2", NumberOfMembers = 5 },
                new OrganizationResult { Name = "Organization3", NumberOfMembers = 6 }
            }.AsQueryable();

            var filter = new OrganizationFilter
            {
                NumberOfPeopleBetween = new Between<int>
                {
                    From = 5,
                    IncludeFrom = true
                }
            };
            //Act
            var result = query.ApplyFilter(filter).ToList();

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Organization2", result.First().Name);
            Assert.Equal("Organization3", result.Skip(1).First().Name);
        }

        [Fact]
        public void Filter_NumberOfPeopleBetweenToIncludedFalse_Returns_People()
        {
            //Arrange
            var query = new List<OrganizationResult>
            {
                new OrganizationResult { Name = "Organization1", NumberOfMembers = 4 },
                new OrganizationResult { Name = "Organization2", NumberOfMembers = 5 },
                new OrganizationResult { Name = "Organization3", NumberOfMembers = 6 }
            }.AsQueryable();

            var filter = new OrganizationFilter
            {
                NumberOfPeopleBetween = new Between<int>
                {
                    From = 5,
                    IncludeFrom = false
                }
            };
            //Act
            var result = query.ApplyFilter(filter).ToList();

            //Assert
            Assert.Single(result);
            Assert.Equal("Organization3", result.First().Name);
        }
    }
}
