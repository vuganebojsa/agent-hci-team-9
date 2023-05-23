using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgent.Model;

namespace TravelAgent.services
{
    public class FileService
    {

        private static string filePathUsers = "../../../database/korisnici.txt";
        private static string filePathAllTrips = "../../../database/putovanja.txt";
        private static string filePathBookedTrips = "../../../database/rezervisana_putovanja.txt";


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
                    String[] info = line.Split(";");
                    DateTime dateTime = DateTime.Parse(info[3]);
                    DateTime startDateOnly = dateTime.Date;
                    dateTime = DateTime.Parse(info[4]);
                    DateTime endDateOnly = dateTime.Date;
                    Console.WriteLine(endDateOnly);


                    trips.Add(
                        new Trip(
                            long.Parse(info[0]), info[1], double.Parse(info[2]), startDateOnly, endDateOnly));
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
                    if (long.Parse(info[1])==userId)
                    {
                        using (StreamReader sr2 = new StreamReader(filePathAllTrips))
                        {
                            string lineOfTrips;
                            lineOfTrips = sr2.ReadLine();
                            while ((lineOfTrips = sr2.ReadLine()) != null)
                            {
                                String[] infoTrips = lineOfTrips.Split(";");
                                if (long.Parse(info[0]) == long.Parse(infoTrips[0])){
                                    DateTime dateTime = DateTime.Parse(infoTrips[3]);
                                    DateTime startDateOnly = dateTime.Date;
                                    dateTime = DateTime.Parse(infoTrips[4]);
                                    DateTime endDateOnly = dateTime.Date;
                                    trips.Add(
                        new Trip(
                            long.Parse(infoTrips[0]), infoTrips[1], double.Parse(infoTrips[2]), startDateOnly, endDateOnly));
                                }
                            }
                            }
                        }

                    }
                  
                }
            return trips;
        }
        private static Trip getTripById(long id)
        {
            using (StreamReader sr = new StreamReader(filePathAllTrips))
            {
                string line;
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    
                    if (long.Parse(info[0]) == id)
                    {
                        DateTime dateTime = DateTime.Parse(info[3]);
                        DateTime startDateOnly = dateTime.Date;
                        dateTime = DateTime.Parse(info[4]);
                        DateTime endDateOnly = dateTime.Date;

                        return new Trip(
                           long.Parse(info[0]), info[1], double.Parse(info[2]), startDateOnly, endDateOnly);
                    }
                }
            }

            return null;
        }
        public static List<Trip> getAllSoldTrips()
        {
            List<Trip> trips = new List<Trip>();
            using (StreamReader sr = new StreamReader(filePathBookedTrips))
            {
                string line;
                //idPutovanja;idPutnika
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    Trip trip = getTripById(long.Parse(info[0]));
                    if (trip != null && isTripOver(trip))
                    {
                        trips.Add(trip);
                    }                                
                }
            }
            return trips;
        }

        private static bool isTripOver(Trip trip)
        {
            return trip.DatumPocetka < DateTime.Now;
        }
        public static bool writeTrips(List<Trip> trips)
        {

            File.WriteAllText(filePathAllTrips, string.Empty);

            using (StreamWriter sw = File.AppendText(filePathAllTrips))
            {
                sw.WriteLine("id;naziv;cena;datum pocetka;datum kraja;atrakcije;smestaj i restorani");
                foreach (Trip tr in trips)
                {
                    sw.WriteLine($"{tr.Id.ToString()};{tr.Naziv};{tr.Cena};{tr.DatumPocetka.ToString()};{tr.DatumKraja};{tr.Atrakcije};{tr.SmestajRestorani}");

                }
            }


            return true;
        }

    }




    }

