﻿using System;
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
        private List<FlightDataDTO> aircraftList;
        private List<string> aircraftscollidingStrings;

        private ILog log;
        private IPrint print; 

        public CollisionStatus(IDataCalculator iDataCalculator)
        {
            iDataCalculator.DataCalculatedEvent += RecieveData;
        }

        public void RecieveData(object sender, AirTrafficEvent airTrafficEvent)
        {
            aircraftList = airTrafficEvent.AirTrafficList; 
            DetectCollision(aircraftList);
        }

        public void DetectCollision(List<FlightDataDTO> aircraftList_)
        {
            for (int i = 0; i < aircraftList_.Count; i++)
            {
                for (int j = i+1; j < aircraftList_.Count ; j++)
                {
                    if (aircraftList_[i].Altitude - aircraftList_[j].Altitude <= 300)
                    {
                        double xDistance = (aircraftList_[i].XCor - aircraftList_[j].XCor);
                        double xPower = Math.Pow((aircraftList_[i].XCor - aircraftList_[j].XCor), 2);

                        double yDistance = (aircraftList_[i].YCor - aircraftList_[j].YCor);
                        double yPower = Math.Pow((aircraftList_[i].YCor - aircraftList_[j].YCor), 2);

                        double c = Math.Sqrt(xPower + yPower);

                        if (xDistance <= 500 || yDistance <= 5000|| c <= 5000)
                        {
                            collisionStatus_ = true;
                            aircraftsColliding_ = aircraftList_[i].TimeStamp + aircraftList_[i].Tag + " is within the collisionrange with " +
                                                  aircraftList_[j].Tag;
                            aircraftscollidingStrings.Add(aircraftsColliding_);

                        }
                        else
                        {
                            collisionStatus_ = false;
                            aircraftInAirspace_ = "Aircrafttag: " + aircraftList_[i].Tag + ""+aircraftList_[i].Altitude+"" + aircraftList_[i].XCor +
                                                  "" + aircraftList_[i].YCor + "" + aircraftList_[i].Course + "" +
                                                  aircraftList_[i].Velocity;
                            aircraftsInAirspaceList.Add(aircraftInAirspace_);
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
                log.LogCollision(aircraftscollidingStrings);
            }
            else
            {
                print.PrintAircraftInfo(aircraftsInAirspaceList);
            }
        }
    }
}
