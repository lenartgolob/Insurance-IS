using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using SelectPdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace web.Controllers;

public class HomeController : Controller
{
    private readonly InsuranceContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _usermanager;

    public HomeController(ILogger<HomeController> logger, InsuranceContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _logger = logger;
        _usermanager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Admin()
    {
        List<string> subtypes = new List<string>();
        List<int> subtypeInstances = new List<int>();
        var query = from s in _context.InsuranceSubtype
                    select s;
        foreach(InsuranceSubtype subtype in query){
            var query2 = from p in _context.InsurancePolicy
                     where p.InsuranceSubtypeID == subtype.InsuranceSubtypeID
                     select p;
            subtypes.Add(subtype.Title);
            subtypeInstances.Add(query2.ToList().Count);
        }
        ViewData["subtypes"] = subtypes;
        ViewData["subtypeInstances"] = subtypeInstances;

        List<string> insuranceTypes = new List<string>();
        var queryTypes = from s in _context.InsuranceType
                    select s;
        foreach(InsuranceType type in queryTypes){
            insuranceTypes.Add(type.Title);
        }
        ViewData["types"] = insuranceTypes;
        // List<string> insuranceAgents = new List<string>();
        // var queryAgents = from u in db.Users
        //             select u;
        // Console.WriteLine(queryAgents);
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
