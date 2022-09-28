using Newtonsoft.Json;
using NUnit.Framework;
using RestaurantChainApp.Dtoes;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantChainApp.Integration.Tests
{
    public class RestaurantChainTests
    {
        private ApiCaller apiCaller;

        [SetUp]
        public void Setup()
        {     
            apiCaller = new ApiCaller();
        }

        [Test]
        public void GetMenuTest()
        {
            string response = this.apiCaller.Get(Urls.GetMenuUrl());

            List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(response);

            Assert.That(dishes.Count, Is.EqualTo(14));
            Assert.That(dishes.Where(dish => !dish.IsMeal).Count, Is.EqualTo(10));
            Assert.That(dishes.Where(dish => dish.IsMeal).Count, Is.EqualTo(4));
        }

        [Test]
        public void GetSingleDishesTest()
        {
            string response = this.apiCaller.Get(Urls.GetSingleDishesUrl());

            List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(response);

            Assert.That(dishes.Count, Is.EqualTo(10));
        }

        [Test]
        public void GetMealsTest()
        {
            string response = this.apiCaller.Get(Urls.GetMealsUrl());

            List<Meal> meals = JsonConvert.DeserializeObject<List<Meal>>(response);

            Assert.That(meals.Count, Is.EqualTo(4));
        }
    }
}