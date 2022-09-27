using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dtoes;
using RestaurantChainApp.Entities;
using RestaurantChainApp.Mappings;
using RestaurantChainApp.Models.Order;
using RestaurantChainApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestaurantChainApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantChainController : ControllerBase
    {      

        private readonly ILogger<RestaurantChainController> logger;
        private IRestaurantChainService restaurantChainService;
        private readonly IMapper mapper;

        public RestaurantChainController(IRestaurantChainService restaurantChainService, ILogger<RestaurantChainController> logger)
        {
            this.restaurantChainService = restaurantChainService;
            this.logger = logger;

            this.mapper = GenerateMapper();
        }

        private IMapper GenerateMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelsToDtoes>();
            });

            return mapperConfiguration.CreateMapper();
        }

        [Route("GetMenu")]
        [HttpGet]        
        public List<Dish> GetMenuItems()
        {
            return this.restaurantChainService.GetMenu().Dishes;
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

        [Route("GetOrder")]
        [HttpGet]
        public OrderDto GetMeals(int orderid)
        {
            return this.restaurantChainService.GetOrder(orderid);
        }

        [Route("CreateOrder")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(CreateOrderModel createOrderModel)
        {
            OrderDto orderDto = this.mapper.Map<OrderDto>(createOrderModel);
            this.restaurantChainService.CreateOrder(orderDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("ModifyOrder")]
        [HttpPut]
        public HttpResponseMessage ModifyOrder(UpdateOrderModel updateOrderModel)
        {
            OrderDto orderDto = this.mapper.Map<OrderDto>(updateOrderModel);
            this.restaurantChainService.ModifyOrder(orderDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("RemoveOrder")]
        [HttpDelete]
        public HttpResponseMessage RemoveOrder(int orderid)
        {
            this.restaurantChainService.RemoveOrder(orderid);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
