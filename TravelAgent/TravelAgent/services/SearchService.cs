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

        public static List<Trip> getTripsByKeyword(String keyword, List<Trip> trips)
        {
            List<Trip> trp = new List<Trip>();

            foreach (Trip trip in trips)
            {
                if (trip.Id.ToString().Contains(keyword) ||
                    trip.Naziv.ToLower().Contains(keyword) ||
                    trip.Cena.ToString().Contains(keyword) ||
                    trip.DatumPocetka.ToString().Contains(keyword) ||
                    trip.DatumKraja.ToString().Contains(keyword))
                {

                    trp.Add(trip);
                }
            }
            return trp;

        }

        public static List<SoldTrip> getSoldTripsByKeyword(String keyword, List<SoldTrip> trips)
        {
           List<SoldTrip> trp = new List<SoldTrip>();

            foreach (SoldTrip trip in trips)
            {
                if (trip.Trip.Id.ToString().Contains(keyword) ||
                    trip.Trip.Naziv.ToLower().Contains(keyword) ||
                    trip.Trip.Cena.ToString().Contains(keyword) ||
                    trip.Trip.DatumPocetka.ToString().Contains(keyword) ||
                    trip.User.Name.ToLower().Contains(keyword))
                {

                    trp.Add(trip);
                }
            }
            return trp;

        }

        public static List<SoldTrip> getSoldTripsBDate(String keyword, List<SoldTrip> trips)
        {
            List<SoldTrip> trp = new List<SoldTrip>();

            foreach (SoldTrip trip in trips)
            {
                if (trip.Trip.DatumPocetka.ToString().Split("/")[0].Contains(keyword)) 
                {
                    trp.Add(trip);

                }
            }
            return trp;

        }
    }
}
