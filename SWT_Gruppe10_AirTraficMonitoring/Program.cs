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
            
            var filter = new Filter(system);

            var calculator = new DataCalculator(filter);
            
            var collision = new CollisionStatus(calculator);

            var logCollision = new LogCollision(collision);

            var consolePrint = new Print(calculator, logCollision);

            var fileLog = new Log(logCollision);
            
            while (true)
                Thread.Sleep(1000);


        }
    }
}
