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

            //Taget fra kodeeksemplet, der er bare ændret at receiver sendes til vores sortTrackData 

            // Using the real transponder data receiver
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            // Dependency injection with the real TDR
            var system = new SortTrackData(receiver);

            // Let the real TDR execute in the background
            while (true)
                Thread.Sleep(1000);


        }
    }
}
