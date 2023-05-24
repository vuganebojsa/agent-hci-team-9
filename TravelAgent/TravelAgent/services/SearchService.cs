using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgent.Model;

namespace TravelAgent.services
{
    public class SearchService
    {

        public static List<PlaceRestaurant> getPlaceRestaurantsByKeyword(String keyword, List<PlaceRestaurant> placeRestaurants)
        {
            List<PlaceRestaurant> prs = new List<PlaceRestaurant>();

            foreach(PlaceRestaurant restaurant in placeRestaurants)
            {
                if (restaurant.Id.ToString().Contains(keyword) ||
                    restaurant.Naziv.ToLower().Contains(keyword) ||
                    restaurant.Adresa.ToLower().Contains(keyword) ||
                    restaurant.vrsta.ToString().ToLower().Contains(keyword))
                {
                   
                        prs.Add(restaurant);
                }
            }
            return prs;

        }
    }
}
