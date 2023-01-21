using System;
using System.Collections.Generic;
using System.Linq;
using TollFee.Api.Models;

namespace TollFee.Api.Services
{
    public class TollService : ITollService
    {
        private TollFreeService _tollFreeService;

        public TollService(TollFreeService tollFreeService) 
        {
           _tollFreeService = tollFreeService;
        }

        public CalculateFeeResponse CalculateFee(DateTime[] request)
        {
            var notFree = _tollFreeService.RemoveFree(request);
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
