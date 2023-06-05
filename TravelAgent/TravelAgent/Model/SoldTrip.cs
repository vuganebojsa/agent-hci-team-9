using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.Model
{
    public class SoldTrip
    {
        public Trip Trip { get; set; }
        public User User { get; set; }

        public SoldTrip(Trip trip, User user)
        {
            Trip = trip;
            User = user;
            

        }
        public SoldTrip()
        {

        }
    }
}
