using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    class DataCalculator : IDataCalculator
    {

        public DataCalculator(ISortTrackData sortTrackData)
        {
            sortTrackData.EVENT += calculate();
        }

        public void calculate()
        {
            
        }

        public void CalculateVelocity()
        {
            throw new NotImplementedException();
        }

        public void calculateCourse()
        {
            throw new NotImplementedException();
        }
    }
}
