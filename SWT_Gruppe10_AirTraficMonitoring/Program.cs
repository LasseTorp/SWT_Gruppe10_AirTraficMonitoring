using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            
            var system = new SortTrackData(receiver);
            
            while (true)
                Thread.Sleep(1000);


        }
    }
}
