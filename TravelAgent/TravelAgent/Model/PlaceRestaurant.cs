using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.Model
{
    public class PlaceRestaurant
    {

        public long Id { get; set; }
        public String Naziv { get; set; }
        public String Adresa { get; set; }
        public Vrsta vrsta { get; set; }

        public PlaceRestaurant()
        {

        }
        public PlaceRestaurant(long id, string name, string address, Vrsta vrsta)
        {
            Id = id;
            Naziv = name;
            this.Adresa = address;
            this.vrsta = vrsta; 
        }
    }

    public enum Vrsta
    {
        Restoran, Smestaj
    }
}
