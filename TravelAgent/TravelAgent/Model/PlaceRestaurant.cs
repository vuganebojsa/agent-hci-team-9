using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.Model
{
    public class PlaceRestaurant: IBivuja
    {

        public long Id { get; set; }
        public String Naziv { get; set; }
        public Location Adresa { get; set; }
        public String JeObrisan { get; set; }

        public Vrsta vrsta { get; set; }

        public PlaceRestaurant()
        {

        }
        public PlaceRestaurant(long id, string name, Location address, Vrsta vrsta, string isDeleted)
        {
            Id = id;
            Naziv = name;
            this.Adresa = address;
            this.vrsta = vrsta;
            this.JeObrisan = isDeleted; 
        }
    }

    public enum Vrsta
    {
        Restoran, Smestaj
    }
}
