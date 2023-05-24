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

        public List<IBivuja> Objekti { get; set; }

        public String JeObrisan;

        public void AddObject(IBivuja obj)
        {
            this.Objekti.Add(obj);
        }
        public void RemoveObject(IBivuja obj)
        {
            this.Objekti.Remove(obj);
        }
        public Trip(long id, string naziv, double cena, DateTime datumPocetka, DateTime datumKraja, List<IBivuja> objekti, string jeObrisan)
        {
            Id = id;
            Naziv = naziv;
            Cena = cena;
            DatumPocetka = datumPocetka;
            DatumKraja = datumKraja;
            Objekti = objekti;
            JeObrisan = jeObrisan;
        }
    }
}
