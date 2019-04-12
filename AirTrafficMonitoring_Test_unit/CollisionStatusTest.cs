using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using SWT_Gruppe10_AirTraficMonitoring;

namespace AirTrafficMonitoring_Test_unit
{
    [TestFixture]
    class CollisionStatusTest
    {
        private CollisionStatus UUT_;
        private AirTrafficEvent airTrafficEvent_;
        private IDataCalculator dataCalculator_; 

        [SetUp]
        public void SetUp()
        {
            airTrafficEvent_ = null;
            dataCalculator_ = NSubstitute.Substitute.For<IDataCalculator>();
            UUT_ = new CollisionStatus(dataCalculator_);

            UUT_.CollisionStatusEvent += (e, args) => { airTrafficEvent_ = args; };
        }

        [TestCase(10000, 11000, 7000, 18000, 11000, 7000, false)]
        [TestCase(10000, 11000, 7000, 14500, 11000, 7000, true)]
        public void detectCollision_dependingonXcoordinates_collisionstatus(int x1, int y1, int altitude1, int x2, int y2, int altitude2, bool collisionStatusFromTest)
        {
            List<FlightDataDTO> TrackData = new List<FlightDataDTO>(); 
            FlightDataDTO flight1 = new FlightDataDTO("ATR423", x1, y1, altitude1, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO flight2 = new FlightDataDTO("ATB675", x2, y2, altitude2, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            TrackData.Add(flight1);
            TrackData.Add(flight2);

            UUT_.DetectCollision(TrackData);

            Assert.That(UUT_.collisionStatus_, Is.EqualTo(collisionStatusFromTest));
        }

        [TestCase(10000, 20000, 7000, 10000, 10000, 7000, false)]
        [TestCase(10000, 11000, 7000, 10000, 11600, 7000, true)]
        public void detectCollision_dependingonYcoordinates_collisionstatus(int x1, int y1, int altitude1, int x2, int y2, int altitude2, bool collisionStatusFromTest)
        {
            List<FlightDataDTO> TrackData = new List<FlightDataDTO>();
            FlightDataDTO flight1 = new FlightDataDTO("ATR423", x1, y1, altitude1, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO flight2 = new FlightDataDTO("ATB675", x2, y2, altitude2, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            TrackData.Add(flight1);
            TrackData.Add(flight2);

            UUT_.DetectCollision(TrackData);

            Assert.That(UUT_.collisionStatus_, Is.EqualTo(collisionStatusFromTest));
        }

        [TestCase(10000, 11000, 20000, 10000, 11000, 7000, false)]
        [TestCase(10000, 11000, 7000, 10000, 11000, 7200, true)]
        public void detectCollision_dependingOnAltitude_collisionstatus(int x1, int y1, int altitude1, int x2, int y2, int altitude2, bool collisionStatusFromTest)
        {
            List<FlightDataDTO> TrackData = new List<FlightDataDTO>();
            FlightDataDTO flight1 = new FlightDataDTO("ATR423", x1, y1, altitude1, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO flight2 = new FlightDataDTO("ATB675", x2, y2, altitude2, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            TrackData.Add(flight1);
            TrackData.Add(flight2);

            UUT_.DetectCollision(TrackData);

            Assert.That(UUT_.collisionStatus_, Is.EqualTo(collisionStatusFromTest));
        }

        [Test]
        public void testRecieve()
        {
            List<FlightDataDTO> Data_ = new List<FlightDataDTO>();

            Data_.Add(new FlightDataDTO("ABCD", 10, 10, 10, DateTime.Now, 10, 10, new CollidingFlightsDTO("", "", "")));

            dataCalculator_.DataCalculatedEvent += Raise.EventWith(this, new AirTrafficEvent(Data_));

            Assert.That(UUT_.aircraftList, Is.EqualTo(Data_));
        }


        //The following is the work that has been done before handing in the first time and handing it in again 
        [Test]
        public void testRecieveFunction()
        {
            List<FlightDataDTO> listOfFlightData = new List<FlightDataDTO>();

            FlightDataDTO flightData1 = new FlightDataDTO("ABC123", 23000, 27000, 1500, DateTime.Now, 50, 0, new CollidingFlightsDTO("","",""));
            FlightDataDTO flightData2 = new FlightDataDTO("DEF456", 35000, 38000, 1800, DateTime.Now, 50, 0, new CollidingFlightsDTO("","",""));

            listOfFlightData.Add(flightData1);
            listOfFlightData.Add(flightData2);

            dataCalculator_.DataCalculatedEvent += Raise.EventWith(new AirTrafficEvent(listOfFlightData));

            Assert.That(airTrafficEvent_, Is.Not.Null);
        }

        [TestCase("ABC123", "DEF456", 10000, 11000, 20000, 10000, 11000, 7000, "", "")]
        [TestCase("ABC123", "DEF456", 10000, 11000, 7000, 10000, 11000, 7200, "ABC123", "DEF456")]
        [TestCase("ABC123", "DEF456", 10000, 11000, 7000, 10000, 11000, 7300, "ABC123", "DEF456")]
        [TestCase("ABC123", "DEF456", 10000, 11000, 7000, 10000, 11000, 7301, "", "")]
        public void detectCollisionDependingOnAltitude(string flightTag1Test, string flightTag2Test, int x1, int y1, int altitude1, int x2, int y2, int altitude2, string expectedTag1, string expectedTag2)
        {
            List<FlightDataDTO> listOfFlightData = new List<FlightDataDTO>();

            FlightDataDTO flight1 = new FlightDataDTO(flightTag1Test, x1, y1, altitude1, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO flight2 = new FlightDataDTO(flightTag2Test, x2, y2, altitude2, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            listOfFlightData.Add(flight1);
            listOfFlightData.Add(flight2);

            dataCalculator_.DataCalculatedEvent += Raise.EventWith(new AirTrafficEvent(listOfFlightData)); 

            //Assert.That(airTrafficEvent_.AirTrafficList[0].CollidingFlightsDto.flightTag1, Is.EqualTo(expectedTag1));
            //Assert.That(airTrafficEvent_.AirTrafficList[0].CollidingFlightsDto.flightTag2, Is.EqualTo(expectedTag2));

            Assert.IsTrue((airTrafficEvent_.AirTrafficList[0].CollidingFlightsDto.flightTag1 == expectedTag1) && (airTrafficEvent_.AirTrafficList[0].CollidingFlightsDto.flightTag2 == expectedTag2));
        }

        [TestCase("ABC123", "DEF456", 10000, 11000, 9000, 15001, 11000, 9000, "", "")] //Boundary
        [TestCase("ABC123", "DEF456", 10000, 11000, 9000, 15000, 11000, 9000, "ABC123", "DEF456")] //Boundary
        [TestCase("ABC123", "DEF456", 10000, 11000, 9000, 11000, 11000, 9000, "ABC123", "DEF456")] //Far too close
        [TestCase("ABC123", "DEF456", 10000, 11000, 9000, 27000, 11000, 9000, "", "")] //Far too far away 
        public void detectCollisionDependingOnXCoordinates(string flightTag1Test, string flightTag2Test, int x1, int y1, int altitude1, int x2, int y2, int altitude2, string expectedTag1, string expectedTag2)
        {
            List<FlightDataDTO> listOfFlightData = new List<FlightDataDTO>();

            FlightDataDTO flight1 = new FlightDataDTO(flightTag1Test, x1, y1, altitude1, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO flight2 = new FlightDataDTO(flightTag2Test, x2, y2, altitude2, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            listOfFlightData.Add(flight1);
            listOfFlightData.Add(flight2);

            dataCalculator_.DataCalculatedEvent += Raise.EventWith(new AirTrafficEvent(listOfFlightData));

            Assert.IsTrue((airTrafficEvent_.AirTrafficList[0].CollidingFlightsDto.flightTag1 == expectedTag1) && (airTrafficEvent_.AirTrafficList[0].CollidingFlightsDto.flightTag2 == expectedTag2));
        }

        [TestCase("ABC123", "DEF456", 10000, 11000, 9000, 10000, 16001, 9000, "", "")] //Boundary
        [TestCase("ABC123", "DEF456", 10000, 11000, 9000, 15000, 16000, 9000, "ABC123", "DEF456")] //Boundary
        [TestCase("ABC123", "DEF456", 10000, 11000, 9000, 10000, 12000, 9000, "ABC123", "DEF456")] //Far too close
        [TestCase("ABC123", "DEF456", 10000, 11000, 9000, 27000, 20000, 9000, "", "")] //Far too far away 
        public void detectCollisionDependingOnYCoordinates(string flightTag1Test, string flightTag2Test, int x1, int y1, int altitude1, int x2, int y2, int altitude2, string expectedTag1, string expectedTag2)
        {
            List<FlightDataDTO> listOfFlightData = new List<FlightDataDTO>();

            FlightDataDTO flight1 = new FlightDataDTO(flightTag1Test, x1, y1, altitude1, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            FlightDataDTO flight2 = new FlightDataDTO(flightTag2Test, x2, y2, altitude2, new DateTime(2019, 4, 17, 14, 30, 40), 0, 0, new CollidingFlightsDTO("", "", ""));
            listOfFlightData.Add(flight1);
            listOfFlightData.Add(flight2);

            dataCalculator_.DataCalculatedEvent += Raise.EventWith(new AirTrafficEvent(listOfFlightData));

            Assert.IsTrue((airTrafficEvent_.AirTrafficList[0].CollidingFlightsDto.flightTag1 == expectedTag1) && (airTrafficEvent_.AirTrafficList[0].CollidingFlightsDto.flightTag2 == expectedTag2));
        }
    }


}
