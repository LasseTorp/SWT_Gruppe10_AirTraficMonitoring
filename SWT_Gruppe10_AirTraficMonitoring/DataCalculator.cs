using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    class DataCalculator : IDataCalculator
    {
        public List<FlightDataDTO> oldTrackData;
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
            
        }

        public void calculateCourse(FlightDataDTO flight)
        {
            
        }
    }
}
