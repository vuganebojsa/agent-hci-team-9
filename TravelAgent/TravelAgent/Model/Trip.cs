using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.Model
{
    public class Trip
    {
        public long Id { get; set; }
        public String Naziv { get; set; }   
        public double Cena { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumKraja { get; set; }

        public List<TouristAttraction> Atrakcije { get; set; }
        public List<PlaceRestaurant> SmestajRestorani { get; set; }
        public string Obrisan { get; set; }

        public Trip(long id, string naziv, double cena, DateTime datumPocetka, DateTime datumKraja, List<TouristAttraction> atrakcije, List<PlaceRestaurant> smestajRestorani, string obrisan)
        {
            Id = id;
            Naziv = naziv;
            Cena = cena;
            DatumPocetka = datumPocetka;
            DatumKraja = datumKraja;
            Atrakcije = atrakcije;
            SmestajRestorani = smestajRestorani;
            Obrisan = obrisan;
        }
        public Trip(long id, string naziv, double cena, DateTime datumPocetka, DateTime datumKraja, string obrisan)
        {
            Id = id;
            Naziv = naziv;
            Cena = cena;
            DatumPocetka = datumPocetka;
            DatumKraja = datumKraja;
            Atrakcije = new List<TouristAttraction>();
            SmestajRestorani = new List<PlaceRestaurant>();
            Obrisan = obrisan;

        }

        public void AddAttraction(TouristAttraction attraction)
        {
            Atrakcije.Add(attraction);
        }

        public void AddPlaceRestaurant(PlaceRestaurant attraction)
        {
            SmestajRestorani.Add(attraction);
        }

        public void RemoveAttraction(long id)
        {
            foreach(TouristAttraction att in Atrakcije)
            {
                if(att.Id == id)
                {
                    Atrakcije.Remove(att);
                    break;
                }
            }
        }

        public void RemovePlaceRestaurant(long id)
        {
            foreach (PlaceRestaurant att in SmestajRestorani)
            {
                if (att.Id == id)
                {
                    SmestajRestorani.Remove(att);
                    break;
                }
            }
        }
    }
}
