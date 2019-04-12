using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using SWT_Gruppe10_AirTraficMonitoring;


namespace AirTrafficMonitoring_Test_unit
{
    class TransponderReceiverTest
    {
        private ITransponderReceiver fakeTransponderReceiver_;
        private SortTrackData UUT_;
        private AirTrafficEvent airTrafficEvent_;

        [SetUp]
        public void Setup()
        {
            //Laver fake Transponder Data Receiver
            fakeTransponderReceiver_ = Substitute.For<ITransponderReceiver>();
            UUT_ = new SortTrackData(fakeTransponderReceiver_);

            airTrafficEvent_ = null;
            
            UUT_.SortDataEvent += (e, args) => { airTrafficEvent_ = args; };
        }

        [Test]
        public void TestReception()
        {
            // Setup test data
            List<string> Data_ = new List<string>();
            Data_.Add("ATR423;39045;12932;14000;20151006213456789");
            Data_.Add("BCD123;10005;85890;12000;20151006213456789");
            Data_.Add("XYZ987;25059;75654;4000;20151006213456789");

            fakeTransponderReceiver_.TransponderDataReady
                += Raise.EventWith(new RawTransponderDataEventArgs(Data_));

            Assert.That(UUT_.DataRecieved_, Is.EqualTo(Data_));
        }

        //Nye test bliver udført nedenfor til genaflevering

        [Test]
        public void NewTestReception()
        {
            List<string> Data_ = new List<string>();
            Data_.Add("ATR423;39045;12932;14000;20151006213456789");
            Data_.Add("BCD123;10005;85890;12000;20151006213456789");
            Data_.Add("XYZ987;25059;75654;4000;20151006213456789");

            fakeTransponderReceiver_.TransponderDataReady
                += Raise.EventWith(new RawTransponderDataEventArgs(Data_));

            Assert.That(airTrafficEvent_, Is.Not.Null);
        }

    }
}
