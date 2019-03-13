using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    interface IDataCalculator
    {
        event EventHandler<AirTrafficEvent> DataCalculatedEvent;
        void calculate(object sender, AirTrafficEvent e);
        void CalculateVelocity(FlightDataDTO flight);
        void calculateCourse(FlightDataDTO flight);
    }
}
