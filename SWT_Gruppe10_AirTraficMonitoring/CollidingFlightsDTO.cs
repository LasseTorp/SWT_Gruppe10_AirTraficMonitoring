using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class CollidingFlightsDTO
    {
        public CollidingFlightsDTO(string flightTag1_, string flightTag2_, string collidingAircraftsString_)
        {
            flightTag1 = flightTag1_;
            flightTag2 = flightTag2_;
            collidingAircraftsString = collidingAircraftsString_; 
        }

        public string flightTag1 { get; set; }
        public string flightTag2 { get; set; }
        public string collidingAircraftsString { get; set; }
    }
}
