﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_Gruppe10_AirTraficMonitoring
{
    public interface IPrint
    {
        void ConsolePrint(object sender, AirTrafficEvent e);
    }
}
