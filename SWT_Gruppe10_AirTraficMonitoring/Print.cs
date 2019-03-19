using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    class Print : IPrint
    {
        public void PrintAircraftInfo(List<string> aircraftsInAirspaceList)
        {
            foreach (var aircraftsInAirspace in aircraftsInAirspaceList)
            {
                Console.WriteLine(aircraftsInAirspace);
            }
        }

    }
}
