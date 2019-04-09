using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public interface ICollisionStatus
    {
        void RecieveData(object sender, AirTrafficEvent airTrafficEvent);
        void DetectCollision(List<FlightDataDTO> aircraftList);
        //void ShowData(); 
    }
}
