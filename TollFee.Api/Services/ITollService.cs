using System;
using TollFee.Api.Models;

namespace TollFee.Api.Services
{
    public interface ITollService
    {
        CalculateFeeResponse CalculateFee(DateTime[] request);
    }
}
