using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dto;
using RestaurantChainApp.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantChainApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantChainController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<RestaurantChainController> _logger;

        public RestaurantChainController(ILogger<RestaurantChainController> logger)
        {
            _logger = logger;
        }

        [Route("MenuItems")]
        [HttpGet]
        public GetMenuResponse GetMenuItems()
        {
            return new GetMenuResponse();
        }
    }
}
