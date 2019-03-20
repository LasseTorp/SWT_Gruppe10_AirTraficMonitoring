using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class CollisionStatus : ICollisionStatus
    {
        public bool collisionStatus_ { get; set; }
        private string aircraftsColliding_;
        private string aircraftInAirspace_;
        private List<string> aircraftsInAirspaceList; 
        public List<FlightDataDTO> aircraftList { get; set; }
        public List<FlightDataDTO> collidingAircrafts;
        public List<CollidingFlightsDTO> CollidingFlightsDTOs; 

        private ILog log;
        private IPrint print; 

        public CollisionStatus(IDataCalculator iDataCalculator)
        {
            iDataCalculator.DataCalculatedEvent += RecieveData;
            collisionStatus_ = false; 
            log = new Log();
            print = new Print();

        }

        public void RecieveData(object sender, AirTrafficEvent airTrafficEvent)
        {
            aircraftList = new List<FlightDataDTO>();
            aircraftList = airTrafficEvent.AirTrafficList; 
            DetectCollision(aircraftList);
        }

        public void DetectCollision(List<FlightDataDTO> aircraftList_)
        {
            aircraftsInAirspaceList = new List<string>();
            CollidingFlightsDTOs = new List<CollidingFlightsDTO>();


            for (int i = 0; i < aircraftList_.Count; i++)
            {
                for (int j = i+1; j < aircraftList_.Count ; j++)
                {
                    if (Math.Abs(aircraftList_[i].Altitude - aircraftList_[j].Altitude) <= 300)
                    {
                        double xDistance = Math.Abs(aircraftList_[i].XCor - aircraftList_[j].XCor);
                        double xPower = Math.Pow((aircraftList_[i].XCor - aircraftList_[j].XCor), 2);

                        double yDistance = Math.Abs(aircraftList_[i].YCor - aircraftList_[j].YCor);
                        double yPower = Math.Pow((aircraftList_[i].YCor - aircraftList_[j].YCor), 2);

                        double c = Math.Sqrt(xPower + yPower);

                        if ((xDistance <= 5000 && yDistance <= 5000) || c <= 5000)
                        {
                            collisionStatus_ = true;
                            aircraftsColliding_ = "Time: " + aircraftList_[i].TimeStamp+":"+aircraftList_[i].TimeStamp.Millisecond + " - " + aircraftList_[i].Tag + " is within the collisionrange of " +
                                                  aircraftList_[j].Tag;

                            CollidingFlightsDTOs.Add(new CollidingFlightsDTO(aircraftList_[i].Tag, aircraftList_[j].Tag, aircraftsColliding_));

                        }
                        else
                        {
                            collisionStatus_ = false;
                            aircraftInAirspace_ = "Time: " +aircraftList_[i].TimeStamp + ":" + aircraftList_[i].TimeStamp.Millisecond + " Aircrafttag: " + aircraftList_[i].Tag + " Altitude: "+aircraftList_[i].Altitude+" X-Cor: " + aircraftList_[i].XCor +
                                                  " Y-Cor: " + aircraftList_[i].YCor + " Course: " + aircraftList_[i].Course + " Velocity: " +
                                                  aircraftList_[i].Velocity+" m/s \n";
                            aircraftsInAirspaceList.Add(aircraftInAirspace_);
                        }
                    }
                    else
                    {
                        collisionStatus_ = false;
                        aircraftInAirspace_ = "Time: " + aircraftList_[i].TimeStamp + ":" + aircraftList_[i].TimeStamp.Millisecond + " Aircrafttag: " + aircraftList_[i].Tag + " Altitude: " + aircraftList_[i].Altitude + " X-Cor: " + aircraftList_[i].XCor +
                                              " Y-Cor: " + aircraftList_[i].YCor + " Course: " + aircraftList_[i].Course + " Velocity: " +
                                              aircraftList_[i].Velocity+" m/s \n";
                        aircraftsInAirspaceList.Add(aircraftInAirspace_);
                    }
                }
            }
            ShowData();
        }

        public void ShowData()
        {
                log.LogCollision(CollidingFlightsDTOs);
                print.PrintAircraftInfo(aircraftsInAirspaceList);
        }
    }
}
