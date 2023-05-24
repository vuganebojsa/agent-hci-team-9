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
                    restaurant.Adresa.Naziv.ToLower().Contains(keyword) ||
                    restaurant.vrsta.ToString().ToLower().Contains(keyword))
                {
                   
                        prs.Add(restaurant);
                }
            }
            return prs;

        }

        public static List<TouristAttraction> getAttractionsByKeyword(String keyword, List<TouristAttraction> attractions)
        {
            List<TouristAttraction> atts = new List<TouristAttraction>();

            foreach (TouristAttraction attraction in attractions)
            {
                if (attraction.Id.ToString().Contains(keyword) ||
                    attraction.Naziv.ToLower().Contains(keyword) ||
                    attraction.Adresa.Naziv.ToLower().Contains(keyword))
                {

                    atts.Add(attraction);
                }
            }
            return atts;

        }

    }
}
