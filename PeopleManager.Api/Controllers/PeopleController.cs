﻿using Microsoft.AspNetCore.Authorization;
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
    public class PeopleController(PersonService personService) : ControllerBase
    {
        private readonly PersonService _personService = personService;

        //Find (more) GET
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Find([FromQuery]Paging paging, [FromQuery]PersonFilter? filter, [FromQuery]Sorting? sorting)
        {
            var result = await _personService.Find(paging, filter, sorting);
            return Ok(result);
        }

        //Get (one) GET
        [AllowAnonymous]
        [HttpGet("{id:int}", Name="GetPersonRoute")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var result = await _personService.Get(id);
            return Ok(result);
        }

        //Create POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PersonRequest request)
        {
            var result = await _personService.Create(request);
            if (result.IsSuccess && result.Data != null)
            {
                return CreatedAtRoute("GetPersonRoute", new { id = result.Data.Id }, result);
            }
            return Ok(result);
        }

        //Update PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]PersonRequest request)
        {
            var result = await _personService.Update(id, request);
            return Ok(result);
        }

        //Delete DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _personService.Delete(id);
            return Ok(result);
        }

    }
}
