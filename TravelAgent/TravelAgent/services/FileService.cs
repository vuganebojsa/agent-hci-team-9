using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgent.Model;

namespace TravelAgent.services
{
    public class FileService
    {

        private static string filePathUsers = "../../../database/korisnici.txt";
        private static string filePathRestaurants = "../../../database/restorani_smestaji.txt";


        public static User getUserByEmailAndPassword(String email, String password)
        {

            using(StreamReader sr = new StreamReader(filePathUsers))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    if (info[3] == email && info[4] == password)
                    {
                        return new User(long.Parse(info[0]), info[1], info[2], info[3], info[4], (Role) Enum.Parse(typeof(Role), info[5]));
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

        public static void deletePlacesRestaurants(PlaceRestaurant selectedItem, List<PlaceRestaurant> placesRestaurants)
        {
            File.WriteAllText(filePathRestaurants, string.Empty);

            selectedItem.isDeleted = "1";
            using (StreamWriter sw = File.AppendText(filePathRestaurants))
            {
                sw.WriteLine("id;naziv;mesto;tip;obrisan");
                foreach (PlaceRestaurant pr in placesRestaurants)
                {
                    
                    sw.WriteLine($"{pr.Id.ToString()};{pr.Naziv};{pr.Adresa};{pr.vrsta.ToString()};{pr.isDeleted}");
                    
                }
            }


            
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
                    
                    restaurants.Add(
                        new PlaceRestaurant(
                            long.Parse(info[0]), info[1], info[2], (Vrsta)Enum.Parse(typeof(Vrsta), info[3]), info[4]));

                        
                    
                }
            }


            return restaurants;
        }
        public static bool writePlacesRestaurants(List<PlaceRestaurant> placeRestaurants)
        {

            File.WriteAllText(filePathRestaurants, string.Empty);

            using (StreamWriter sw = File.AppendText(filePathRestaurants))
            {
                sw.WriteLine("id;naziv;mesto;tip;fleg");
                foreach(PlaceRestaurant pr in placeRestaurants)
                {
                    sw.WriteLine($"{pr.Id.ToString()};{pr.Naziv};{pr.Adresa};{pr.vrsta.ToString()};{pr.isDeleted}");

                }
            }


            return true;
        }

        public static bool addPlaceRestaurant(PlaceRestaurant placeRestaurant)
        {

            long id = getLastIdFromPlacesRestaurants();

            using (StreamWriter sw = File.AppendText(filePathRestaurants))
            {
                
                sw.WriteLine($"{id};{placeRestaurant.Naziv};{placeRestaurant.Adresa};{placeRestaurant.vrsta.ToString()};{placeRestaurant.isDeleted}");
            }


            return true;
        }

    }
}
