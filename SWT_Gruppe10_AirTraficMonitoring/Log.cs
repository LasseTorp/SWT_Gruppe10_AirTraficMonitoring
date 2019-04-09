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

        public void LogCollision(List<CollidingFlightsDTO> aircraftsCollidingList)
        {
            for (int i = 0; i < aircraftsCollidingList.Count; i++)
            {
                if (!previousLoggedFlights.Contains(aircraftsCollidingList[i].flightTag1+" & "+aircraftsCollidingList[i].flightTag2))
                {
                    FileStream output =
                        new FileStream("CollidingAircrafts.txt", FileMode.Append, FileAccess.Write);

                    StreamWriter fileWriter = new StreamWriter(output);

                    fileWriter.WriteLine(aircraftsCollidingList[i].collidingAircraftsString);

                    fileWriter.Close();

                    previousLoggedFlights.Add(aircraftsCollidingList[i].flightTag1+" & "+aircraftsCollidingList[i].flightTag2);

              
                }
            }
        }
    }
}
