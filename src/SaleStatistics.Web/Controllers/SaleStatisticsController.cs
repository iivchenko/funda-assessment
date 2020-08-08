using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaleStatistics.Application.Queries.GetTopSaleObjects;
using SaleStatistics.Web.Models;

namespace SaleStatistics.Web.Controllers
{
    public class SaleStatisticsController : Controller
    {
        private const int TopCount = 10;
        private const string AmsterdamSalesFilter = "/amsterdam/";
        private const string AmsterdamWithGarenSalesFilter = "/amsterdam/tuin";

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<SaleStatisticsController> _logger;

        public SaleStatisticsController(
            IMapper mapper,
            IMediator mediator,
            ILogger<SaleStatisticsController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new SaleStatisticsViewModel()
            {
                TopTenAgentsWithSalesObjects = await QueryStatistics(TopCount, AmsterdamSalesFilter),
                TopTenAgentsWithSalesObjectsAndGardens = await QueryStatistics(TopCount, AmsterdamWithGarenSalesFilter)
            };

            return View(viewModel);
        }

        public async Task<IEnumerable<SaleStatisticsItem>> QueryStatistics(int count, string filter)
        {
            var query = new GetTopSaleObjectsQuery
            {
                Count = count,
                Filter = filter
            };

            var response = await _mediator.Send(query);

            return _mapper.Map<IEnumerable<GetTopSaleObjectsQueryResponseItem>, IEnumerable<SaleStatisticsItem>>(response.Statistics);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
