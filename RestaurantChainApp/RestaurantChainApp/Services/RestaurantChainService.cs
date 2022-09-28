using RestaurantChainApp.BusinessLogic;
using RestaurantChainApp.Dtoes;
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
        private readonly IDatabaseConnectionFactory databaseConnectionFactory;

        private IPriceCalculator priceCalculator { get; set; }

        private readonly MenuItemsRepository menuItemsRepository;
        private readonly OrdersRepository ordersRepository;
        private readonly OrderItemsRepository orderItemsRepository;

        private readonly IMapper mapper;


        public RestaurantChainService(IPriceCalculator priceCalculator, 
                                      IRepositoryFactory repositoryFactory,
                                      IDatabaseConnectionFactory databaseConnectionFactory)
        {
            this.priceCalculator = priceCalculator;
            this.databaseConnectionFactory = databaseConnectionFactory;

            menuItemsRepository = repositoryFactory.CreateMenuItemsRepository();
            ordersRepository = repositoryFactory.CreateOrdersRepository();
            orderItemsRepository = repositoryFactory.CreateOrderItemsRepository();

            this.mapper = GenerateMapper();
        }

        private IMapper GenerateMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntitiesToDtoesProfile>();
                cfg.AddProfile<DtoToEntitesProfile>();
            });

            return mapperConfiguration.CreateMapper();
        }

        public List<Dish> GetMenu() 
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
                            }

                            dishes = priceCalculator.CalculateForDishes(dishes);
                            meals = priceCalculator.CalculateForMeals(meals);

                            dishes.AddRange(meals);

                            menuItems = this.mapper.Map<List<MenuItem>>(dishes);

                            foreach (var menuItem in menuItems)
                            {
                                menuItemsRepository.Update(connection, menuItem, transaction);
                            }
                            transaction.Commit();

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
                            dishes = priceCalculator.CalculateForDishes(dishes);

                            menuItems = this.mapper.Map<List<MenuItem>>(dishes);

                            foreach (var menuItem in menuItems)
                            {
                                menuItemsRepository.Update(connection, menuItem, transaction);
                            }
                            transaction.Commit();

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

                            }

                            meals = priceCalculator.CalculateForMeals(meals);

                            menuItems = this.mapper.Map<List<MenuItem>>(meals);

                            foreach (var menuItem in menuItems)
                            {
                                menuItemsRepository.Update(connection, menuItem, transaction);
                            }
                            transaction.Commit();

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
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Create())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                      
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex.Message);
                    }
                }
            }
        }

        public void ModifyOrder(OrderDto order) 
        {
        }

        public void RemoveOrder(int orderid) 
        {
        }
    }
}
