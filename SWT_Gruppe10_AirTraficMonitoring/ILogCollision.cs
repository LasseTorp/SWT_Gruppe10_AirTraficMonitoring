using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public interface ILogCollision
    {
        event EventHandler<AirTrafficEvent> DeterminedLogEvent;
        void DetermineLog(object sender, AirTrafficEvent e);
    }
}
