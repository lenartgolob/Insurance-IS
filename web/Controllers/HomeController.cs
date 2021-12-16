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

    public async Task<IActionResult> Admin()
    {
        List<string> subtypes = new List<string>();
        List<int> subtypeInstances = new List<int>();
        var query = from s in _context.InsuranceSubtype
                    select s;
        foreach(InsuranceSubtype subtype in query){
            var query2 = from p in _context.InsurancePolicy
                     where p.InsuranceSubtypeID == subtype.InsuranceSubtypeID
                     select p;
            int instances = query2.ToList().Count;
            if(instances >= 1){
                subtypes.Add(subtype.Title);
                subtypeInstances.Add(instances);
            }
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

        var queryPolicies = from p in _context.InsurancePolicy
            select p;
        
        var users = await _usermanager.Users.ToListAsync();
        Dictionary<string, List<decimal>> userSumByTypeDict = new Dictionary<string, List<decimal>>();
        foreach(ApplicationUser user in users){
            List<decimal> userSumByTypeList = new List<decimal>();
            foreach(InsuranceType type in queryTypes){
                decimal userSumByType = 0;
                foreach(InsuranceSubtype subtype in query){
                    if(type.InsuranceTypeID == subtype.InsuranceTypeID){
                        foreach(InsurancePolicy policy in queryPolicies){
                            if(policy.InsuranceSubtypeID == subtype.InsuranceSubtypeID && user.Id == policy.OwnerID){
                                userSumByType = userSumByType + policy.FinalSum;
                            }
                        }
                    }
                }
                userSumByTypeList.Add(userSumByType);
            }
            string fullName = user.FirstName + " " + user.LastName;
            userSumByTypeDict.Add(fullName, userSumByTypeList);
        }
        ViewData["userSumByType"] = Newtonsoft.Json.JsonConvert.SerializeObject(userSumByTypeDict);
        ViewData["users"] = Newtonsoft.Json.JsonConvert.SerializeObject(users);
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
