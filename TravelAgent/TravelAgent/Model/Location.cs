using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.Model
{
    public class Location
    {
        public long Id { get; set; }
        public String Naziv { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Location(long id, string naziv, double longitude, double latitude)
        {
            Id = id;
            Naziv = naziv;
            Longitude = longitude;
            Latitude = latitude;
        }
        public Location(string naziv, double longitude, double latitude)
        {
            
            Naziv = naziv;
            Longitude = longitude;
            Latitude = latitude;
        }

        public String ToString()
        {
            return this.Naziv;
        }
    }
}
