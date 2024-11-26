using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Filters;
using PeopleManager.Dto.Requests;
using PeopleManager.Services;
using Vives.Services.Model;

namespace PeopleManager.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OrganizationsController(OrganizationService organizationService) : ControllerBase
    {
        private readonly OrganizationService _organizationService = organizationService;

        //Find (more) GET
        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] Paging paging, [FromQuery] OrganizationFilter? filter)
        {
            var organizations = await _organizationService.Find(paging, filter);
            return Ok(organizations);
        }

        //Get (one) GET
        [HttpGet("{id:int}", Name = "GetOrganizationRoute")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _organizationService.Get(id);
            return Ok(result);
        }

        //Create POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrganizationRequest request)
        {
            var result = await _organizationService.Create(request);
            if (result.IsSuccess && result.Data is not null)
            {
                return CreatedAtRoute("GetOrganizationRoute", new { id = result.Data.Id }, result);
            }

            return Ok(result);
        }

        //Update PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OrganizationRequest request)
        {
            var result = await _organizationService.Update(id, request);
            return Ok(result);
        }

        //Delete DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _organizationService.Delete(id);
            return Ok(result);
        }

    }
}
