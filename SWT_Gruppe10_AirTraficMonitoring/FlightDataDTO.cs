using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class FlightDataDTO
    {

        public string Tag { get; set; }
        public int XCor { get; set; }
        public int YCor { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Velocity { get; set; }
        public int Course { get; set; }
        public string Collision { get; set; }

    }
}
