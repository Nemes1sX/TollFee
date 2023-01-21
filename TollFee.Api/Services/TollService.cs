using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TollFee.Api.Models;

namespace TollFee.Api.Services
{
    public class TollService : ITollService
    {
        public CalculateFeeResponse CalculateFee(DateTime[] request)
        {
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
