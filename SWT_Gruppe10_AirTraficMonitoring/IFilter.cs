using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public interface IFilter
    {
        event EventHandler<AirTrafficEvent> FiltratedEvent;

        void FiltrateArea(object sender, AirTrafficEvent e);

    }
}
