using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class Log : ILog
    {
        //private List<CollidingFlightsDTO> previousLoggedFlights = new List<CollidingFlightsDTO>();
        //private List<string> previousLoggedFlights = new List<string>();

        public Log(ILogCollision logCollision)
        {
            logCollision.DeterminedLogEvent += LogCollision;
        }

        public void LogCollision(object sender, AirTrafficEvent e)
        {
            List<FlightDataDTO> aircraftsCollidingList = new List<FlightDataDTO>();
            aircraftsCollidingList = e.AirTrafficList;

            foreach (var flight in aircraftsCollidingList)
            {
                FileStream output =
                    new FileStream("CollidingAircrafts.txt", FileMode.Append, FileAccess.Write);

                StreamWriter fileWriter = new StreamWriter(output);

                fileWriter.WriteLine(flight.CollidingFlightsDto.collidingAircraftsString);

                fileWriter.Close();
            }

           
        }
    }
}
