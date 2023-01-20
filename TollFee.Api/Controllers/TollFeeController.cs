using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TollFee.Api.Models;
using TollFee.Api.Services;

namespace TollFee.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TollFeeController : ControllerBase
    {
        private readonly ITollService _tollService;
        public TollFeeController(ITollService tollService)
        {
            _tollService = tollService ?? throw new ArgumentNullException(nameof(tollService));
        }

        [HttpGet]
        public CalculateFeeResponse CalculateFee([FromQuery]DateTime[] request)
        {
            request = new List<DateTime>()
            {
                new DateTime(2021, 12, 1, 7, 30, 1),
                new DateTime(2021, 12, 1, 9, 30, 1),
                new DateTime(2021, 1, 1),
                new DateTime(2021, 1, 2)
            }.ToArray();

            var notFree = TollFreeService.RemoveFree(request);
            var totalFee = TollFeeService.GetFee(notFree);

            var response = new CalculateFeeResponse
            {
                TotalFee = totalFee,
                AverageFeePerDay = totalFee / request.Distinct().Count()
            };

            return response;
        }
    }
}
