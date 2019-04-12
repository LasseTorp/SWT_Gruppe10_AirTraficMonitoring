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
    class LogCollisionTest
    {
        private AirTrafficEvent airTrafficEvent_;
        private ICollisionStatus fakeCollisionStatus_;
        private LogCollision UUT_;

        [SetUp]
        public void SetUp()
        {
            //Laver fake Transponder Data Receiver
            fakeCollisionStatus_ = Substitute.For<ICollisionStatus>();
            UUT_ = new LogCollision(fakeCollisionStatus_);

            airTrafficEvent_ = null;

            UUT_.DeterminedLogEvent += (e, args) => { airTrafficEvent_ = args; };
        }

        [Test]
        public void NewTestReception()
        {
            List<FlightDataDTO> flightDataList = new List<FlightDataDTO>();
            FlightDataDTO flightData = new FlightDataDTO("ATR423", 39045, 12932, 14000, DateTime.Now, 30, 105, new CollidingFlightsDTO("", "", ""));
            flightDataList.Add(flightData);

            fakeCollisionStatus_.CollisionStatusEvent += Raise.EventWith(new AirTrafficEvent(flightDataList));

            Assert.That(airTrafficEvent_, Is.Not.Null);
        }
        [Test]
        public void LoggingDataIfNotLoggedBefore()
        {   
            FlightDataDTO flightData1 = new FlightDataDTO("ATR423", 39045, 12932, 14000, DateTime.Now, 30, 105, new CollidingFlightsDTO("","",""));
            FlightDataDTO flightData2 = new FlightDataDTO("ATR423", 39045, 12932, 14000, DateTime.Now, 30, 105, new CollidingFlightsDTO("","",""));


            List<FlightDataDTO> flightDataList1 = new List<FlightDataDTO>();
            List<FlightDataDTO> flightDataList2 = new List<FlightDataDTO>();
            flightDataList1.Add(flightData1);
            flightDataList2.Add(flightData2);



            List<FlightDataDTO> flightDataList3 = new List<FlightDataDTO>();
            flightDataList3.Add(flightData1);
            flightDataList3.Add(flightData2);

            //fakeCollisionStatus_.CollisionStatusEvent += Raise.EventWith(new AirTrafficEvent(flightDataList2));
            //airTrafficEvent_ = null;
            //fakeCollisionStatus_.CollisionStatusEvent += Raise.EventWith(new AirTrafficEvent(flightDataList1));

            fakeCollisionStatus_.CollisionStatusEvent += Raise.EventWith(new AirTrafficEvent(flightDataList3));

            Assert.That(airTrafficEvent_.AirTrafficList.Count, Is.EqualTo(1));
        }
    }
}
