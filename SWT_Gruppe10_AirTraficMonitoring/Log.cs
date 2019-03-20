using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class Log : ILog
    {
        public void LogCollision(List<string> aircraftsCollidingList)
        {
            StreamWriter sw = new StreamWriter("Collision.txt");
            sw.WriteLine(aircraftsCollidingList);
            //log aircraftscolliding til fil 
            
        }

    }
}
