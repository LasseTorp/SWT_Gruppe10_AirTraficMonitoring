using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    class LogCollision : ILogCollision
    {
        private List<string> previousLoggedFlights = new List<string>();
        public event EventHandler<AirTrafficEvent> DeterminedLogEvent;
       

        public LogCollision(ICollisionStatus collisionStatus)
        {
            collisionStatus.
        }

        public void DetermineLog(object sender, AirTrafficEvent e)
        {
            for (int i = 0; i < aircraftsCollidingList.Count; i++)
            {
                if (!previousLoggedFlights.Contains(aircraftsCollidingList[i].flightTag1 + " & " + aircraftsCollidingList[i].flightTag2))
                {
                   
                    previousLoggedFlights.Add(aircraftsCollidingList[i].flightTag1 + " & " + aircraftsCollidingList[i].flightTag2);


                }
            }
        }
    }
}
