using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaleStatistics.Application.Queries.GetStatistics;
using SaleStatistics.Web.Models;

namespace SaleStatistics.Web.Controllers
{
    public class SaleStatisticsController : Controller
    {
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
            return View(await QueryStatistics());
        }

        public async Task<SaleStatisticsViewModel> QueryStatistics()
        {
            var query = new GetStatisticsQuery();

            var response = await _mediator.Send(query);

            return _mapper.Map<GetStatisticsQueryResponse, SaleStatisticsViewModel>(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
