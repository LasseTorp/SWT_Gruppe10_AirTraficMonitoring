﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    interface ICollisionStatus
    {
        void RecieveData(object sender, AirTrafficEvent airTrafficEvent);
        void DetectCollision();
        void ShowData(); 
    }
}