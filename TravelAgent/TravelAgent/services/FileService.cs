using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using TravelAgent.Model;

namespace TravelAgent.services
{
    public class FileService
    {

        private static string filePathUsers = "../../../database/korisnici.txt";
        private static string filePathAllTrips = "../../../database/putovanja.txt";
        private static string filePathBookedTrips = "../../../database/rezervisana_putovanja.txt";
        private static string filePathLocations = "../../../database/lokacije.txt";
        private static string filePathRestaurants = "../../../database/restorani_smestaji.txt";
        private static string filePathAtractions = "../../../database/turisticke_atrakcije.txt";


        public static User getUserByEmailAndPassword(String email, String password)
        {

            using (StreamReader sr = new StreamReader(filePathUsers))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    if (info[3] == email && info[4] == password)
                    {

                        return new User(long.Parse(info[0]), info[1], info[2], info[3], info[4], (Role)Enum.Parse(typeof(Role), info[5]));


                    }
                }
            }


            return null;
        }

        public static User getUserById(long id)
        {

            using (StreamReader sr = new StreamReader(filePathUsers))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    if (long.Parse(info[0]) == id)
                    {

                        return new User(long.Parse(info[0]), info[1], info[2], info[3], info[4], (Role)Enum.Parse(typeof(Role), info[5]));


                    }
                }
            }


            return null;
        }

        public static TouristAttraction getAtractionsById(long id)
        {

            using (StreamReader sr = new StreamReader(filePathAtractions))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    if (long.Parse(info[0]) == id)
                    {

                        Location location = getLocationFromFileById(info[2]);

                        return new TouristAttraction(long.Parse(info[0]), info[1], location, info[3]);


                    }
                }
            }


            return null;
        }

        public static PlaceRestaurant getRestaurantsById(long id)
        {

            using (StreamReader sr = new StreamReader(filePathRestaurants))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    if (long.Parse(info[0]) == id)
                    {

                        Location location = getLocationFromFileById(info[2]);

                        return new PlaceRestaurant(
                            long.Parse(info[0]), info[1], location, (Vrsta)Enum.Parse(typeof(Vrsta), info[3]), info[4]);


                    }
                }
            }


            return null;
        }

        public static long getLastUserIdFromFile()
        {
            String id = "#";
            using (StreamReader sr = new StreamReader(filePathUsers))
            {
                string line;
                // 
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    long lid = long.Parse(info[0]);
                    lid = lid + 1;
                    id = lid.ToString();
                }
            }
            if (id == "#") return 1;
            return long.Parse(id);
        }

        public static bool registerUser(String name, String surname, String email, String password)
        {

            long id = getLastUserIdFromFile();

            using (StreamWriter sw = File.AppendText(filePathUsers))
            {
                String role = Role.USER.ToString();
                sw.WriteLine($"{id};{name};{surname};{email};{password};{role}");
            }


            return true;
        }
        public static long getLastIdFromPlacesRestaurants()
        {
            String id = "#";
            using (StreamReader sr = new StreamReader(filePathRestaurants))
            {
                string line;
                // 
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    long lid = long.Parse(info[0]);
                    lid = lid + 1;
                    id = lid.ToString();
                }
            }
            if (id == "#") return 1;
            return long.Parse(id);
        }

        public static long getLastIdFromTrips()
        {
            String id = "#";
            using (StreamReader sr = new StreamReader(filePathAllTrips))
            {
                string line;
                // 
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    long lid = long.Parse(info[0]);
                    lid = lid + 1;
                    id = lid.ToString();
                    
                }
            }
            if (id == "#") return 1;
            return long.Parse(id);
        }

        public static void deletePlacesRestaurants(PlaceRestaurant selectedItem, List<PlaceRestaurant> placesRestaurants)
        {
            File.WriteAllText(filePathRestaurants, string.Empty);

            selectedItem.JeObrisan = "1";
            using (StreamWriter sw = File.AppendText(filePathRestaurants))
            {
                sw.WriteLine("id;naziv;mesto;tip;obrisan");
                foreach (PlaceRestaurant pr in placesRestaurants)
                {

                    sw.WriteLine($"{pr.Id.ToString()};{pr.Naziv};{pr.Adresa.Id};{pr.vrsta.ToString()};{pr.JeObrisan}");

                }
            }



        }

        public static List<PlaceRestaurant> getPlacesAndRestaurants()
        {
            List<PlaceRestaurant> restaurants = new List<PlaceRestaurant>();
            using (StreamReader sr = new StreamReader(filePathRestaurants))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    String locationId = info[2];
                    Location location = getLocationFromFileById(locationId);

                    restaurants.Add(
                        new PlaceRestaurant(
                            long.Parse(info[0]), info[1], location, (Vrsta)Enum.Parse(typeof(Vrsta), info[3]), info[4]));

                }
            }


            return restaurants;
        }

        public static List<TouristAttraction> getAtractions()
        {
            List<TouristAttraction> atractions = new List<TouristAttraction>();
            using (StreamReader sr = new StreamReader(filePathAtractions))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    String locationId = info[2];
                    Location location = getLocationFromFileById(locationId);

                    atractions.Add(
                        new TouristAttraction(
                            long.Parse(info[0]), info[1], location,  info[3]));

                }
            }


            return atractions;
        }

        private static Location getLocationFromFileById(string locationId)
        {
            Location location = null;
            using (StreamReader sr = new StreamReader(filePathLocations))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    if (info[0] == locationId)
                    {
                        location = new Location(long.Parse(info[0]), info[1], Double.Parse(info[2]), Double.Parse(info[3]));
                        break;
                    }




                }
            }
            return location;
        }



        public static List<Location> GetAllLocations()
        {
            List<Location> locations = new List<Location>();
            using (StreamReader sr = new StreamReader(filePathLocations))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    String locationId = info[2];
                    Location location = getLocationFromFileById(locationId);

                    locations.Add(
                        new Location(
                            long.Parse(info[0]), info[1], Double.Parse(info[2]), Double.Parse(info[3])));

                }
            }


            return locations;
        }
        public static bool editLocation(Location location)
        {
            List<Location> locations = GetAllLocations();

            File.WriteAllText(filePathLocations, string.Empty);

            using (StreamWriter sw = File.AppendText(filePathLocations))
            {
                sw.WriteLine("id;naziv;longitude;latitude");
                foreach (Location loc in locations)
                {
                    if (loc.Id == location.Id)
                        sw.WriteLine($"{location.Id};{location.Naziv};{location.Longitude};{location.Latitude}");
                    else
                        sw.WriteLine($"{loc.Id};{loc.Naziv};{loc.Longitude};{loc.Latitude}");

                }
            }


            return true;
        }

        public static List<Trip> getAllTrips()
        {
            List<Trip> trips = new List<Trip>();
            
            using (StreamReader sr = new StreamReader(filePathAllTrips))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    List<IBivuja> bivujas = new List<IBivuja>();
                    String[] info = line.Split(";");
                    DateTime dateTime = DateTime.Parse(info[3]);
                    DateTime startDateOnly = dateTime.Date;
                    dateTime = DateTime.Parse(info[4]);
                    DateTime endDateOnly = dateTime.Date;
                    //id-t/id-s/id-t
                    String[] objectsIds = info[5].Split("/");
                    foreach(String objectId in objectsIds)
                    {
                        String[] ids = objectId.Split("-");
                        if (ids[1] == "t")
                        {
                            bivujas.Add(getAtractionsById(long.Parse(ids[0])));
                        }
                        else
                        {
                            bivujas.Add(getRestaurantsById(long.Parse(ids[0])));
                        }
                    }

                    trips.Add(
                        new Trip(
                            long.Parse(info[0]), info[1], double.Parse(info[2]), startDateOnly, endDateOnly,bivujas, info[6]));
                }
            }
            return trips;
        }

        public static bool writePlacesRestaurants(List<PlaceRestaurant> placeRestaurants)
        {

            File.WriteAllText(filePathRestaurants, string.Empty);

            using (StreamWriter sw = File.AppendText(filePathRestaurants))
            {
                sw.WriteLine("id;naziv;mesto;tip;fleg");
                foreach (PlaceRestaurant pr in placeRestaurants)
                {
                    sw.WriteLine($"{pr.Id.ToString()};{pr.Naziv};{pr.Adresa.Id};{pr.vrsta.ToString()};{pr.JeObrisan}");

                }
            }


            return true;
        }

        public static bool writeTrips(List<Trip> trips)
        {

            File.WriteAllText(filePathAllTrips, string.Empty);

            using (StreamWriter sw = File.AppendText(filePathAllTrips))
            {
                sw.WriteLine("id;naziv;cena;datum pocetka;datum kraja;atrakcije;smestaj i restorani;obrisan");
                foreach (Trip trip in trips)
                {
                    String str = "";
                    foreach (IBivuja ibj in trip.Objekti)
                    {
                        if (ibj.GetType() == typeof(PlaceRestaurant))
                        {
                            str += ibj.Id + "-s/";

                        }
                        else
                        {
                            str += ibj.Id + "-t/";

                        }

                    }
                    if( str.Length > 0) {
                        str = str.Remove(str.Length - 1);
                    }

                    sw.WriteLine($"{trip.Id.ToString()};{trip.Naziv};{trip.Cena};{trip.DatumPocetka};{trip.DatumKraja};{str};{trip.JeObrisan}");

                }
            }


            return true;
        }

        public static bool addPlaceRestaurant(PlaceRestaurant placeRestaurant)
        {

            long id = getLastIdFromPlacesRestaurants();
            long locationId = getLastIdFromLocations();

            using (StreamWriter sw = File.AppendText(filePathLocations))
            {

                sw.WriteLine($"{locationId};{placeRestaurant.Adresa.Naziv};{placeRestaurant.Adresa.Longitude};{placeRestaurant.Adresa.Latitude}");
            }
            using (StreamWriter sw = File.AppendText(filePathRestaurants))
            {

                sw.WriteLine($"{id};{placeRestaurant.Naziv};{locationId};{placeRestaurant.vrsta.ToString()};{placeRestaurant.JeObrisan}");
            }


            return true;
        }

        public static bool addTrips(Trip trip)
        {

            long id = getLastIdFromTrips();           
            using (StreamWriter sw = File.AppendText(filePathAllTrips))
            {
               
                    String str = "";
                    foreach (IBivuja ibj in trip.Objekti)
                    {
                        if (ibj.GetType() == typeof(PlaceRestaurant))
                        {
                            str += ibj.Id + "-s/";

                        }
                        else
                        {
                            str += ibj.Id + "-t/";

                        }

                    }
                if (str.Length > 0)
                {
                    str = str.Remove(str.Length - 1);
                }

                sw.WriteLine($"{id};{trip.Naziv};{trip.Cena};{trip.DatumPocetka};{trip.DatumKraja};{str};{trip.JeObrisan}");

                

                // sw.WriteLine($"{id};{placeRestaurant.Naziv};{locationId};{placeRestaurant.vrsta.ToString()};{placeRestaurant.JeObrisan}");
            }


            return true;
        }

        private static long getLastIdFromLocations()
        {
            String id = "#";
            using (StreamReader sr = new StreamReader(filePathLocations))
            {
                string line;
                // 
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    long lid = long.Parse(info[0]);
                    lid = lid + 1;
                    id = lid.ToString();
                }
            }
            if (id == "#") return 1;
            return long.Parse(id);
        }

        public static List<Trip> getAllActiveTrips()
        {
            List<Trip> trips = new List<Trip>();
            
            using (StreamReader sr = new StreamReader(filePathAllTrips))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    List<IBivuja> bivujas = new List<IBivuja>();
                    String[] info = line.Split(";");
                    DateTime dateTime = DateTime.Parse(info[3]);
                    DateTime startDateOnly = dateTime.Date;
                    dateTime = DateTime.Parse(info[4]);
                    DateTime endDateOnly = dateTime.Date;
                    
                    String[] objectsIds = info[5].Split("/");
                    foreach (String objectId in objectsIds)
                    {
                        String[] ids = objectId.Split("-");
                        if (ids[1] == "t")
                        {
                            bivujas.Add(getAtractionsById(long.Parse(ids[0])));
                        }
                        else
                        {
                            bivujas.Add(getRestaurantsById(long.Parse(ids[0])));
                        }
                    }

                    if (info[6] == "0")
                    {
                        trips.Add(
                         new Trip(
                             long.Parse(info[0]), info[1], double.Parse(info[2]), startDateOnly, endDateOnly, bivujas, info[6]));
                    }
                }
            }
            return trips;
        }

        public static List<Trip> getAllTripsByUserId(long userId)
        {
            
            List<Trip> trips = new List<Trip>();
            using (StreamReader sr = new StreamReader(filePathBookedTrips))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    if (long.Parse(info[1]) == userId)
                    {
                        using (StreamReader sr2 = new StreamReader(filePathAllTrips))
                        {
                            string lineOfTrips;
                            lineOfTrips = sr2.ReadLine();
                            while ((lineOfTrips = sr2.ReadLine()) != null)
                            {
                                List<IBivuja> bivujas = new List<IBivuja>();
                                String[] infoTrips = lineOfTrips.Split(";");
                                if (long.Parse(info[0]) == long.Parse(infoTrips[0]))
                                {
                                    DateTime dateTime = DateTime.Parse(infoTrips[3]);
                                    DateTime startDateOnly = dateTime.Date;
                                    dateTime = DateTime.Parse(infoTrips[4]);
                                    DateTime endDateOnly = dateTime.Date;
                                    String[] objectsIds = infoTrips[5].Split("/");
                                    foreach (String objectId in objectsIds)
                                    {
                                        String[] ids = objectId.Split("-");
                                        if (ids[1] == "t")
                                        {
                                            bivujas.Add(getAtractionsById(long.Parse(ids[0])));
                                        }
                                        else
                                        {
                                            bivujas.Add(getRestaurantsById(long.Parse(ids[0])));
                                        }
                                    }
                                    trips.Add(
                        new Trip(
                            long.Parse(infoTrips[0]), infoTrips[1], double.Parse(infoTrips[2]), startDateOnly, endDateOnly, bivujas, infoTrips[6]));
                                }
                            }
                        }
                    }

                }

            }
            return trips;
        }
        public static Trip getTripById(long id)
        {
            
            using (StreamReader sr = new StreamReader(filePathAllTrips))
            {
                string line;
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    List<IBivuja> bivujas = new List<IBivuja>();
                    String[] info = line.Split(";");

                    if (long.Parse(info[0]) == id)
                    {
                        DateTime dateTime = DateTime.Parse(info[3]);
                        DateTime startDateOnly = dateTime.Date;
                        dateTime = DateTime.Parse(info[4]);
                        DateTime endDateOnly = dateTime.Date;
                        String[] objectsIds = info[5].Split("/");
                        foreach (String objectId in objectsIds)
                        {
                            String[] ids = objectId.Split("-");
                            if (ids[1] == "t")
                            {
                                bivujas.Add(getAtractionsById(long.Parse(ids[0])));
                            }
                            else
                            {
                                bivujas.Add(getRestaurantsById(long.Parse(ids[0])));
                            }
                        }

                        return new Trip(
                            long.Parse(info[0]), info[1], double.Parse(info[2]), startDateOnly, endDateOnly, bivujas, info[6]);
                    }
                }
            }

            return null;
        }
        public static List<SoldTrip> getAllSoldTrips()
        {
            List<SoldTrip> soldTrips = new List<SoldTrip>();
            using (StreamReader sr = new StreamReader(filePathBookedTrips))
            {
                string line;
                //idPutovanja;idPutnika
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    Trip trip = getTripById(long.Parse(info[0]));
                    //if (trip.obrisan!= 0 && isTripOver(trip))

                    if (isTripOver(trip))
                    {

                        User user = getUserById(long.Parse(info[1]));
                        SoldTrip soldTrip = new SoldTrip(trip, user);
                        soldTrips.Add(soldTrip);
                    }
                }
            }
            return soldTrips;
        }

        private static bool isTripOver(Trip trip)
        {
            return trip.DatumPocetka < DateTime.Now;
        }

        public static void deleteTrip(List<Trip> trips, Trip selectedItem)
        {
            File.WriteAllText(filePathAllTrips, string.Empty);

            selectedItem.JeObrisan = "1";
            using (StreamWriter sw = File.AppendText(filePathAllTrips))
            {
                


                sw.WriteLine("id;naziv;cena;datum pocetka;datum kraja;atrakcije;smestaj i restorani;obrisan");
                foreach (Trip tr in trips)
                {
                    String str = "";
                    foreach (IBivuja ibj in tr.Objekti)
                    {
                        if (ibj.GetType() == typeof(PlaceRestaurant))
                        {
                            str += ibj.Id + "-s/";
                            

                        }
                        else
                        {
                            str += ibj.Id + "-t/";
                            
                        }
                        
                    }
                    str = str.Remove(str.Length - 1);

                    sw.WriteLine($"{tr.Id.ToString()};{tr.Naziv};{tr.Cena};{tr.DatumPocetka};{tr.DatumKraja};{str};{tr.JeObrisan}");

                }
            }



        }
    }




}

