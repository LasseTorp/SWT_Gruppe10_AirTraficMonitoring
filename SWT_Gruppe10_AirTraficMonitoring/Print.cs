using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    class Print : IPrint
    {
        public Print(IDataCalculator dataCalculator, ILogCollision logCollision)
        {
            dataCalculator.DataCalculatedEvent += ConsolePrint;
            logCollision.DeterminedLogEvent += ConsolePrintCollision;
        }
        public void ConsolePrint(object sender, AirTrafficEvent e)
        {
            foreach (var aircraftsInAirspace in e.AirTrafficList)
            {
                if (aircraftsInAirspace.Course != 0 || aircraftsInAirspace.Velocity != 0)
                {
                    Console.WriteLine("Time: " + aircraftsInAirspace.TimeStamp + ":" +
                                      aircraftsInAirspace.TimeStamp.Millisecond + " Aircrafttag: " +
                                      aircraftsInAirspace.Tag + " Altitude: " + aircraftsInAirspace.Altitude +
                                      " X-Cor: " + aircraftsInAirspace.XCor + " Y-Cor " + aircraftsInAirspace.YCor +
                                      " Course: " + aircraftsInAirspace.Course + " Velocity: " +
                                      aircraftsInAirspace.Velocity + " m/s \n");
                }
            }
        }

        public void ConsolePrintCollision(object sender, AirTrafficEvent e)
        {
            foreach (var aircraftsInAirspaceColliding in e.AirTrafficList)
            {
                Console.WriteLine(aircraftsInAirspaceColliding.CollidingFlightsDto.collidingAircraftsString + "\n");
            }
        }
    }
}
