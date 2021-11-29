using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web.Models;

namespace web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // string path = Server.MapPath("~/Images/");
        // string[] imageFiles = Directory.GetFiles(path);
        // ViewBag.images = imageFiles;
        return View();
    }

    public IActionResult Admin()
    {
        return View();
    }

    public IActionResult Insure()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
