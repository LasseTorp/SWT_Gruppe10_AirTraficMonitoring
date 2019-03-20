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
        private List<CollidingFlightsDTO> previousLoggedFlights = new List<CollidingFlightsDTO>();

        public void LogCollision(List<CollidingFlightsDTO> aircraftsCollidingList)
        {
            for (int i = 0; i < aircraftsCollidingList.Count; i++)
            {
                if (!previousLoggedFlights.Contains(aircraftsCollidingList[i]))
                {
                    FileStream output =
                        new FileStream("CollidingAircrafts.txt", FileMode.OpenOrCreate, FileAccess.Write);

                    StreamWriter fileWriter = new StreamWriter(output);

                    fileWriter.WriteLine(aircraftsCollidingList[i].collidingAircraftsString);

                    fileWriter.Close();

                    //OVERSKRIVER DEN TIDLIGERE KOLLISION (FEJL)
                }
            }
        }
    }
}
