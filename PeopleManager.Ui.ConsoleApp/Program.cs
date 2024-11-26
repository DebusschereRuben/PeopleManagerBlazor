using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleManager.Dto.Filters;
using PeopleManager.Sdk;
using PeopleManager.Sdk.Extensions;
using PeopleManager.Ui.ConsoleApp.Settings;
using Vives.Services.Model;

var configurationBuilder = new ConfigurationBuilder();
var services = new ServiceCollection();

configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = configurationBuilder.Build();

var apiSettings = new ApiSettings();
configuration.GetSection(nameof(ApiSettings)).Bind(apiSettings);
services.AddApi(apiSettings.BaseUrl);

var serviceProvider = services.BuildServiceProvider();

//Show Organizations
var organizationService = serviceProvider.GetRequiredService<OrganizationSdk>();

var paging = new Paging { Offset = 0, Limit = 1000 };
var organizations = await organizationService.Find(paging);

foreach (var organization in organizations)
{
    Console.WriteLine($"[{organization.Id}] {organization.Name} ({organization.NumberOfMembers} members)");
}

//Show People
var personService = serviceProvider.GetRequiredService<PersonSdk>();

var result = await personService.Find(paging, null);

if (result.IsSuccess && result.Data is not null)
{
    foreach (var person in result.Data)
    {
        Console.WriteLine($"[{person.Id}] {person.FirstName} {person.LastName} ({person.OrganizationName})");
    }
}

Console.ReadLine();
