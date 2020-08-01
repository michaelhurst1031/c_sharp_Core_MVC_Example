using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using MVC.Models;
using Newtonsoft.Json;


namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private LinkGenerator _linkGenerator;
        
        public HomeController(ILogger<HomeController> logger, LinkGenerator linkGenerator)
        {
            _logger = logger;
            _linkGenerator = linkGenerator;
        }
[HttpGet("")]
[HttpGet("Home")]
[HttpGet("Home/Index")]

        public IActionResult Index()
        {
            return View();
        }
[HttpGet("[action]")]
        public IActionResult Link() {
            //var link = _linkGenerator.GetPathByAction("Privacy","Home");
            var url = Url.RouteUrl("Products_List", new{},Request.Scheme);
            return Content(url);
        }

        private static User _User = new User() {
            Username = "MHurst",
            Fullname = "Michael Hurst",
            Password = "test"
        };

        [HttpGet("[action]",Name="Products_List")]
        public IActionResult Privacy()
        {
            var json = JsonConvert.SerializeObject(_User,Formatting.Indented);

            _logger.LogInformation(json);

            // throw new Exception("Broken");
        
            return View(_User);
        }

        [HttpPost("[action]")]
        public IActionResult Privacy(User user)
        {
            if (ModelState.IsValid) {
                _User.Fullname=user.Fullname;
                _User.Username=user.Username;
                _User.Password=user.Password;
                return RedirectToAction("Privacy");
            } else {
                return View(user);
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
