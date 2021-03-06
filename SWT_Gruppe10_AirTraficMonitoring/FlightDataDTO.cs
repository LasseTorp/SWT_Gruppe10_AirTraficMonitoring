﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class FlightDataDTO
    {

        public string Tag { get; set; }
        public int XCor { get; set; }
        public int YCor { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Velocity { get; set; }
        public int Course { get; set; }

        public CollidingFlightsDTO CollidingFlightsDto { get; set; } 

        public FlightDataDTO(string tag, int xcor, int ycor, int altitude, DateTime timestamp, int velocity, int course, CollidingFlightsDTO collidingFlightsDTO)
        {
            Tag = tag;
            XCor = xcor;
            YCor = ycor;
            Altitude = altitude;
            TimeStamp = timestamp;
            Velocity = velocity;
            Course = course;
            CollidingFlightsDto = collidingFlightsDTO; 
        }

    }
}
