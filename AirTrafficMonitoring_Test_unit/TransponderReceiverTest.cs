﻿using System;
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
    public class TransponderReceiverTest
    {
        private ITransponderReceiver fakeTransponderReceiver_;
        private SortTrackData uut_;

        //HER MANGLER UUT

        [SetUp]
        public void Setup()
        {
            //Laver fake Transponder Data Receiver
            fakeTransponderReceiver_ = Substitute.For<ITransponderReceiver>();
            uut_ = new SortTrackData(fakeTransponderReceiver_);
            
        }

        [Test]
        public void TestReception()
        {
            // Setup test data
            List<string> Data_ = new List<string>();
            Data_.Add("ATR423;39045;12932;14000;20151006213456789");
            Data_.Add("BCD123;10005;85890;12000;20151006213456789");
            Data_.Add("XYZ987;25059;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            fakeTransponderReceiver_.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(Data_));

            // Assert something here or use an NSubstitute Received
            Assert.That(uut_.DataRecieved_, Is.EqualTo(Data_));
        }
    }
}
