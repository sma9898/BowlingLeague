using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; } //Context

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(long? teamnameid, int pageNum = 0)
        {
            int pageSize = 5; //5 names per page

            return View(context.Bowlers
                .Where(m => m.TeamId == teamnameid || teamnameid == null)
                .OrderBy(m => m.Team)
                .Skip((pageNum-1) * pageSize)
                .Take(pageSize)
                .ToList());
                /*
                .FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamnameid} OR {teamnameid} IS NULL") //Can use regular SQL queries
                .ToList()); 
                */

                /*Where(x => x.BowlerFirstName.Contains("Barbara"))
                .OrderBy(x => x.BowlerLastName)
                .ToList());*/
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
}
