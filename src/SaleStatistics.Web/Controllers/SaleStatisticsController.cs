using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaleStatistics.Web.Models;

namespace SaleStatistics.Web.Controllers
{
    public class SaleStatisticsController : Controller
    {
        private readonly ILogger<SaleStatisticsController> _logger;

        public SaleStatisticsController(ILogger<SaleStatisticsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var dummy = new SaleStatisticsViewModel
            {
                TopTenAgentsWithSalesObjects = new List<SaleStatisticsItem>
                {
                    new SaleStatisticsItem
                    {
                        Agent = "Agent 1",
                        Count = 10
                    },
                     new SaleStatisticsItem
                    {
                        Agent = "Agent 2",
                        Count = 8
                    },
                      new SaleStatisticsItem
                    {
                        Agent = "Agent 3",
                        Count = 4
                    },
                       new SaleStatisticsItem
                    {
                        Agent = "Agent 5",
                        Count = 1
                    }
                },

                TopTenAgentsWithSalesObjectsAndGardens = new List<SaleStatisticsItem>
                {
                    new SaleStatisticsItem
                    {
                        Agent = "Agent 1",
                        Count = 6
                    },
                     new SaleStatisticsItem
                    {
                        Agent = "Agent 2",
                        Count = 5
                    },
                      new SaleStatisticsItem
                    {
                        Agent = "Agent 3",
                        Count = 4
                    },
                       new SaleStatisticsItem
                    {
                        Agent = "Agent 5",
                        Count = 3
                    }
                }
            };

            return View(dummy);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
