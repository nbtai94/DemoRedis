using DemoRedis.Models;
using DemoRedis.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text.Json.Serialization;

namespace DemoRedis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRedisService _redisService;

        public HomeController(ILogger<HomeController> logger, IRedisService redisService)
        {
            _logger = logger;
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            var user = new User { Id = 2,Name = "PhuongMai" };
            var id = 1;
            _redisService.SetValue($"UserId:{id}", user);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetCache()
        {
            var data = _redisService.GetValue<User>($"UserId:{1}");
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCache()
        {
            _redisService.RemoveCache("Name:1");
            return Redirect("https://google.com");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
