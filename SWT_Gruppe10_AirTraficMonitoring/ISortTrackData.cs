using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public interface ISortTrackData
    {
        event EventHandler<AirTrafficEvent> SortDataEvent;
        void SortData(object sender, RawTransponderDataEventArgs e);
        
    }
}
