using RestaurantChainApp.Entities;
using System.Data;
using System.Resources;
using System.Collections.Generic;
using Dapper;

namespace RestaurantChainApp.Repositories
{
    public class MenuItemsRepository
    {
        public List<MenuItem> SelectMenuItems(IDbConnection connection, IDbTransaction transaction = null)
        {          
            
            return connection.Query<MenuItem>(Sql.Queries["SelectMenuItems"]).AsList();
        }

        public List<MenuItem> SelectMenuItemsByMealId(IDbConnection connection, int mealid, IDbTransaction transaction = null)
        {
            return connection.Query<MenuItem>(Sql.Queries["SelectMenuItemsByMealId"], new { mealid = mealid }).AsList();
        }
    }
}
