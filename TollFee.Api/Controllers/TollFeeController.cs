using Microsoft.AspNetCore.Mvc;
using System;
using TollFee.Api.Models;
using TollFee.Api.Models.ValidationRules;
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

        [HttpPost]
        public CalculateFeeResponse CalculateFee([FromBody][ExistingYear]DateTime[] request)
        {
           var response = _tollService.CalculateFee(request);

            return response;
        }
    }
}
