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



    }
}
