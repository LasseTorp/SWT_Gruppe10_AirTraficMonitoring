using System;
using System.Security.Cryptography;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public class DataContainer : EventArgs
    {

        public string Tag { get; set; }
        public int XCor { get; set; }
        public int YCor { get; set; }
        public int Altitude { get; set;  }
        public DateTime TimeStamp { get; set; }
        public int Velocity { get; set; }
        public int Course { get; set; }
        public string Collision { get; set; }
        
    }
}
