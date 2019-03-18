using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class DataCalculator : IDataCalculator
    {
        public List<FlightDataDTO> oldTrackData { set; get; }
        public List<FlightDataDTO> newTrackData;
        public bool firstTime { set; get; }
        

        public DataCalculator(ISortTrackData sortTrackData)
        {
            firstTime = true;
            sortTrackData.SortDataEvent += calculate;
        }

        public event EventHandler<AirTrafficEvent> DataCalculatedEvent;

        public void calculate(object sender, AirTrafficEvent e)
        {
            newTrackData = new List<FlightDataDTO>();
            newTrackData = e.AirTrafficList;

            if (firstTime == false)
            {
                foreach (var Flight in newTrackData)
                {
                    CalculateVelocity(Flight);
                    calculateCourse(Flight);
                }
            }

            oldTrackData = newTrackData;
            firstTime = false;

            AirTrafficEvent airTrafficEvent = new AirTrafficEvent();
            airTrafficEvent.AirTrafficList = newTrackData;
            DataCalculatedEvent?.Invoke(this, airTrafficEvent);
            
        }

        public void CalculateVelocity(FlightDataDTO flight)
        {
            foreach (var oldFlight in oldTrackData)
            {
                if (oldFlight.Tag == flight.Tag)
                {
                    int xOld = oldFlight.XCor;
                    int yOld = oldFlight.YCor;
                    int xNew = flight.XCor;
                    int yNew = flight.YCor;

                    //Udregner distancen flyet er fløjet
                    double distance = Math.Sqrt(Math.Pow((xOld - xNew), 2) + Math.Pow((yOld - yNew), 2));

                    // Udregn tiden det har taget

                    double time = (flight.TimeStamp - oldFlight.TimeStamp).TotalSeconds;

                    int velocity = Convert.ToInt32(distance / time);

                    flight.Velocity = velocity;
                }
            }
        }

        public void calculateCourse(FlightDataDTO flight)
        {
            foreach (var oldFlight in oldTrackData)
            {
                if (oldFlight.Tag == flight.Tag)
                {
                    int x1 = oldFlight.XCor;
                    int x2 = flight.XCor;
                    int y1 = oldFlight.YCor;
                    int y2 = flight.YCor;

                    if (x1 == x2)
                    {
                        flight.Course = 0;
                    }
                    else
                    {
                        int course;

                        course = Convert.ToInt32((180 / Math.PI) * Math.Atan((x2 - x1) / (y2 - y1)));

                        if (course < 0)
                        {
                            course = course + 360;
                            flight.Course = course;
                        }
                        else
                        {
                            flight.Course = course;
                        }

                    }
                }
            }

        }
    }
}
