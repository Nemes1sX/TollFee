using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TollFee.Api.Config;

namespace TollFee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpPost]
        [Route("SetYear")]
        public IActionResult SetYear([FromBody][Required, Range(2000, 2025)]int year)
        {
            _configService.SetYear(year);

            return Ok(new {messge = string.Format("{0} is set", year)});
        }
    }
}
