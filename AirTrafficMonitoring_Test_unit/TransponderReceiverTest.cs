using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using SWT_Gruppe10_AirTraficMonitoring;


namespace AirTrafficMonitoring_Test_unit
{
    public class TransponderReceiverTest
    {
        private ITransponderReceiver fakeTransponderReceiver_;

        //HER MANGLER UUT

        [SetUp]
        public void Setup()
        {
            //Laver fake Transponder Data Receiver
            fakeTransponderReceiver_ = Substitute.For<ITransponderReceiver>();
            
        }

        [Test]
        public void TestReception()
        {

        }
    }
}
