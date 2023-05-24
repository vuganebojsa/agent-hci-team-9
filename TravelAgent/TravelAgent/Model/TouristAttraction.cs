using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.Model
{
    public class TouristAttraction: IBivuja
    {
        public long Id { get; set; }
        public String Naziv { get; set; }
        public Location Adresa { get; set; }
        public String JeObrisan { get; set; }
        public TouristAttraction()
        {

        }
        public TouristAttraction(long id, string name, Location address, String jeObrisan)
        {
            Id = id;
            Naziv = name;
            this.Adresa = address;
            this.JeObrisan = jeObrisan;
        }
    }
}
