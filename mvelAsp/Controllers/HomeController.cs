using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvelAsp.Models;

namespace mvelAsp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
