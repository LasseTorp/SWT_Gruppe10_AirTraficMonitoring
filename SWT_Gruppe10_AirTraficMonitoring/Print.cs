using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    class Print : IPrint
    {
        public DataCalculator(ISortTrackData sortTrackData)
        {
            firstTime = true;
            sortTrackData.SortDataEvent += calculate;
            oldTrackData = new List<FlightDataDTO>();
        }

        public Print(IDataCalculator dataCalculator)
        {
            dataCalculator.DataCalculatedEvent += ConsolePrint;

        }
        public void ConsolePrint(object sender, AirTrafficEvent e)
        {
            foreach (var aircraftsInAirspace in aircraftsInAirspaceList)
            {
                Console.WriteLine(aircraftsInAirspace);
            }
        }
    }
}
