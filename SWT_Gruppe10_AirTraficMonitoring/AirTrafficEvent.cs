using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class AirTrafficEvent : EventArgs
    {

        public List<FlightDataDTO> AirTrafficList { get; set; }

        public AirTrafficEvent(List<FlightDataDTO> airTrafficList_)
        {
            AirTrafficList = airTrafficList_;
        }

    }
}
