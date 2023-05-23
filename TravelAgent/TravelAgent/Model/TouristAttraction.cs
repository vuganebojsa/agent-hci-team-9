using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.Model
{
    public class TouristAttraction
    {
        public long Id { get; set; }
        public String Naziv { get; set; }
        public String Adresa { get; set; }

        public TouristAttraction()
        {

        }
        public TouristAttraction(long id, string name, string address)
        {
            Id = id;
            Naziv = name;
            this.Adresa = address;
        }
    }
}
