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
        private List<DataContainerDTO> aircraftList;

        public CollisionStatus(IDataCalculator iDataCalculator)
        {
            iDataCalculator.DataCalculatedEvent += DetectCollision; 
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
                        }
                        else
                        {
                            collisionStatus_ = false;
                        }
                    }
                    else
                    {
                        collisionStatus_ = false;
                    }
                }
            }
            ShowData();
        }

        public void ShowData()
        {
            if (collisionStatus_ == true)
            {

            }
            else
            {
                
            }
        }
    }
}
