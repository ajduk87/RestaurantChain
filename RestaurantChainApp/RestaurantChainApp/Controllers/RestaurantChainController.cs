using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dto;
using RestaurantChainApp.Services;
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

        private readonly ILogger<RestaurantChainController> logger;
        private IRestaurantChainService restaurantChainService;

        public RestaurantChainController(IRestaurantChainService restaurantChainService, ILogger<RestaurantChainController> logger)
        {
            this.restaurantChainService = restaurantChainService;
            this.logger = logger;
        }

        [Route("GetMenu")]
        [HttpGet]
        public Menu GetMenuItems()
        {
            return this.restaurantChainService.GetMenu();
        }

        [Route("GetSingleDishes")]
        [HttpGet]
        public List<Dish> GetSingleDishes()
        {
            return this.restaurantChainService.GetSingleDishes();
        }

        [Route("GetMeals")]
        [HttpGet]
        public List<Meal> GetMeals()
        {
            return this.restaurantChainService.GetMeals();
        }
    }
}
