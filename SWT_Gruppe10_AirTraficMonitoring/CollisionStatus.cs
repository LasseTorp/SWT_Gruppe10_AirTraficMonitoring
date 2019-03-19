using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    class CollisionStatus : ICollisionStatus
    {
        private bool collisionStatus_;
        private string aircraftsColliding_;
        private string aircraftsInAirspace; 
        private List<FlightDataDTO> aircraftList;

        private ILog log;
        private IPrint print; 

        public CollisionStatus(IDataCalculator iDataCalculator)
        {
            iDataCalculator.DataCalculatedEvent += RecieveData;
        }

        public void RecieveData(object sender, AirTrafficEvent airTrafficEvent)
        {
            aircraftList = airTrafficEvent.AirTrafficList; 
            DetectCollision();
        }

        public void DetectCollision()
        {
            for (int i = 0; i < aircraftList.Count; i++)
            {
                for (int j = i+1; j < aircraftList.Count ; j++)
                {
                    if (aircraftList[i].Altitude - aircraftList[j].Altitude <= 300)
                    {
                        double xDistance = (aircraftList[i].XCor - aircraftList[j].XCor);
                        double xPower = Math.Pow((aircraftList[i].XCor - aircraftList[j].XCor), 2);

                        double yDistance = (aircraftList[i].YCor - aircraftList[j].YCor);
                        double yPower = Math.Pow((aircraftList[i].YCor - aircraftList[j].YCor), 2);

                        double c = Math.Sqrt(xPower + yPower);

                        if (xDistance <= 500 || yDistance <= 5000|| c <= 5000)
                        {
                            collisionStatus_ = true;
                            aircraftsColliding_ = aircraftList[i].Tag + " is within the collisionrange with " +
                                                  aircraftList[j].Tag;
                            //mangler noget med string aircraftsinairspace
                        }
                        else
                        {
                            collisionStatus_ = false;
                            //mangler noget med string aircraftsinairspace
                        }
                    }
                    else
                    {
                        collisionStatus_ = false;
                        //mangler noget med string aircraftsinairspace
                    }
                }
            }
            ShowData();
        }

        public void ShowData()
        {
            if (collisionStatus_ == true)
            {
                log.LogCollision(aircraftsColliding_);
            }
            else
            {
                print.PrintAircraftInfo(aircraftsInAirspace);
            }
        }
    }
}
