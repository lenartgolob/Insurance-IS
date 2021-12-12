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
        return View();
    }

    public IActionResult Admin()
    {
        ViewBag.Data = "Value,Value1,Value2,Value3"; //list of strings that you need to show on the chart. as mentioned in the example from c-sharpcorner
        ViewBag.ObjectName = "Test,Test1,Test2,Test3";
        return View();
    }

    public IActionResult Agent()
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
