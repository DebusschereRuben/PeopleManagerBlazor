using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Models;
using System.Diagnostics;
using PeopleManager.Sdk;
using Vives.Services.Model;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonSdk _personSdk;

        public HomeController(PersonSdk personSdk)
        {
            _personSdk = personSdk;
        }

        public async Task<IActionResult> Index([FromQuery]Paging paging)
        {
            var result = await _personSdk.Find(paging);
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
