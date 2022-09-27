using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dto;
using RestaurantChainApp.Entities;
using RestaurantChainApp.Repositories;
using RestaurantChainApp.Factories;
using Npgsql;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantChainApp.Mappings;

namespace RestaurantChainApp.Services
{
    public class RestaurantChainService : IRestaurantChainService
    {
        private IDatabaseConnectionFactory databaseConnectionFactory;

        private IPriceCalculator priceCalculator { get; set; }

        private MealsDishesRepository mealsDishesRepository;
        private MenuItemsRepository menuItemsRepository;
        private OrdersRepository ordersRepository;
        private OrderItemsRepository orderItemsRepository;

        protected readonly IMapper mapper;


        public RestaurantChainService(IPriceCalculator priceCalculator, 
                                      IRepositoryFactory repositoryFactory,
                                      IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.priceCalculator = priceCalculator;
            this.databaseConnectionFactory = databaseConnectionFactory;
            mealsDishesRepository = repositoryFactory.CreateMealsDishesRepository();
            menuItemsRepository = repositoryFactory.CreateMenuItemsRepository();
            ordersRepository = repositoryFactory.CreateOrdersRepository();
            orderItemsRepository = repositoryFactory.CreateOrderItemsRepository();

            this.mapper = GenerateMapper();
        }

        private IMapper GenerateMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntitiesToDtosProfile>();
            });

            return mapperConfiguration.CreateMapper();
        }

        public Menu GetMenu() 
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        List<MenuItem> menuItems = menuItemsRepository.SelectMenuItems(connection, transaction);

                        List<Dish> dishes = this.mapper.Map<List<Dish>>(menuItems.Where(menuitem => !menuitem.IsMeal));
                        List<Meal> meals = this.mapper.Map<List<Meal>>(menuItems.Where(menuitem => menuitem.IsMeal));

                        foreach (var meal in meals)
                        {
                            List<MenuItem> menuItemsForMeal = menuItemsRepository.SelectMenuItemsByMealId(connection, meal.Id, transaction);

                            meal.Dishes = this.mapper.Map<List<Dish>>(menuItemsForMeal);
                            meal.Price = priceCalculator.CalculateForMeal(meal);

                        }

                        dishes.AddRange(meals);

                        Menu menu = new Menu(dishes);

                        return menu;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new Menu(new List<Dish>());
                    }
                  
                }
            }

          
        }

        public List<Dish> GetSingleDishes() 
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        List<MenuItem> menuItems = menuItemsRepository.SelectMenuItemsByIsMeal(connection, ismeal: false, transaction);

                        List<Dish> dishes = this.mapper.Map<List<Dish>>(menuItems);

                        return dishes;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new List<Dish>();
                    }

                }
            }
        }
        public List<Meal> GetMeals() 
        {
            using (NpgsqlConnection connection = databaseConnectionFactory.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        List<MenuItem> menuItems = menuItemsRepository.SelectMenuItemsByIsMeal(connection, ismeal: true, transaction);

                        List<Meal> meals = this.mapper.Map<List<Meal>>(menuItems.Where(menuitem => menuitem.IsMeal));

                        foreach (var meal in meals)
                        {
                            List<MenuItem> menuItemsForMeal = menuItemsRepository.SelectMenuItemsByMealId(connection, meal.Id, transaction);

                            meal.Dishes = this.mapper.Map<List<Dish>>(menuItemsForMeal);

                            //menuItemsForMeal.ForEach(item => 
                            //{
                            //    Dish dish = this.mapper.Map<Dish>(item);
                            //    meal.AddDish(dish);
                            //});
                            meal.Price = priceCalculator.CalculateForMeal(meal);

                        }


                        return meals;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new List<Meal>();
                    }

                }
            }
        }

        public OrderDto GetOrder(int orderid)
        {
            return new OrderDto();
        }

        public void CreateOrder(OrderDto order) 
        {
        }

        public void ModifyOrder(OrderDto order) 
        {
        }

        public void RemoveOrder(int orderid) 
        {
        }
    }
}
