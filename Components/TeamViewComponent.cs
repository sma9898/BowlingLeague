using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Components
{
    //Set up data for view component
    public class TeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;

        //Constructor
        public TeamViewComponent (BowlingLeagueContext ctx)
        {
            context = ctx;
        }
        //Pass information
        public IViewComponentResult Invoke()
        {
            return View(context.Teams
                //.Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x)
                //.ToList()
                );
        }
    }
}
