﻿using System;
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
        private static string filePathBookedTrips = "../../../database/putovanja.txt";


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
            using (StreamReader sr = new StreamReader(filePathBookedTrips))
            {
                string line;
                //id;ime;prezime;email;lozinka;uloga
                line = sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    String[] info = line.Split(";");
                    DateTime dateTime = DateTime.Parse(info[3]);
                    DateTime startDateOnly = dateTime.Date;
                    dateTime = DateTime.Parse(info[3]);
                    DateTime endDateOnly = dateTime.Date;
                    Console.WriteLine(endDateOnly);


                    trips.Add(
                        new Trip(
                            long.Parse(info[0]), info[1], double.Parse(info[2]), startDateOnly, endDateOnly));
                }
            }
            return trips;
        }

    }
}
