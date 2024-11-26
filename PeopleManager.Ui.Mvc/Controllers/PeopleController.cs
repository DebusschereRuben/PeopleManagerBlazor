using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Filters;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Sdk;
using Vives.Services.Model;

namespace PeopleManager.Ui.Mvc.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly PersonSdk _personSdk;
        private readonly OrganizationSdk _organizationSdk;

        public PeopleController(
            PersonSdk personSdk,
            OrganizationSdk organizationSdk)
        {
            _personSdk = personSdk;
            _organizationSdk = organizationSdk;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]Paging paging, [FromQuery]PersonFilter? filter = null, [FromQuery]Sorting? sorting = null)
        {
            var result = await _personSdk.Find(paging, filter, sorting);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await CreateEditView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                var person = new PersonResult
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email
                };

                return await CreateEditView(person);
            }

            await _personSdk.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var result = await _personSdk.Get(id);

            if (!result.IsSuccess || result.Data is null)
            {
                return RedirectToAction("Index");
            }

            return await CreateEditView(result.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                var result = await _personSdk.Get(id);
                if (!result.IsSuccess || result.Data is null)
                {
                    return RedirectToAction("Index");
                }

                var person = result.Data;
                person.FirstName = request.FirstName;
                person.LastName = request.LastName;
                person.Email = request.Email;

                return await CreateEditView(person);
            }

            await _personSdk.Update(id, request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var person = await _personSdk.Get(id);

            return View(person.Data);
        }

        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personSdk.Delete(id);

            return RedirectToAction("Index");
        }
        
        private async Task<IActionResult> CreateEditView(PersonResult? person = null)
        {
            var paging = new Paging { Offset = 0, Limit = 1000 };
            var organizations = await _organizationSdk.Find(paging);
            ViewBag.Organizations = organizations;
            return View(person);
        }
    }
}
