using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.Model
{
    public interface IBivuja
    {
        long Id { get; set; }
        String Naziv { get; set; }
        Location Adresa { get; set; }
        String JeObrisan { get; set; }
    }
}
