using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class LogCollision : ILogCollision
    {
        private List<string> previousLoggedFlights = new List<string>();
        public event EventHandler<AirTrafficEvent> DeterminedLogEvent;
        
        public LogCollision(ICollisionStatus collisionStatus)
        {
            collisionStatus.CollisionStatusEvent += DetermineLog;
        }

        public void DetermineLog(object sender, AirTrafficEvent e)
        {
            List<FlightDataDTO> aircraftsCollidingList = new List<FlightDataDTO>();
            aircraftsCollidingList = e.AirTrafficList;

            List<FlightDataDTO> newCollisionFlights = new List<FlightDataDTO>();

            for (int i = 0; i < aircraftsCollidingList.Count; i++)
            {
                if (aircraftsCollidingList[i].CollidingFlightsDto.collidingAircraftsString != "")
                {
                    if (!previousLoggedFlights.Contains(aircraftsCollidingList[i].CollidingFlightsDto.flightTag1 + " & " + aircraftsCollidingList[i].CollidingFlightsDto.flightTag2))
                    {

                        newCollisionFlights.Add(aircraftsCollidingList[i]);

                        previousLoggedFlights.Add(aircraftsCollidingList[i].CollidingFlightsDto.flightTag1 + " & " + aircraftsCollidingList[i].CollidingFlightsDto.flightTag2);

                    }
                }
                
            }

            AirTrafficEvent airTrafficEvent = new AirTrafficEvent(newCollisionFlights);
            DeterminedLogEvent?.Invoke(this, airTrafficEvent);
        }
    }
}
